using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandingDeskPartner.Settings
{
    public class SpecificTime
    {

        public DateTime Time { get; set; }

        public SpecificTime() { }

        public SpecificTime(DateTime time)
        {
            Time = time;
        }
    }
}
