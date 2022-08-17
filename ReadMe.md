# Switch Script

A scripting option to organize the screenshots produced by the nintendo switch


According to the [switchbrew wiki on the SD Filesystem](https://switchbrew.org/wiki/SD_Filesystem), each media file ends with a 32 character *"title-specific hex id"*

For example:

* **Breath of the Wild**: `F1C11A22FAEE3B82F21B330E1B786A39`
* **Animal Crossing: NW**: `02CB906EA538A35643C1E1484C4B947D`


## Folder Structure

### Starting Folder Structure

```none
Album
├── YYYY
└── YYYY
    ├── MM
    └── MM
        ├── DD
        └── DD
            └── yyyymmddhhss00-F1C11A22FAEE3B82F21B330E1B786A39.jpg
```

### Goal Folder Structure

```none
Switch
├── Game
└── Game
    └── Game YYYY-MM-DD HH:SS.jpg
```


## Prior Art

[**RenanGreca/Switch-Screenshots**](https://github.com/RenanGreca/Switch-Screenshots)


## Scripting Flavors

* [x] dotnet-script cbx
* [ ] powershell
* [ ] bash
* [ ] cmd
* [ ] npm / js
* [ ] python
* [ ] vbs


## Dot NET Script

```bash
# install scripting tool
dotnet tool install --global dotnet-script
# template new script
dotnet script init switch
# run script file
dotnet script switch.csx
```

### Platform

* [dotnet-script](https://github.com/filipw/dotnet-script)
* [C# scripts using dotnet-script](https://galdin.dev/blog/csharp-scripts-using-dotnet-script/)
* [C# and .NET Core scripting with the "dotnet-script" global tool](https://www.hanselman.com/blog/CAndNETCoreScriptingWithTheDotnetscriptGlobalTool.aspx)

### Standard Library

* [`Directory.GetFiles`](https://docs.microsoft.com/en-us/dotnet/api/system.io.directory.getfiles?view=netcore-3.1)
* [`Path.GetFileName`](https://docs.microsoft.com/en-us/dotnet/api/system.io.path.getfilename?view=netcore-3.1)
* [`File.Exists`](https://docs.microsoft.com/en-us/dotnet/api/system.io.file.exists?view=netcore-3.1)
* [`File.Move`](https://docs.microsoft.com/en-us/dotnet/api/system.io.file.move?view=netcore-3.1)
* [`File.SetCreationTime`](https://docs.microsoft.com/en-us/dotnet/api/system.io.file.setcreationtime?view=netcore-3.1)
* [`Regex.Match`](https://docs.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.match?view=netcore-3.1)
* [`Enumerable.Distinct`](https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.distinct?view=netcore-3.1)
* [`Enumerable.Except`](https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except?view=netcore-3.1)
* [`DateTime.ParseExact`](https://docs.microsoft.com/en-us/dotnet/api/system.datetime.parseexact?view=netcore-3.1)
* [C#6 Dictionary Indexed Initializers](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/object-and-collection-initializers#collection-initializers)
* [C#7 Tuples](https://docs.microsoft.com/en-us/dotnet/csharp/tuples)

