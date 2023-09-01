using StandingDeskPartner.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Windows.UI;

namespace StandingDeskPartner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ISettingsRepo Repo { get; set; }

        public MainWindow(ISettingsRepo repo)
        {
            InitializeComponent();

            Repo = repo;
        }

        private void OpenSettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            SettingsView settingsView = new SettingsView(this.Repo);
            settingsView.Owner = this;
            settingsView.ShowDialog(); // Don't continue until settings window has closed
        }
    }
}
