# BUM (Backup Manager)
Backup Manager - Windows Forms Application

### Purpose:
Export/Copy one or more source locations (folders) to a destination backup location

### Description:
This Windows Forms application is a personal project I use to backup my files. 

For the die-hards and purists, this project doesn't use Model/View, Model/Presenter, MVC, or MVVM frameworks or patterns.
I generally don't use those with WinForms apps and small projects. 
Small amounts of business logic, validation, etc. in a form is easy enough to test and maintain.
This is especially true with WinForms since controls don't support one-way or two-way binding.

### General Notes:
- BUM uses dynamic C# scripting to allow you to write sophisticated rules for excluding files or folders.

- Fixed/Known (non-dynamic) folders can be easily skipped by adding them to a list in the source definition.

- Program can be used interactively or automated via command-line flags.

- When running a backup job, a root folder named \\*JobID*, where JobID is a numeric number, is appended to the destination folder.
For example, a backup job with destination folder: *g:\backups* will have an actual output folder: *g:\backups\1*.
This keeps backup specific items all under a single, short root folder. All logs files will also be output to this location.

- Folders named *\BumTrash* and *\BumHistory* are appended to the root destination folder. 
These are where file overwrites and deletes reside if the options are configured in the backup job settings.
See section **Technical Notes** for specifics.

### File Change Detection:
BUM uses the following methods in this order:

1) Difference in file sizes
2) Difference in file attributes for creation time and last update time.

BUM doesn't use the archive attribute as it's not a reliable indicator due to other programs unsetting and setting this bit.
Also, file data (content) is never compared between two files as it drastically slows down backups.
If you have a program that changes file contents without updating the last update time,
it could be malware or stealth software and you have bigger problems than skipping a file for backup.


### Program Usage Quick Start:
1) Add a backup source definition. This is defined by a starting folder and any optional file or folder exclusions.
2) Add a backup job using the backup source definition created in step #1.
3) Define the destination folder at the root of the backup, along with handling options for file overwrites and deletes.
4) Run the job in simulation mode. If summary counts appear correct, run without simulation mode. 
5) For detailed troubleshooting, activate and run in Debug mode.


### Technical Notes:
    
- *Configuration/Settings* are stored in file: C:\Users\username\Documents\BUM\Settings.dat. 
This file is a JSON-formatted, serialized version of the configuration.
*Note:* Do NOT manually edit this file unless you know what you are doing. Use the UI to make all changes if possible.

- *Simulation Mode:* used to test your backups prior to production runs. It does not copy, create, move, or delete files. 
This can also be useful to run in combination with Debug Mode to review what folders/files are being skipped, scanned, etc.

- *Debug Mode:* used to get a detailed log of key decisions BUM is making while processing a backup job.

- *Delete Handling:* When a file is contained in the destination root folder and not processed in the backup, it's either an old file that has been deleted or possibly a renamed file.
Either way, eligible for delete-handling. When this happens, you have three choices:
  - *Leave alone:* files in the destination folder will be left alone. These old files will be accumulated over time making your backup more of a dirty, cumulative backup.
  - *Delete:* files in the destination folder will be deleted. This is the most aggressive choice.
  - *Move to an archive folder for deletes:* a nice choice that first creates a copy of the deleted file in a special folder (BumTrash), then removes the file. Each copy is stamped with a date-time so repeated conflicts are safely versioned. This choice keeps your destination folder clean and current.

- *Overwrite Handling:* When a source file has changed and a new version is copied to the backup folder, you can choose to maintain the history (versions) of the file. 
If this option is selected, each version of the changed file will be copied to a special folder named BumHistory. 
This is useful if you want to maintain revisions of documents, etc. 
If backup space is critical, the BumHistory folder can be periodically reviewed and large files that are no longer needed can be manually deleted.

- *Scripting:* uses the more relaxed C# rules in `Microsoft.CodeAnalysis.CSharp.Scripting` and `Microsoft.CodeAnalysis.Scripting`.
   
   Variables are considered global in scope, but you can define your own if you need to.

   Functions in the following Namespaces are allowed: `System`, `System.IO`, `OCSS.StringUtil` (for advanced string functions).

