using System;

namespace StandingDeskPartner.Settings
{
    public class SpecificTime
    {
        private DateTime _time;

        public DateTime Time
        {
            get { return _time; }
            set { _time = value; }
        }

        public SpecificTime() { }

        public SpecificTime(DateTime time)
        {
            _time = time;
        }
    }
}
