using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandingDeskPartner.Settings
{
    public class SettingsRepo: ISettingsRepo
    {
        public void SaveSettings(SettingsModel model)
        {

        }

        public SettingsModel GetSettings()
        {
            return new SettingsModel();
        }

    }
}
