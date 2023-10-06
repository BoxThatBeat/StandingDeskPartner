using StandingDeskPartner.Settings;
using System;
using System.Windows;
using System.Windows.Controls;

namespace StandingDeskPartner
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : Window
    {

        ISettingsRepo SettingsRepo { get; set; }


        public SettingsView(ISettingsRepo repo)
        {
            InitializeComponent();
            this.SettingsRepo = repo;
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

        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
