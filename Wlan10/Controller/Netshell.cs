// Netshell.cs in Wlan10/Wlan10
// Created 2016/08/27
// Copyright ©2016 Bertware (bertware.net)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using Net.Bertware.Wlan10.Model;

namespace Net.Bertware.Wlan10.Controller
{
	/// <summary>
	///     Netshell interactions
	/// </summary>
	internal static class Netshell
	{
		private const string CmdList = "wlan sh profiles";

		public static ObservableCollection<WlanNetwork> GetNetworks()
		{
			string output = NetshellCmd(CmdList);
			MatchCollection m = Regex.Matches(output, "User Profile\\s+:\\s*(.*?)$", RegexOptions.Multiline);
			ObservableCollection<WlanNetwork> results = new ObservableCollection<WlanNetwork>();

			for (int i = 0; i < m.Count; i++)
			{
				string name = m[i].Groups[1].Value.Trim('\r');
				if (!string.IsNullOrWhiteSpace(name))
				{
						results.Add(new WlanNetwork(name));
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
			// The default codepage (and console output) in your Windows is almost certainly not Unicode.
			// If you're alive in 2017 you likely see network SSIDs with lovely international characters,
			// emoji and other good stuff. So we need to do something. Alas, you can't set console output
			// encoding of a Windows app in debug mode according to Jon Skeet:
			//    https://stackoverflow.com/a/18580114/83089
			// Ideally we'd use the following, but if you try to run it in VisualStudio, it'll throw:
			//    System.Console.OutputEncoding = System.Text.Encoding.UTF8;
			// If we use the PowerShell chcp command to set the console's output with our process object,
			// the codepage will reset before our next command. The solution? Chain commands with cmd.exe.
			// Nasty, but it works.
			p.StartInfo.FileName = "cmd.exe";
			p.StartInfo.Arguments = "/C chcp 65001 && netsh " + cmd;
			p.StartInfo.StandardOutputEncoding = Encoding.UTF8;
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