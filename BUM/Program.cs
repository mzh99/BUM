using System;
using System.Windows.Forms;

namespace BUM {

   static class Program {

      public static CmdlineHelper cmdLineHelper;

      /// <summary>The main entry point for the application.</summary>
      /// <remarks>
      ///   MZH: this method signature was changed from Main() in order to process command-line parms.
      ///   See: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/main-and-command-args/command-line-arguments
      /// </remarks>
      [STAThread]
      static void Main(string[] args) {
         cmdLineHelper = new CmdlineHelper(args);
         // don't bother calling main form if a command-line error or a validation occured
         if (cmdLineHelper.CmdErrorCode == 0) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main_Frm());
         }
         // return error code to environment if detected; this allows testing in batch files for errorlevel
         if (cmdLineHelper.CmdErrorCode != 0)
            Environment.ExitCode = cmdLineHelper.CmdErrorCode;
      }
   }

}
