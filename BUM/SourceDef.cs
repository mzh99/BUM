using System.Collections.Generic;

namespace BUM {

   public class SourceFolderExclude {
      public string Loc { get; set; }
      public bool AllSubs { get; set; }

      public SourceFolderExclude(): this(string.Empty, true) { }

      public SourceFolderExclude(string loc, bool allSubs) {
         this.Loc = loc;
         this.AllSubs = allSubs;
      }
   }

   public class SourceDef {
      public int ID { get; set; }
      public string StartingFolder { get; set; }
      public bool TraverseSubs { get; set; }
      public List<ExcludeRule> GlobalExcludeRules { get; set; }
      public List<SourceFolderExclude> KnownFolderExcludes { get; set; }

      public SourceDef() : this(0, string.Empty, true, null, null) { }

      public SourceDef(int id, string startFolder, bool doSubs, List<ExcludeRule> globalRules, List<SourceFolderExclude> folderExcludes) {
         this.ID = id;
         this.StartingFolder = startFolder;
         this.TraverseSubs = doSubs;
         GlobalExcludeRules = (globalRules == null) ? new List<ExcludeRule>() : globalRules.DeepCopyUsingJson();
         KnownFolderExcludes = (folderExcludes == null) ? new List<SourceFolderExclude>() : folderExcludes.DeepCopyUsingJson();
      }
   }

}