- *Exclusion Rules*: must return a boolean value to exlude a file/folder. 
You can either explicitly use a `return` statement or the final line must contain a value that evaluates to a boolean.

   *Example 1:* `return IsHidden || IsSystem || IsTemporary;`
   
   versus
   
   *Example 2:* `IsHidden || IsSystem || IsTemporary;`

   All rules are dynamically evaluated at run-time. Sample rules are included in the UI. Just pick one and click the Add button and the script code will be added to the editor.

- *Apply To Types* for Rules: Each rule has an "Apply to" type. Each rule will be evaluated according to whether a file or folder is being processed. 
When excluding a folder, you have to choose whether to skip just the folder or the folder and its subfolders. 
This gives you an easy way to skip a deeply nested folder hierarchy.

   *Note:* If you have multiple rules, the item will be skipped/excluded if any of the rules return a true value for that item type (file/folder).

- `GlobalScriptVars` contains simple properties that you can use in your rules.
    - `FullName` is a `string` containing the full file/folder path.
    - `Name` is a `string` containing the base file/folder name without any path.
    - `Ext` is a `string` containing the file extension. *Note:* When processing folders, this property is an empty string.
    - `Size` is a `long` value containing the size of the file or zero when processing folders.
    - `BackupStart` is a local `DateTime` value of the official backup start time.
    - `CreationTime` is a local `DateTime` value of the file/folder creation.
    - `LastWriteTime` is a local `DateTime` value of the last update for the file/folder.
    - `Attrib` is the `FileAttributes` value for the file/folder attributes.

- `GlobalScriptVars` also contains some common and convenient functions and properties you can use in your rules.
    - `IsHidden, IsSystem, IsReadOnly, IsArchive IsCompressed, IsEncrypted, IsTemporary, IsSparseFile` properties for file/folder attributes. Each is a boolean testing a single attribute.
    - `DaysBetweenCreateAndBackup` property returns a `double` value indicating the days between file/folder creation and start of backup.
    - `DaysBetweenLastUpdateAndBackup` property returns a `double` value indicating the days between file/folder last update and start of backup.
    - `FolderInChainIs(string name, bool anyCase)` function can be used to look for a named folder in the current hierarchy. The `AnyCase` variable is true when you want a case-insensitive search. Using this function, you can search for a single folder or more specific folder combinations. See advanced examples below.
    

### Advanced Rule Examples:
```
// Folder and subfolder rule
// Skip CSharp bin and obj folders and subfolders
// Exclude any folders named bin or obj (case insensitive) having "\source\cs\" (also case insensitive) anywhere in folder hierarchy.
// Note: \source\cs is specific to my machine; change as needed
string folderName = Name.ToLower();
return FolderInChainIs(@"source\cs", true) && (folderName == "obj" || folderName == "bin");
```

```
// Folder and subfolder rule
// Skip specific Unity project development folders and subfolders
// Note: \games\unity is specific to my machine; change as needed
string currName = Name.ToLower();
return FolderInChainIs(@"games\unity", true) && (currName == "library" || currName == "temp" || currName == "obj" || currName == "build" || currName == "builds" || currName == "logs" || currName == "memorycaptures");
```

```
// File rule
// Skip hidden or system or temp files, but not in .vs folders
return (IsHidden || IsSystem || IsTemporary) && (FolderInChainIs(".vs", true) == false);
```

```
// Folder and subfolder rule
// Skip folders and subfolders where folder hierarchy contains "\source\cs\"
// Note: \source\cs is specific to my machine; change as needed
return FolderInChainIs(@"source\cs", true) && Name.ToLower() == "testresults";
```

### Optional Command-line flags: 
Run BUM in automated mode. Program will be auto-closed when done processing.
Use dashes or forward slashes as flag prefixes. 

Example: BUM.exe /j123 /e /l /x

- `jJobID` - run Job ID (numeric value)
- `s` - run in **S**imulation mode; no files copied, deleted, etc.
- `d` - run in **D**ebug mode; this slows things down and can create a large log file for your review of step by step processing notes.
- `e` - auto-show **E**rror file after run (if any encountered)
- `l` - auto-show **L**og file after run.
- `z` - auto-show debug log after run (only if flag d is also present)
- `x` - e**x**port configuration/settings to output folder root (stamped with date-time)
