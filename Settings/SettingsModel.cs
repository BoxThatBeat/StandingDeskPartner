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
        public DateTime StartTime;
        public DateTime EndTime;
        public int MinutesStanding;
        public int MinutesInterval;
        public ObservableCollection<DateTime> SpecificTimes;
    }
}
