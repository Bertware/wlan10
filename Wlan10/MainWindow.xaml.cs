using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

        private void btnSaveOrder_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < networks.Count; i++)
            {
                networks[i].setPriority(i+1);
            }
            MessageBox.Show("The new network order was applied succesfully!");
        }

        private void btnDonate_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You will now be redirected to the donate page");
            Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=K4856LBVQZ25L");
        }

        private void btnCredits_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Idea and creation by Bertware.\r\nwww.bertware.net www.bertmarcelis.be\r\n\r\nThis program uses the GongSolutions.Wpf.DragDrop library made by Punker79\r\nhttps://github.com/punker76/gong-wpf-dragdrop");
        }

    }
}