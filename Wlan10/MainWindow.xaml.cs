using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Wlan10
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ObservableCollection<WlanNetwork> networks = Netshell.GetNetworks();
        public MainWindow()
        {
            InitializeComponent();
            lvNetworks.ItemsSource = networks;
        }

    }
}