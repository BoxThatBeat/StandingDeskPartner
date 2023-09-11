using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandingDeskPartner.Settings
{
    public class SettingsModel
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int MinutesStanding { get; set; }
        public int MinutesStandingInterval { get; set; }
        public List<SpecificTime> SpecificTimes { get; set; }

        public SettingsModel() {
        
            SpecificTimes = new List<SpecificTime>();
        }

        public SettingsModel(DateTime startTime, DateTime endTime, int minutesStanding, int minutesStandingInterval, List<SpecificTime> specificTimes)
        {
            StartTime = startTime;
            EndTime = endTime;
            MinutesStanding = minutesStanding;
            MinutesStandingInterval = minutesStandingInterval;
            SpecificTimes = specificTimes;
        }
    }
}
