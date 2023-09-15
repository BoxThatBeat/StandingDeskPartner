using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StandingDeskPartner.Settings
{
    public class SettingsModel : ObservableObject
    {
        private DateTime _startTime;

        public DateTime StartTime
        {
            get { return _startTime; }
            set 
            { 
                _startTime = value;
                OnPropertyChanged();
            }
        }

        private DateTime _endTime;

        public DateTime EndTime
        {
            get { return _endTime; }
            set 
            { 
                _endTime = value;
                OnPropertyChanged();
            }
        }

        private int _minutesStanding;

        public int MinutesStanding
        {
            get { return _minutesStanding; }
            set 
            { 
                _minutesStanding = value;
                OnPropertyChanged();
            }
        }

        private int _minutesStandingInterval;

        public int MinutesStandingInterval
        {
            get { return _minutesStandingInterval; }
            set 
            { 
                _minutesStandingInterval = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<SpecificTime> _specificTimes;

        public ObservableCollection<SpecificTime> SpecificTimes
        {
            get { return _specificTimes; }
            set 
            { 
                _specificTimes = value;
                OnPropertyChanged();
            }
        }

        public SettingsModel() {

            _specificTimes = new ObservableCollection<SpecificTime>();
        }

        public SettingsModel(DateTime startTime, DateTime endTime, int minutesStanding, int minutesStandingInterval, ObservableCollection<SpecificTime> specificTimes)
        {
            _startTime = startTime;
            _endTime = endTime;
            _minutesStanding = minutesStanding;
            _minutesStandingInterval = minutesStandingInterval;
            _specificTimes = specificTimes;
        }
    }
}
