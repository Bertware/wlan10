// WlanNetwork.cs in Wlan10/Wlan10
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

using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace Net.Bertware.Wlan10
{
	/// <summary>
	///     A network obtained from netshell output.
	/// </summary>
	internal class WlanNetwork
	{
		private const string CmdDetail = "wlan sh profiles \"<name>\"";

		/// <summary>
		///     Set network priority
		/// </summary>
		private const string CmdUpdate =
			"wlan set profileorder name=\"<name>\" interface=\"<interface>\" priority=<priority>";

		/// <summary>
		///     autoswitch can be yes or no
		/// </summary>
		private const string CmdSetAutoswitch =
			"wlan set profileparameter name=\"<name>\" SSIDname=\"<ssid>\" autoSwitch=<autoswitch>";

		/// <summary>
		///     Connectionmode can be auto or manual
		/// </summary>
		private const string CmdSetAutoConnect =
			"wlan set profileparameter name=\"<name>\" SSIDname=\"<ssid>\" ConnectionMode=<autoconnect>";

		/// <summary>
		///     nonbroadcast can be yes or no
		/// </summary>
		private const string CmdSetNonBroadcast =
			"wlan set profileparameter name=\"<name>\" SSIDname=\"<ssid>\" nonBroadcast=<nonbroadcast>";

		/// <summary>
		///     Set the pre-shared key or passphrase
		/// </summary>
		private const string CmdSetKey =
			"wlan set profileparameter name=\"<name>\" SSIDname=\"<ssid>\" keyMaterial=<key> ";

		/// <summary>
		///     Delete this profile
		/// </summary>
		private const string CmdDelete = "wlan delete profile \"<name>\"";

		private readonly bool _allUsers;

		public bool AllUsers
		{
			get { return _allUsers; }
		}

		private readonly string _interface;
		private readonly int _version;
		private readonly string _profileType;
		private readonly string _name;
		private bool _autoconnect;
		private bool _connectWithoutBroadcast;
		private bool _autoswitch;
		private readonly bool _macRandomization;
		private readonly int _SSIDCount;
		private readonly string _SSIDName;
		private readonly string _networkType;
		private readonly string _radioType;
		private readonly string _authentication;
		private readonly string _cipher;
		private readonly bool _securityKey;
		private readonly string _cost;
		private readonly bool _congested;
		private readonly bool _approachingLimit;
		private readonly bool _overLimit;
		private readonly bool _roaming;
		private readonly string _costSource;

		public int Version
		{
			get { return _version; }
		}

		public string ProfileType
		{
			get { return _profileType; }
		}

		public string Name
		{
			get { return _name; }
		}

		public bool Autoconnect
		{
			get { return _autoconnect; }
			set
			{
				_autoconnect = value;
				string param = value ? "auto" : "manual";
				Netshell.NetshellCmd(PrepareCommand(CmdSetAutoConnect.Replace("<autoconnect>", param)));
			}
		}

		public bool ConnectWithoutBroadcast
		{
			get { return _connectWithoutBroadcast; }
			set
			{
				_connectWithoutBroadcast = value;
				string param = value ? "yes" : "no";
				Netshell.NetshellCmd(PrepareCommand(CmdSetNonBroadcast.Replace("<nonbroadcast>", param)));
			}
		}

		public bool Autoswitch
		{
			get { return _autoswitch; }
			set
			{
				_autoswitch = value;
				string param = value ? "yes" : "no";
				Netshell.NetshellCmd(PrepareCommand(CmdSetAutoswitch.Replace("<autoswitch>", param)));
			}
		}

		public bool MacRandomization
		{
			get { return _macRandomization; }
		}

		public int SsidCount
		{
			get { return _SSIDCount; }
		}

		public string SsidName
		{
			get { return _SSIDName; }
		}

		public string NetworkType
		{
			get { return _networkType; }
		}

		public string RadioType
		{
			get { return _radioType; }
		}

		public string Authentication
		{
			get { return _authentication; }
		}

		public string Cipher
		{
			get { return _cipher; }
		}

		public bool SecurityKey
		{
			get { return _securityKey; }
		}

		public string Cost
		{
			get { return _cost; }
		}

		public bool Congested
		{
			get { return _congested; }
		}

		public bool ApproachingLimit
		{
			get { return _approachingLimit; }
		}

		public bool OverLimit
		{
			get { return _overLimit; }
		}

		public bool Roaming
		{
			get { return _roaming; }
		}

		public string CostSource
		{
			get { return _costSource; }
		}

		/// <summary>
		///     Get a full network based on the network name. Requires netshell access.
		/// </summary>
		/// <param name="name"></param>
		public WlanNetwork(string name)
		{
			if (String.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException("Name cannot be empty");
			}

			_name = name;
			string output = Netshell.NetshellCmd(PrepareCommand(CmdDetail));
			try
			{
				_interface = parseInterface(output);

				_allUsers = (String.Equals(getFieldFromOutput(output, "Applied"), "All User Profile",
					StringComparison.Ordinal));
				_version = Int32.Parse(getFieldFromOutput(output, "Version"));
				_profileType = getFieldFromOutput(output, "Type");
				_name = getFieldFromOutput(output, "Name");
				_connectWithoutBroadcast = (!String.Equals(getFieldFromOutput(output, "Network broadcast"),
					"Connect only if this network is broadcasting", StringComparison.Ordinal));
				_autoconnect = (String.Equals(getFieldFromOutput(output, "Connection mode"), "Connect automatically",
					StringComparison.Ordinal));
				_autoswitch = (String.Equals(getFieldFromOutput(output, "AutoSwitch"),
					"Switch to more preferred network if possible", StringComparison.Ordinal));
				_macRandomization = (String.Equals(getFieldFromOutput(output, "MAC Randomization"), "Enabled",
					StringComparison.Ordinal));
				_SSIDCount = Int32.Parse(getFieldFromOutput(output, "Number of SSIDs"));
				_SSIDName = getFieldFromOutput(output, "SSID name");
				_networkType = getFieldFromOutput(output, "Network type");
				_radioType = getFieldFromOutput(output, "Radio type");

				_authentication = getFieldFromOutput(output, "Authentication");
				_cipher = getFieldFromOutput(output, "Cipher");
				_securityKey = (String.Equals(getFieldFromOutput(output, "Security key"), "Present",
					StringComparison.Ordinal));

				_cost = getFieldFromOutput(output, "Cost");

				_congested = (getFieldFromOutput(output, "Congested") == "Yes");
				_approachingLimit = (getFieldFromOutput(output, "Approaching Data Limit") == "Yes");
				_overLimit = (getFieldFromOutput(output, "Over Data Limit") == "Yes");
				_roaming = (getFieldFromOutput(output, "Roaming") == "Yes");
				_costSource = getFieldFromOutput(output, "Cost Source");
			}
			catch (Exception e)
			{
				MessageBox.Show(
					"The application encountered an unexpected error while parsing a network. You can continue using the app, but some profiles may be missing. Please report this error to the developer (\"http://github.com/Bertware/Wlan10/Issues\")\r\n" +
					"Error details:\r\n" +
					e.Message + "\r\n"
					+ e.StackTrace + "\r\n\r\n" +
					output, "Unexpected error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		/// <summary>
		///     Parse the network interface
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		private string parseInterface(string output)
		{
			return Regex.Match(output, "on interface (.*?):").Groups[1].Value;
		}

		/// <summary>
		///     Parse a field with information
		/// </summary>
		/// <param name="output"></param>
		/// <param name="field"></param>
		/// <returns></returns>
		private string getFieldFromOutput(string output, string field)
		{
			Match m = Regex.Match(output, field + "\\s*:\\s*(.+?)\\s*$",
				RegexOptions.Multiline | RegexOptions.IgnoreCase);
			return m.Groups[1].Value.Trim(' ', '"');
		}

		public override string ToString()
		{
			return Name + "(" + Authentication + " " + NetworkType + ")";
		}

		private string PrepareCommand(string command)
		{
			return command.Replace("<name>", Name)
				.Replace("<ssid>", SsidName)
				.Replace("<interface>", _interface);
		}

		/// <summary>
		///     Set the network key (WEP/WPA/WPA2) for this network
		/// </summary>
		/// <param name="key"></param>
		public void SetKey(string key)
		{
			Netshell.NetshellCmd(PrepareCommand(CmdSetKey).Replace("<key>", key));
		}

		/// <summary>
		///     Set the priority for this network
		/// </summary>
		/// <param name="priority"></param>
		public void SetPriority(int priority)
		{
			Netshell.NetshellCmd(PrepareCommand(CmdUpdate).Replace("<priority>", priority.ToString()));
		}

		/// <summary>
		///     Forget this network
		/// </summary>
		public void ForgetNetwork()
		{
			Netshell.NetshellCmd(PrepareCommand(CmdDelete));
		}
	}
}