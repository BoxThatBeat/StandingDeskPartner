using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandingDeskPartner.Settings
{
    public interface ISettingsRepo
    {
        public Task SaveSettingsAsync(SettingsModel model);

        public Task<SettingsModel> GetSettingsAsync();
    }
}
