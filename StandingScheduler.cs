using Quartz.Impl;
using Quartz;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;

namespace StandingDeskPartner
{
    public class StandingScheduler
    {
        int MinutesStanding { get; set; }

        IList<DateTime> StandingTimes { get; set; }

        public StandingScheduler(int minutesStanding, IList<DateTime> standingTimes) 
        {
            MinutesStanding = minutesStanding;
            StandingTimes = standingTimes;
        }

        public async Task ScheduleStandingJobsAsync()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<StandJob>()
                .WithIdentity(StandJob.Key)
                .Build();
            await scheduler.AddJob(job, replace: true, storeNonDurableWhileAwaitingScheduling: true);

            IList<ITrigger> triggers = GenerateTriggersFromStandingTimes(StandingTimes, MinutesStanding);

            foreach (ITrigger trigger in triggers)
                await scheduler.ScheduleJob(trigger);
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
                    .Build();

                triggers.Add(trigger);
            }

            return triggers;
        }
    }
}
