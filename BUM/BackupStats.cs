using System;

namespace BUM {

   public class BackupStats {
      public static readonly string SpanCompactFmt = @"hh\:mm\:ss";

      public int JobID { get; set; }
      public bool RunInSimMode { get; set; }
      public bool RunInDebugMode { get; set; }
      public string DestPathRoot { get; set; }
      public string ErrLogFileName { get; set; }
      public string StatsLogFileName { get; set; }
      public string DebugFileName { get; set; }
      public string JobStamp { get; private set; }
      public DateTime DtTmStarted { get; set; }
      public DateTime DtTmEnded { get; set; }
      public int NumAdds { get; set; }
      public int NumUpdates { get; set; }
      public int NumDeletes { get; set; }
      public long FoldersScanned { get; set; }
      public long FilesScanned { get; set; }
      public long FoldersExcluded { get; set; }
      public long FilesExcluded { get; set; }
      public long VolumeScanned {  get; set; }
      public long VolumeAdds {  get; set; }
      public long VolumeUpdates {  get; set; }
      public long VolumeDeletes {  get; set; }
      public int NumErrors { get; set; }
      public int NumWarnings { get; set; }
      public int SimNumAdds { get; set; }
      public int SimNumUpdates { get; set; }
      public long SimVolumeAdds {  get; set; }
      public long SimVolumeUpdates {  get; set; }

      public TimeSpan JobSpan { get { return DtTmEnded - DtTmStarted; } }
      public string JobSpanAsStr { get { return JobSpan.ToString(SpanCompactFmt); } }

      public TimeSpan JobSpanElapsed { get { return DateTime.Now - DtTmStarted; } }
      public string JobSpanElapsedAsStr { get { return JobSpanElapsed.ToString(SpanCompactFmt); } }

      public BackupStats() {
         DateTime dtTm = DateTime.Now;
         string stamp = dtTm.ToString("yyyyMMddHHmmss");
         this.JobID = 0;
         this.RunInSimMode = false;
         this.RunInDebugMode = false;
         this.DestPathRoot = string.Empty;
         this.ErrLogFileName = string.Empty;
         this.StatsLogFileName = string.Empty;
         this.DebugFileName = string.Empty;
         this.JobStamp = stamp;
         this.DtTmStarted = dtTm;
         this.DtTmEnded = dtTm;
         this.NumAdds = 0;
         this.NumUpdates = 0;
         this.NumDeletes = 0;
         this.FoldersScanned = 0;
         this.FilesScanned = 0;
         this.FoldersExcluded = 0;
         this.FilesExcluded = 0;
         this.VolumeScanned = 0;
         this.VolumeAdds = 0;
         this.VolumeUpdates = 0;
         this.VolumeDeletes = 0;
         this.NumErrors = 0;
         this.NumWarnings = 0;
         this.SimNumAdds = 0;
         this.SimNumUpdates = 0;
         this.SimVolumeAdds = 0;
         this.SimVolumeUpdates = 0;
      }

   }

}

