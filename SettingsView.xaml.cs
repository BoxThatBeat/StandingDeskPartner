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
using System.Windows.Shapes;

namespace StandingDeskPartner
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : Window
    {
        ISettingsRepo SettingsRepo { get; set; }

        //SettingsModel model { get; set; }

        public SettingsView(ISettingsRepo repo)
        {
            InitializeComponent();
            
            this.SettingsRepo = repo;

            //this.model = new SettingsModel();
            this.DataContext = new SettingsModel();
        }

        protected override async void OnContentRendered(EventArgs e)
        {
            SettingsModel defaultModel = new SettingsModel(
                new DateTime(2021, 1, 1, 9, 0, 0),
                new DateTime(2021, 1, 1, 17, 0, 0),
                60,
                60,
                new List<DateTime>()
                {
                    new DateTime(2021, 1, 1, 10, 0, 0),
                    new DateTime(2021, 1, 1, 14, 0, 0)
                });

            //TODO remove
            await this.SettingsRepo.SaveSettingsAsync(defaultModel);

            this.DataContext = await this.SettingsRepo.GetSettingsAsync();
            Console.WriteLine("Settings loaded");
        }

        private void AddNewStandingTime_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Ensure that it is a valid date format
            if (!string.IsNullOrEmpty(StandingTimeTextBox.Text))
            {
                ListOfStandingTimes.Items.Add(" - " + StandingTimeTextBox.Text);
                StandingTimeTextBox.Clear();
            }
        }

        private void SaveSettings_Click(object sender, RoutedEventArgs e)
        {
            // Validate settings

            // Save to repo


            // Close window
            this.Close();
        }

        private void CancelSettings_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
