using Quartz.Impl;
using Quartz;
using StandingDeskPartner.Settings;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Threading.Tasks;
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

        public MainWindow(ISettingsRepo repo)
        {
            InitializeComponent();

            SettingsRepo = repo;
        }

        protected override async void OnContentRendered(EventArgs e)
        {
            SettingsModel settings = await this.SettingsRepo.GetSettingsAsync();

            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<StandJob>()
                .WithIdentity(StandJob.Key)
                .Build();
            await scheduler.AddJob(job, replace: true, storeNonDurableWhileAwaitingScheduling: true);

            IList<ITrigger> triggers = GenerateTriggersFromStandingTimes(ComputeListOfStandUpTimes(settings), settings.MinutesStanding);

            foreach (ITrigger trigger in triggers)
                await scheduler.ScheduleJob(trigger);
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
            //TODO: use algo to determine the standing times

            return new List<DateTime>()
            {
                 new DateTime(2023, 10, 6, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second).AddSeconds(10),
                 new DateTime(2023, 10, 6, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second).AddSeconds(20),
                 new DateTime(2023, 10, 6, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second).AddSeconds(30),
            };
        }

        private string CronExpressionFromDateTime(DateTime standUpTime)
        {
            return $"{standUpTime.Second} {standUpTime.Minute} {standUpTime.Hour} ? * MON-FRI";
        }

        private IList<ITrigger> GenerateTriggersFromStandingTimes(IList<DateTime> standingTimes, int minutesStanding)
        {
            List<ITrigger> triggers = new List<ITrigger>();

            foreach (DateTime time in standingTimes)
            {
                string cronExpression = CronExpressionFromDateTime(time);
                Debug.WriteLine("Scheduling standup time for: " + cronExpression);

                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity(time.ToString() + "-trigger", "group")
                    .ForJob(StandJob.Key)
                    .WithCronSchedule(cronExpression)
                    .UsingJobData("minutesStanding", minutesStanding)
//                .StartAt(startTime)
//                .EndAt(endTime)
                    .Build();

                triggers.Add(trigger);
            }

            return triggers;
        }
    }
}
