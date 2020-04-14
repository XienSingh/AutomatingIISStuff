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

			// Use ProcessStartInfo class
			ProcessStartInfo startInfo = new ProcessStartInfo();
			startInfo.CreateNoWindow = false;
			startInfo.UseShellExecute = true;
			startInfo.FileName = @"C:\SSL\WACS\wacs.exe";
			startInfo.WindowStyle = ProcessWindowStyle.Hidden;
			startInfo.Verb = "runas";
			startInfo.Arguments = $" --target manual --host {string.Join(",", bindings)} --siteid {SiteID}  --installation iis  --installationsiteid {SiteID}";

			try
			{
				using (Process exeProcess = Process.Start(startInfo))
				{
					exeProcess.WaitForExit();
					switch (exeProcess.ExitCode)
					{
						case 0:
							return "Cert Added Successfully to : " + string.Join(",", bindings); ;

						case -1:
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