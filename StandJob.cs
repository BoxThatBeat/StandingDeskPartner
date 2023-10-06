using Quartz;
using System;
using System.Threading.Tasks;

namespace StandingDeskPartner
{
    class StandJob : IJob
    {
        public static readonly JobKey Key = new JobKey("standing-job", "group");

        public async Task Execute(IJobExecutionContext context)
        {
            // Send notif to user to stand up
            var minutesStanding = context.MergedJobDataMap.GetInt("minutesStanding");
            await Console.Out.WriteLineAsync("Stand up!");

            // Start a timer for configured amount of time to then send a notification to sit down
            //Task.Delay(10000).Wait(); // Wait 10 seconds

            //Console.Out.WriteLineAsync("Sit Down!");
        }
    }
}
