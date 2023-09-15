using StandingDeskPartner.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public bool SettingsFinishedLoading = false;

        ISettingsRepo SettingsRepo { get; set; }


        public SettingsView(ISettingsRepo repo)
        {
            InitializeComponent();
            
            this.SettingsRepo = repo;

            //this.model = new SettingsModel();
            this.DataContext = new SettingsModel();
        }

        protected override async void OnContentRendered(EventArgs e)
        {
            this.DataContext = await this.SettingsRepo.GetSettingsAsync();
            this.SettingsFinishedLoading = true;
        }

        private void AddNewStandingTime_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Ensure that it is a valid date format

            if (!string.IsNullOrEmpty(StandingTimeTextBox.Text))
            {
                ((SettingsModel)this.DataContext)?.SpecificTimes.Add(new SpecificTime(DateTime.Parse(StandingTimeTextBox.Text)));
                StandingTimeTextBox.Clear();
            }
        }

        private async void SaveSettings_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Validate settings

            // Save settings
            await this.SettingsRepo.SaveSettingsAsync((SettingsModel)this.DataContext);

            // Close window
            this.Close();
        }

        private void CancelSettings_Click(object sender, RoutedEventArgs e)
        {
            // Close window without saving
            this.Close();
        }

        private void RemoveStandingTimeBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            SpecificTime time = (SpecificTime)btn.DataContext;
            ((SettingsModel)this.DataContext)?.SpecificTimes.Remove(time);
        }
    }
}
