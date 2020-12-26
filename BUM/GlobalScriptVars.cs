using System;
using System.IO;

namespace BUM {

   public class GlobalScriptVars {
      public readonly string FullName;
      public readonly string Name;
      public readonly string Ext;
      public readonly long Size;
      public readonly DateTime BackupStart;
      public readonly DateTime CreationTime;
      public readonly DateTime LastWriteTime;
      public readonly FileAttributes Attrib;

      public bool IsFileEntry { get; private set; }

      public bool IsHidden { get { return IsAttrBitSet(FileAttributes.Hidden); } }
      public bool IsSystem { get { return IsAttrBitSet(FileAttributes.System); } }
      public bool IsReadOnly { get { return IsAttrBitSet(FileAttributes.ReadOnly); } }
      public bool IsArchive { get { return IsAttrBitSet(FileAttributes.Archive); } }
      public bool IsCompressed { get { return IsAttrBitSet(FileAttributes.Compressed); } }
      public bool IsEncrypted { get { return IsAttrBitSet(FileAttributes.Encrypted); } }
      public bool IsTemporary { get { return IsAttrBitSet(FileAttributes.Temporary); } }
      public bool IsSparseFile { get { return IsAttrBitSet(FileAttributes.SparseFile); } }

      public double DaysBetweenCreateAndBackup { get { return (BackupStart - CreationTime).TotalDays; } }
      public double DaysBetweenLastUpdateAndBackup { get { return (BackupStart - LastWriteTime).TotalDays; } }

      public GlobalScriptVars(FileInfo fileInfo, DateTime backupStart) {
         this.BackupStart = backupStart;
         this.FullName = fileInfo.FullName;
         this.Name = fileInfo.Name;
         this.Ext = fileInfo.Extension;
         this.CreationTime = fileInfo.CreationTime;
         this.LastWriteTime = fileInfo.LastWriteTime;
         this.Size = fileInfo.Length;
         this.Attrib = fileInfo.Attributes;
         this.IsFileEntry = true;
      }

      public GlobalScriptVars(DirectoryInfo dirInfo, DateTime backupStart) {
         this.BackupStart = backupStart;
         this.FullName = dirInfo.FullName;
         this.Name = dirInfo.Name;
         this.Ext = string.Empty;   // not applicable to folders
         this.CreationTime = dirInfo.CreationTime;
         this.LastWriteTime = dirInfo.LastWriteTime;
         this.Size = 0; // not applicable to folders
         this.Attrib = dirInfo.Attributes;
         this.IsFileEntry = false;
      }

      public bool IsAttrBitSet(FileAttributes flag) {
         return (Attrib & flag) == flag;
      }

      public bool FolderInChainIs(string name, bool anyCase) {
         if (anyCase) {
            return FullName.ToLower().Contains(Path.DirectorySeparatorChar + name.ToLower() + Path.DirectorySeparatorChar);
         }
         else {
            return FullName.Contains(Path.DirectorySeparatorChar + name + Path.DirectorySeparatorChar);
         }
      }

   }

}