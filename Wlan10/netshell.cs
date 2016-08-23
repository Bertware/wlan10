// Netshell.cs in Wlan10/Wlan10
// Created 2016/07/27
// ©Bertware, visit http://bertware.net
// 
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file,
// you can obtain one at http://mozilla.org/MPL/2.0/.

using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Net.Bertware.Wlan10
{
    /// <summary>
    /// Netshell interactions
    /// </summary>
    static class Netshell
    {
        private const string CmdList = "wlan sh profiles";

        public static ObservableCollection<WlanNetwork> GetNetworks()
        {
            string output = NetshellCmd(CmdList);
            MatchCollection m = Regex.Matches(output, ":\\s*(.*?)\r\n", RegexOptions.Multiline);
            ObservableCollection<WlanNetwork> results = new ObservableCollection<WlanNetwork>();
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