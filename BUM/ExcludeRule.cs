using System.Linq;

namespace BUM {

   public enum RuleAppliesTo { Files, FolderAndSubfolders, ParentFolderOnly }

   public class ExcludeRule {

      public RuleAppliesTo AppliesTo { get; set; }
      public string Desc { get; set; }
      public string RuleCode { get; set; }

      public ExcludeRule(): this(RuleAppliesTo.Files, string.Empty, string.Empty) { }

      public ExcludeRule(RuleAppliesTo appliesTo, string desc, string ruleCode) {
         this.Desc = desc;
         this.AppliesTo = appliesTo;
         this.RuleCode = ruleCode;
      }

   }

   public static class CommonRules {

      public static readonly ExcludeRule[] AllRules = {
         new ExcludeRule(RuleAppliesTo.Files, "Skip files with attributes: System, Hidden, or Temporary", "return IsHidden || IsSystem || IsTemporary;"),
         new ExcludeRule(RuleAppliesTo.Files, "Skip files with attributes: System or Hidden", "return IsHidden || IsSystem;"),
         new ExcludeRule(RuleAppliesTo.Files, "Skip files with extension .xxx", "return Ext.ToLower() == \".xxx\";"),
         new ExcludeRule(RuleAppliesTo.Files, "Skip files with base file name (minus extension) xxx", "return Path.GetFileNameWithoutExtension(Name) == \"xxx\";"),
         new ExcludeRule(RuleAppliesTo.Files, "Skip files larger than 100MB", "return Size > 100_000_000;"),
         new ExcludeRule(RuleAppliesTo.Files, "Skip files with size of zero bytes", "return Size == 0;"),
         new ExcludeRule(RuleAppliesTo.Files, "Skip files with names starting with xxx", "return Name.StartsWith(\"xxx\");"),
         new ExcludeRule(RuleAppliesTo.Files, "Skip files with names ending with xxx", "return Name.EndsWith(\"xxx\");"),
         new ExcludeRule(RuleAppliesTo.Files, "Skip files with names containing xxx", "return Name.Contains(\"xxx\");"),
         new ExcludeRule(RuleAppliesTo.Files, "Skip where creation date is > 180 days old", "return DaysBetweenCreateAndBackup > 180;"),

         new ExcludeRule(RuleAppliesTo.FolderAndSubfolders, "Skip folder (and subs) with attributes: System or Hidden", "return IsHidden || IsSystem;"),
         new ExcludeRule(RuleAppliesTo.FolderAndSubfolders, "Skip folder (and subs) with names starting with xxx", "return Name.StartsWith(\"xxx\");"),
         new ExcludeRule(RuleAppliesTo.FolderAndSubfolders, "Skip folder (and subs) with names ending with xxx", "return Name.EndsWith(\"xxx\");"),
         new ExcludeRule(RuleAppliesTo.FolderAndSubfolders, "Skip folder (and subs) with names containing xxx", "return Name.Contains(\"xxx\");"),
         new ExcludeRule(RuleAppliesTo.FolderAndSubfolders, "Skip folder (and subs) where creation date is > 180 days old", "return DaysBetweenCreateAndBackup > 180;"),
         new ExcludeRule(RuleAppliesTo.FolderAndSubfolders, "Skip folder (and subs) where last update date is > 90 days old", "return DaysBetweenLastUpdateAndBackup > 90;"),
         new ExcludeRule(RuleAppliesTo.FolderAndSubfolders, "Skip folder (and subs) having a case-sensitive parent folder named Obj", "return FolderInChainIs(\"Obj\", false);"),
         new ExcludeRule(RuleAppliesTo.FolderAndSubfolders, "Skip folder (and subs) having a case-insensitive parent folder", "return FolderInChainIs(\"obj\", true);"),
         new ExcludeRule(RuleAppliesTo.FolderAndSubfolders, "Skip folder (and subs) named xxx having parent folder zzz", "return FolderInChainIs(\"zzz\", true) && Name.ToLower() == \"xxx\";"),
         new ExcludeRule(RuleAppliesTo.FolderAndSubfolders, "(Advanced) Skip multiple folders (and their subs) under a parent folder", "string currName = Name.ToLower();\r\nreturn FolderInChainIs(@\"games\\unity\", true) && (currName == \"library\" || currName == \"temp\" || currName == \"obj\" || currName == \"build\" || currName == \"builds\" || currName == \"logs\" || currName == \"memorycaptures\");"),

         new ExcludeRule(RuleAppliesTo.ParentFolderOnly, "Skip folder with attributes: System or Hidden", "return IsHidden || IsSystem;"),
         new ExcludeRule(RuleAppliesTo.ParentFolderOnly, "Skip folder with names starting with xxx", "return Name.StartsWith(\"xxx\");"),
         new ExcludeRule(RuleAppliesTo.ParentFolderOnly, "Skip folder with names ending with xxx", "return Name.EndsWith(\"xxx\");"),
         new ExcludeRule(RuleAppliesTo.ParentFolderOnly, "Skip folder with names containing xxx", "return Name.Contains(\"xxx\");"),
         new ExcludeRule(RuleAppliesTo.ParentFolderOnly, "Skip folder where creation date is > 180 days old", "return DaysBetweenCreateAndBackup > 180;"),
         new ExcludeRule(RuleAppliesTo.ParentFolderOnly, "Skip folder where last update date is > 90 days old", "return DaysBetweenLastUpdateAndBackup > 90;"),
         new ExcludeRule(RuleAppliesTo.ParentFolderOnly, "Skip folder having a case-sensitive parent folder named Obj", "return FolderInChainIs(\"Obj\", false);"),
         new ExcludeRule(RuleAppliesTo.ParentFolderOnly, "Skip folder having a case-insensitive parent folder", "return FolderInChainIs(\"obj\", true);"),
         new ExcludeRule(RuleAppliesTo.ParentFolderOnly, "Skip folder named xxx having parent folder zzz", "return FolderInChainIs(\"zzz\", true) && Name.ToLower() == \"xxx\";")
      };

      public static readonly ExcludeRule[] RulesFiles = AllRules.Where(r => r.AppliesTo == RuleAppliesTo.Files).ToArray();
      public static readonly ExcludeRule[] RulesFoldersAndSubs = AllRules.Where(r => r.AppliesTo == RuleAppliesTo.FolderAndSubfolders).ToArray();
      public static readonly ExcludeRule[] RulesParentFolder = AllRules.Where(r => r.AppliesTo == RuleAppliesTo.ParentFolderOnly).ToArray();

      public static readonly ExcludeRule DefaultAttrRuleForFiles = AllRules[0];
   }

}
