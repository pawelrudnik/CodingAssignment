# CodingAssignment

## Prerequisites

Application requires .net core framework 2.1 installed on system.

## How to build and run

To build and test application run commands below in application main folder. Please replace MSBuild with full path to MSBuild.exe.

### Restore packages
```
MSBuild -t:restore
```

### Build
```
MSBuild
```

### Run
```
cd CodingAssignment\
dotnet run "path to log file" (for example: dotnet run "C:\logfile.txt")
```
