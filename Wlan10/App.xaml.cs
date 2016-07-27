using System;
using System.IO;
using System.Reflection;
using System.Windows;

namespace Net.Bertware.Wlan10
{
    /// <summary>
    /// Interaction logic for App.xaml
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
