using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Wlan10
{
    class Netshell
    {
        private const string CmdList = "wlan sh profiles";

        public static List<WlanNetwork> GetNetworks()
        {
            string output = NetshellCmd(CmdList);
            MatchCollection m = Regex.Matches(output, ":\\s*(.*?)\r\n",RegexOptions.Multiline);
            List<WlanNetwork> results = new List<WlanNetwork>();
            for (int i = 1; i < m.Count; i++)
            {
                if (!string.IsNullOrWhiteSpace(m[i].Value))
                {
                    results.Add(new WlanNetwork(m[i].Groups[1].Value));
                }
            }
            return results;
        }

        public static string NetshellCmd(string cmd)
        {
            // Start the child process.
            Process p = new Process();
            // Redirect the output stream of the child process.
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = "netsh";
            p.StartInfo.Arguments = cmd;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            // Do not wait for the child process to exit before
            // reading to the end of its redirected stream.
            // p.WaitForExit();
            // Read the output stream first and then wait.
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            return output;
        }
    }
}