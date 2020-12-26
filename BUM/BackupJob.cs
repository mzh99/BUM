using System.Collections.Generic;

namespace BUM {

   public class BackupJob {
      public int ID { get; set; }
      public List<int> SourceIDs { get; set; }
      public string Destination { get; set; }
      public JobOpts Options { get; set; }
      public int MaxErrors { get; set; }

      public BackupJob() {
         this.ID = 0;
         this.SourceIDs = new List<int>();
         this.Destination = string.Empty;
         this.Options = new JobOpts();
         this.MaxErrors = BackupSettings.MaxErrOptDefault;
      }

   }

}
