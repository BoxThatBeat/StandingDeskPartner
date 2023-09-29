using Quartz.Impl;
using Quartz;
using StandingDeskPartner.Settings;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Threading.Tasks;

namespace StandingDeskPartner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        public UserState CurrentState { get; set; } = UserState.OutOfOffice;

        ISettingsRepo SettingsRepo { get; set; }

        public MainWindow(ISettingsRepo repo)
        {
            InitializeComponent();

            SettingsRepo = repo;
        }

        protected override async void OnContentRendered(EventArgs e)
        {
            SettingsModel settings = await this.SettingsRepo.GetSettingsAsync();

            string cronExpression = ConvertTimesToCronExpression(ComputeListOfStandUpTimes(settings));
            await CreateStandUpScheduledNotifications(cronExpression, settings.StartTime, settings.EndTime, settings.MinutesStanding);
        }

        private void OpenSettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            SettingsView settingsView = new SettingsView(this.SettingsRepo);
            settingsView.Owner = this; // Don't continue until settings window has closed
            settingsView.ShowDialog(); 
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove(); // Allow app to be dragged by grabbing any control
        }

        private List<DateTime> ComputeListOfStandUpTimes(SettingsModel settings)
        {
            return new List<DateTime>();
        }

        private string ConvertTimesToCronExpression(List<DateTime> standUpTimes)
        {
            return "1 * * ? * MON-FRI";
        }

        private async Task<DateTimeOffset> CreateStandUpScheduledNotifications(string cronExpression, DateTime startTime, DateTime endTime, int minutesStanding)
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();
            IJobDetail job = JobBuilder.Create<StandJob>().Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("StandJob", "group")
                .WithCronSchedule(cronExpression)
                .UsingJobData("minutesStanding", minutesStanding)
//                .StartAt(startTime)
//                .EndAt(endTime)
                .Build();

            return await scheduler.ScheduleJob(job, trigger);
        }
    }
}
