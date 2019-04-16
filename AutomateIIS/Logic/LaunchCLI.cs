using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace AutomateIIS.Logic
{
    public class LaunchCLI
    {
        public static string LaunchCommandLineApp(int SiteID, string[] bindings)
        {
            const string SSLType = "N";
            var SiteIDforSSL = SiteID;

            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = true;
            startInfo.FileName = @"C:\SSL\letsencrypt.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Verb = "runas";
            startInfo.Arguments = SSLType + " " + SiteIDforSSL.ToString() + " " + string.Join(",", bindings);

            try
            {
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                    switch (exeProcess.ExitCode)
                    {
                        case 0:
                            return "No action taken";

                        case 1:
                            return "SSL Cert Added Successfully to : "+ string.Join(",", bindings); ;

                        case 2:
                            return "Exeption in adding Cert, Please contact the internal SSL Issuer";

                        default:
                            return "Somethng happend but I dont know what...";

                    }
                }

            }
            catch (Exception e)
            {
                //TODO: Log error.
                return "Failed with errors";
            }
        }
    }
}