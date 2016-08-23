// App.xaml.cs in Wlan10/Wlan10
// Created 2016/07/27
// ©Bertware, visit http://bertware.net
// 
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file,
// you can obtain one at http://mozilla.org/MPL/2.0/.

using System;
using System.IO;
using System.Reflection;
using System.Windows;

namespace Net.Bertware.Wlan10
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            AppDomain.CurrentDomain.AssemblyResolve += LoadDll;
        }

        public static Assembly LoadDll(object sender, ResolveEventArgs args)
        {
            //Load embedded DLLs

            String resourceName = "Net.Bertware.Wlan10.Resources." + new AssemblyName(args.Name).Name + ".dll";
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (stream == null || stream.Length < 1)
                {
                    return null;
                }
                Byte[] assemblyData = new Byte[stream.Length];
                stream.Read(assemblyData, 0, assemblyData.Length);

                return Assembly.Load(assemblyData);
            }
        }
    }
}