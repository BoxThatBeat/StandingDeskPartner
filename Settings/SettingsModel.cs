using System;
using System.Collections.Generic;

namespace StandingDeskPartner.Settings
{
    public class SettingsModel : ObservableObject
    {
        private DateTime _startTime;

        public DateTime StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        private DateTime _endTime;

        public DateTime EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }

        private int _minutesStanding;

        public int MinutesStanding
        {
            get { return _minutesStanding; }
            set { _minutesStanding = value; }
        }

        private int _minutesStandingInterval;

        public int MinutesStandingInterval
        {
            get { return _minutesStandingInterval; }
            set { _minutesStandingInterval = value; }
        }

        private List<SpecificTime> _specificTimes;

        public List<SpecificTime> SpecificTimes
        {
            get { return _specificTimes; }
            set { _specificTimes = value; }
        }

        public SettingsModel() {

            _specificTimes = new List<SpecificTime>();
        }

        public SettingsModel(DateTime startTime, DateTime endTime, int minutesStanding, int minutesStandingInterval, List<SpecificTime> specificTimes)
        {
            _startTime = startTime;
            _endTime = endTime;
            _minutesStanding = minutesStanding;
            _minutesStandingInterval = minutesStandingInterval;
            _specificTimes = specificTimes;
        }
    }
}
