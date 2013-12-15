using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;

namespace SwdPageRecorder.UI.CodeGeneration
{
    public class ExternalGenerator
    {
        public void SaveToJSonFile(WebElementDefinition[] definitions, string filePath)
        {
            string json = JsonConvert.SerializeObject(definitions, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public string RunExternalProcess(string fullCommandLine)
        {
            Process compiler = new Process();
            compiler.StartInfo.FileName = "cmd.exe";
            compiler.StartInfo.Arguments = "/C " + fullCommandLine;
            compiler.StartInfo.UseShellExecute = false;
            compiler.StartInfo.RedirectStandardOutput = true;
            compiler.Start();

            return compiler.StandardOutput.ReadToEnd();

            compiler.WaitForExit();
        }
    }
}
