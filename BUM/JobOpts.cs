namespace BUM {

   public enum DeleteDetectHandling { LeaveAloneOnDest, DeleteFromDest, MoveToDeleteArchive }

   public class JobOpts {
      public bool MaintainHistoryFolder { get; set; }
      public DeleteDetectHandling DeleteHandling { get; set; }

      public JobOpts() {
         MaintainHistoryFolder = true;
         DeleteHandling = DeleteDetectHandling.MoveToDeleteArchive;
      }
   }

}
