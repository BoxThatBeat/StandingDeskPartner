using StandingDeskPartner.Settings;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace StandingDeskPartner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            // Could create a DI framework here but since there is only one service, just create it and pass down
            SettingsRepo repo = new SettingsRepo();

            MainWindow window = new MainWindow(repo);
            window.Show();
        }

    }
}
