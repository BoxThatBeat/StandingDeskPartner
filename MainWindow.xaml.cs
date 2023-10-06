using Quartz.Impl;
using Quartz;
using StandingDeskPartner.Settings;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Diagnostics;

namespace StandingDeskPartner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        public UserState CurrentState { get; set; } = UserState.OutOfOffice;

        ISettingsRepo SettingsRepo { get; set; }

        SettingsModel Settings { get; set; }

        StandingScheduler Scheduler { get; set; }

        public MainWindow(ISettingsRepo repo)
        {
            InitializeComponent();

            SettingsRepo = repo;
            Settings = new SettingsModel();

            //TODO: get standing times from settings.ComputedStandingTimes
            var standingTimes = new List<DateTime>()
            {
                 new DateTime(2023, 10, 6, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second).AddSeconds(10),
                 new DateTime(2023, 10, 6, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second).AddSeconds(20),
                 new DateTime(2023, 10, 6, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second).AddSeconds(30),
            };

            Scheduler = new StandingScheduler(Settings.MinutesStanding, standingTimes);
        }

        protected override async void OnContentRendered(EventArgs e)
        {
            this.Settings = await this.SettingsRepo.GetSettingsAsync();

            await this.Scheduler.ScheduleStandingJobsAsync();
        }

        private void OpenSettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            SettingsView settingsView = new SettingsView(this.SettingsRepo);
            settingsView.Owner = this; // Don't continue until settings window has closed
            settingsView.DataContext = this.Settings;
            settingsView.ShowDialog();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove(); // Allow app to be dragged by grabbing any control
        }
    }
}
