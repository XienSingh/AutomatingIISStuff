using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace AutomateIIS.Logic
{
    public class LaunchCLI
    {
       public static void LaunchCommandLineApp(int SiteID,string[] bindings)
        {
            const string SSLType = "N";
            var SiteIDforSSL = SiteID;

            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = true;
            startInfo.FileName = @"E:\win-acme-1.9.12.2\letsencrypt-win-simple\bin\Debug\letsencrypt.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Verb = "runas";
            startInfo.Arguments = SSLType + " " + SiteIDforSSL.ToString() + " "+ string.Join(",",bindings);

            try
            {
                //Make sure you are elevated
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch(Exception e)
            {
                // Log error.
            }
        }
    }
}