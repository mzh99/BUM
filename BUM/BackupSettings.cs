using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace BUM {

   public class BackupSettings {
      public static readonly int CurrentVersion = 1;
      public static readonly string ProgramID = "BUM";

      public static readonly string SettingsFilenameBase = "Settings";
      public static readonly string SettingsFilenameExt = ".dat";
      public static readonly string SettingsFilename = SettingsFilenameBase + SettingsFilenameExt;

      // these 3 items go together; be careful with changes
      public static readonly int MaxErrOptDefaultIndex = 3;
      public static readonly int MaxErrOptDefault = 1000;
      public static readonly int[] MaxErrOptList = new int[] { 10, 100, 500, 1000, 5000, 10_000, 25_000, 50_000, 100_000, 500_000, 1_000_000 };

      public int Version { get; set; }
      public int HighestJobID {  get; set; }
      public int HighestSourceID {  get; set; }
      public List<SourceDef> SourceDefs { get; set; }
      public List<BackupJob> Jobs { get; set; }

      public BackupSettings(): this(null, null) { }
      public BackupSettings(List<SourceDef> defs, List<BackupJob> jobs) {
         Version = CurrentVersion;
         HighestJobID = 0;
         HighestSourceID = 0;
         // make copies of the lists
         SourceDefs = defs == null ? new List<SourceDef>() : defs.DeepCopyUsingJson();
         Jobs = jobs == null ? new List<BackupJob>() : jobs.DeepCopyUsingJson();
      }

      public void Save() {
         SaveSettings(this);
      }

      public static void SaveSettings(BackupSettings settings) {
         SaveSettings(settings, GetUserSaveName());
      }

      public static void SaveSettings(BackupSettings settings, string fileName) {
         // ensure directory exists
         FileUtils.CreateDestFolderStructure(Path.GetDirectoryName(fileName));
         settings.Version = CurrentVersion;  // always update settings version
         var dat = JsonSerializer.SerializeToUtf8Bytes<BackupSettings>(settings);
         File.WriteAllBytes(fileName, dat);
      }

      public static BackupSettings GetSettings() {
         string filename = GetUserSaveName();
         if (File.Exists(filename) == false)
            return new BackupSettings();
         ReadOnlySpan<byte> dat = File.ReadAllBytes(filename);
         return JsonSerializer.Deserialize<BackupSettings>(dat);
      }

      public static string GetUserSaveFolder() {
         return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), ProgramID);
      }

      public static string GetUserSaveName() {
         return Path.Combine(GetUserSaveFolder(), SettingsFilename);
      }

      public static string GetBaseNameVersioned(string stamp) {
         return SettingsFilenameBase + "_" + stamp + SettingsFilenameExt;
      }

   }

}
