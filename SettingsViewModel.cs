using StandingDeskPartner.Settings;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandingDeskPartner
{

    class SettingsViewModel
    {
        public ISettingsRepo SettingsRepo; 

        public SettingsModel Model;

        public SettingsViewModel(ISettingsRepo repo)
        {
            this.SettingsRepo = repo;

            Model = this.SettingsRepo.GetSettings();
        }

        public SaveSettingsForm()
        {
            this.SettingsRepo.SaveSettings(this.Model);
        }


    }
}
