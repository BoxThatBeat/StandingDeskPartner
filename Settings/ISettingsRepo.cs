using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandingDeskPartner.Settings
{
    interface ISettingsRepo
    {
        public void SaveSettings(SettingsModel model);

        public SettingsModel GetSettings();
    }
}
