#!/usr/bin/env dotnet-script

using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;

// TODO
var myFile = @"This PC\Galaxy Note10+\Card\Switch\Animal Crossing\Animal Crossing 2020-03-23 06꞉02꞉02 PM.jpg";
var myDate = new DateTime(2020,03,23,06,02,02);
// TODO

File.SetCreationTime(myFile, myDate);

// inputs
var inputPath = @"C:\Users\kylemit\Pictures\Switch";
var outputPath = @"C:\Users\kylemit\Pictures\Switch Album2";


// manually go identify each game by hex code and add to this lookup
var gameHexLookup = new Dictionary<string, string> {
    ["F1C11A22FAEE3B82F21B330E1B786A39"] = "Breath of the Wild",
    ["16851BE00BC6068871FE49D98876D6C5"] = "MarioKart 8",
    ["1E95E5926F1CB99A87326D927F27B47E"] = "Switch",
    ["099ECEEF904DB62AEE3A76A3137C241B"] = "Mario Party",
    ["57B4628D2267231D57E0FC1078C0596D"] = "Switch",
    ["0E7DF678130F4F0FA2C88AE72B47AFDF"] = "Super Smash Bros Ultimate",
    ["F489C99A244DF57DCBDC4BFD2DB926F1"] = "Fortnite",
    ["8AEDFF741E2D23FBED39474178692DAF"] = "Super Mario Odyssey",
    ["210BD7B645AF1D6DC1D497E9525121EC"] = "Color Zen",
    ["1CED1B76CE76DB49E48C6F360E022485"] = "Pode",
    ["1668A2AE192E414710E0098220C23017"] = "Hue",
    ["75A32021BE3512D7AA96B2D72F764411"] = "Celeste",
    ["21F2C1B9B6791F4FD5F86868D324646C"] = "TypeRider",
    ["062DD3BC3CF59885A6762E5A30A14CD1"] = "FIFA 18",
    ["906D1DD5BCEDC3E72889D5D23917398E"] = "Unravel two",
    ["CBA841B50A92A904E313AE06DF4EF71A"] = "Splatoon 2",
    ["BF19FBEA37724338D87F26F17A3B97B2"] = "Super Mario Maker 2",
    ["9129043EF2AAD7F1157CF852BACB8F7D"] = "Links Awakening",
    ["2AF2C4CCD5F28D087B476BE33BFE1BF8"] = "Witcher 3",
    ["05DC14F80A13996B94160CD375AFD506"] = "Super NES",
    ["5855558AA8FCD2E87BC21377436CFBF0"] = "Child of Light",
    ["AFCACDE251C5D7A00FA55F133EE3E959"] = "Ori and the Blind Forest",
    ["74EA5D8C57EB2F39A242F585A490F51B"] = "Skyrim",
    ["29DAC76F23A7892B4AE05C9C1BC67E22"] = "Castlevania",
    ["02CB906EA538A35643C1E1484C4B947D"] = "Animal Crossing",
    ["CCFA659F4857F96DDA29AFEDB2E166E6"] = "Switch"
};


// get files
var filesPaths = Directory.GetFiles(inputPath, "*.*", SearchOption.AllDirectories);


var files = filesPaths.Select(fullPath => {
    var fileName = Path.GetFileName(fullPath);
    
    var filePartsRgx = new Regex(@"(\d*)0[012]-(.*)\.");
    var fileParts = filePartsRgx.Match(fileName);
    var timestamp = fileParts.Groups[1].Value;
    var gameHex = fileParts.Groups[2].Value;
    var fileDate = DateTime.ParseExact(timestamp, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);

    var fileInfo = (fullPath, fileName, fileDate, gameHex);
    return fileInfo;
}).ToArray();


// get unique game hex codes
var gameHexValues = files.Select(f => f.gameHex).Distinct().ToArray();

// double check we have all codes, or leave
var missingGames = gameHexValues.Where(name => !gameHexLookup.ContainsKey(name)).ToArray();
if (missingGames.Any()) {
    Console.WriteLine("Missing Games - Add the following titles to gameHexLookup");
    Console.WriteLine(string.Join("\n", missingGames));
    return;
}


// create game folders
foreach (var gameName in gameHexLookup.Values)
{
     var gameDir = Path.Combine(outputPath, gameName);
     Directory.CreateDirectory(gameDir);
}

// move pictures
foreach (var fileInfo in files)
{
    var sourcePath = fileInfo.fullPath;
    
    var gameName =  gameHexLookup[fileInfo.gameHex];
    var dateString = fileInfo.fileDate.ToString("yyyy-MM-dd hh꞉mm꞉ss tt");
    var fileExt = Path.GetExtension(sourcePath);
    var fileName = $"{gameName} {dateString}{fileExt}";
    var targetPath = Path.Combine(outputPath, gameName, fileName);

    // move file
    var safeTargetPath = SafeFileMove(sourcePath, targetPath);

    // fix create date
    File.SetCreationTime(safeTargetPath, fileInfo.fileDate);
}


string SafeFileMove(string sourceFilePath, string destFilePath) {
    
    var destFileDir = Path.GetDirectoryName(destFilePath);
    var destFileName = Path.GetFileNameWithoutExtension(destFilePath);
    var destFileExt = Path.GetExtension(destFilePath);

    var safeDestFilePath = destFilePath;


    // on collion append #
    var i = 1;
    while (File.Exists(safeDestFilePath)) {
        var safeFileName = $"{destFileName} ({i}){destFileExt}";
        safeDestFilePath = Path.Combine(destFileDir, safeFileName);
        i++;
    }


    File.Move(sourceFilePath, safeDestFilePath);

    return safeDestFilePath;
}
