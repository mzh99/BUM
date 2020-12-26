using System.Text;
using OCSS.Util.CmdLine;

namespace BUM {

   public class CmdLineParmRet {
      public int JobID { get; set; }
      public bool RunInSimMode { get; set; }
      public bool DebugMode { get; set; }
      public bool ShowErrFile { get; set; }
      public bool ShowLogFile { get; set; }
      public bool ExportConfig { get; set; }
      public bool ShowDebugFile { get; set; }

      public CmdLineParmRet() {
         this.JobID = 0;
         this.RunInSimMode = false;
         this.DebugMode = false;
         this.ShowErrFile = false;
         this.ShowLogFile = false;
         this.ExportConfig = false;
         this.ShowDebugFile = false;
      }

   }

   public class CmdlineHelper {
      public static readonly string CmdLineParm_JobID = "j";
      public static readonly string CmdLineParm_SimMode = "s";
      public static readonly string CmdLineParm_DebugMode = "d";
      public static readonly string CmdLineParm_ShowErrorFile = "e";
      public static readonly string CmdLineParm_ShowLogFile = "l";
      public static readonly string CmdLineParm_ShowDebugFile = "z";
      public static readonly string CmdLineParm_ExportConfig = "x";

      public static readonly string TypicalFlagPrefix = @"\";

      // command-line errors reserved for: 1 to 20
      public static readonly int AppError_CmdLineParseFailed = 1;
      public static readonly int AppError_CmdLineJobIDNotNumeric = 2;
      public static readonly int AppError_CmdLineInvalidJobID = 3;

      public bool RunViaCommandLine { get; private set; }
      public bool AllParmsPresent { get; private set; }
      // Return package is only non-null when RunViaCommandLine=true AND AllParmsPresent=true AND basic validations returned success
      public CmdLineParmRet ReturnPackage { get; private set; }
      public CmdLine CmdLineParser { get; private set; }
      // we allow setting this value from outside callers; this allows further validations, for example
      public int CmdErrorCode { get; set; }

      public CmdlineHelper(string[] args) {
         ReturnPackage = null;
         CmdErrorCode = 0;
         CmdLineParser = null;
         AllParmsPresent = false;
         RunViaCommandLine = args.Length > 0;
         // if command line parms, process them
         if (RunViaCommandLine) {
            CmdFlag[] flags = {
               new CmdFlag(CmdLineParm_JobID, true),  // required parm
               new CmdFlag(CmdLineParm_SimMode, false),
               new CmdFlag(CmdLineParm_DebugMode, false),
               new CmdFlag(CmdLineParm_ShowLogFile, false),
               new CmdFlag(CmdLineParm_ShowErrorFile, false),
               new CmdFlag(CmdLineParm_ShowDebugFile, false),
               new CmdFlag(CmdLineParm_ExportConfig, false)
            };
            CmdLineParser = new CmdLine(flags);
            AllParmsPresent = CmdLineParser.ProcessCmdLine(args);
            if (AllParmsPresent) {
               // validate Job ID is numeric
               string jobStr = CmdLineParser.GetParm(CmdLineParm_JobID);
               int jobID;
               if (int.TryParse(jobStr, out jobID) == false) {
                  CmdErrorCode = AppError_CmdLineJobIDNotNumeric;
                  return;
               }
               bool debugMode = CmdLineParser.ParmExists(CmdLineParm_DebugMode);
               ReturnPackage = new CmdLineParmRet {
                  JobID = jobID,
                  RunInSimMode = CmdLineParser.ParmExists(CmdLineParm_SimMode),
                  DebugMode = debugMode,
                  ShowErrFile = CmdLineParser.ParmExists(CmdLineParm_ShowErrorFile),
                  ShowLogFile = CmdLineParser.ParmExists(CmdLineParm_ShowLogFile),
                  ExportConfig = CmdLineParser.ParmExists(CmdLineParm_ExportConfig),
                  ShowDebugFile = debugMode ? CmdLineParser.ParmExists(CmdLineParm_ShowDebugFile) : false
               };
            }
            else {
               CmdErrorCode = AppError_CmdLineParseFailed;
            }
         }

      }

      /// <summary>Given a set of UI parameters, return a valid command-line parameter list</summary>
      /// <param name="parms">a set of UI parameters</param>
      /// <param name="prefix">the preferred flag prefix for command-line parameters; typically a forward slash or dash</param>
      /// <returns>a string with the command-line parameter portion (the executable name is not included and can be added by the caller)</returns>
      /// <remarks>
      ///   A simple batch file might be: (the start /b parm is to prevent the quoted program name from being misinterpreted so we can pass parms to BUM)
      ///      @echo off
      ///      start /b "" /wait "\PathToBum\BUM.exe" /j99 /e /l
      ///      echo Return code was %errorlevel%
      ///      IF %ERRORLEVEL% EQU 20 (Echo Fatal processing error in BUM)
      ///      IF %ERRORLEVEL% EQU 1 (Echo Required command line parms missing)
      ///      IF %ERRORLEVEL% EQU 2 (Echo Job ID is not numeric)
      ///      IF %ERRORLEVEL% EQU 3 (Echo Invalid Job ID)
      ///      IF %ERRORLEVEL% EQU 0 (Echo Success)
      /// </remarks>
      public static string ConstructCommandLineString(CmdLineParmRet parms, string prefix = "-") {
         StringBuilder sb = new StringBuilder();
         // start with required flags
         sb.Append(prefix);
         sb.Append(CmdLineParm_JobID);
         sb.Append(parms.JobID.ToString());
         sb.Append(" ");
         // now add optional flags
         if (parms.DebugMode) {
            sb.Append(prefix);
            sb.Append(CmdLineParm_DebugMode);
            sb.Append(" ");
         }
         if (parms.ExportConfig) {
            sb.Append(prefix);
            sb.Append(CmdLineParm_ExportConfig);
            sb.Append(" ");
         }
         if (parms.RunInSimMode) {
            sb.Append(prefix);
            sb.Append(CmdLineParm_SimMode);
            sb.Append(" ");
         }
         if (parms.ShowDebugFile) {
            sb.Append(prefix);
            sb.Append(CmdLineParm_ShowDebugFile);
            sb.Append(" ");
         }
         if (parms.ShowErrFile) {
            sb.Append(prefix);
            sb.Append(CmdLineParm_ShowErrorFile);
            sb.Append(" ");
         }
         if (parms.ShowLogFile) {
            sb.Append(prefix);
            sb.Append(CmdLineParm_ShowLogFile);
            sb.Append(" ");
         }

         return sb.ToString().TrimEnd();
      }

   }

}
