using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using OCSS.StringUtil;

namespace BUM {

   public class CompiledExclusionRule {

      public RuleAppliesTo AppliesTo { get; private set; }
      public Script<bool> CompiledCode { get; private set; }

      public string CompileErrors { get { return compileErrors; } }
      public bool HasErrors { get { return compileErrors != string.Empty; } }

      private readonly string compileErrors;

      public CompiledExclusionRule(ExcludeRule rule) : this(rule.AppliesTo, rule.RuleCode) { }

      public CompiledExclusionRule(RuleAppliesTo appliesTo, string scriptCode) {
         this.AppliesTo = appliesTo;
         this.CompiledCode = ScriptRunner.CompileScript(scriptCode, out compileErrors);
      }

   }


   public static class ScriptRunner {

      public static bool ExecuteCode(Script<bool> script, GlobalScriptVars glob, out string err) {
         err = string.Empty;
         bool ret = false;
         try {
            script.RunAsync(glob).ContinueWith(s => ret = s.Result.ReturnValue).Wait();
            return ret;
         }
         catch (CompilationErrorException e) {
            err = e.Message;
            return ret;
         }
      }

      public static Script<bool> CompileScript(string code, out string err) {
         err = string.Empty;
         ScriptOptions opts = ScriptOptions.Default.AddReferences(typeof(MHString).Assembly).AddImports("System", "System.IO", "OCSS.StringUtil");
         try {
            var script = CSharpScript.Create<bool>(code, opts, typeof(GlobalScriptVars));
            var diags = script.Compile();
            if (diags.Length > 0) {
               foreach (var diag in diags) {
                  err += diags[0].ToString() + "\r\n";
               }
               return null;
            }
            return script;
         }
         catch (CompilationErrorException e) {
            err = e.Message;
            return null;
         }
      }

   }

}
