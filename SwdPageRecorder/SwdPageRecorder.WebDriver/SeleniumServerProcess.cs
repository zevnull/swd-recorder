using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace SwdPageRecorder.WebDriver
{
    public static class SeleniumServerProcess
    {
        private static Process currentProcess;


        public static void Launch(string pathToStartupBatFile, string additionalArgs = "")
        {
            currentProcess = new Process();
            var p = currentProcess;
            p.StartInfo.FileName = pathToStartupBatFile;
            p.StartInfo.Arguments =  additionalArgs;
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.RedirectStandardOutput = false;
            p.Start();
            
            p.WaitForExit();
        }

        public static void Close()
        {
            currentProcess.Close();
        }

    }
}
