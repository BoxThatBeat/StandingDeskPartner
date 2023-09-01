using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Windows.Storage;

namespace StandingDeskPartner.Settings
{
    public class SettingsRepo: ISettingsRepo
    {
        private readonly string APP_FOLDER = "StandingDeskReminderApp";
        private readonly string SETTINGS_FILENAME = "StandingDeskSettings.json";
        private string SettingsPath { get; set; }

        public SettingsRepo()
        {
            this.SettingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), this.APP_FOLDER);
        }

        public async Task SaveSettingsAsync(SettingsModel model)
        {
            Directory.CreateDirectory(this.SettingsPath);
            using FileStream stream = File.Create(Path.Combine(this.SettingsPath, this.SETTINGS_FILENAME));
            await JsonSerializer.SerializeAsync(stream, model);
            await stream.DisposeAsync(); 
        }

        public async Task<SettingsModel> GetSettingsAsync()
        {
            await using FileStream stream = File.OpenRead(Path.Combine(this.SettingsPath, this.SETTINGS_FILENAME));
            SettingsModel? model = await JsonSerializer.DeserializeAsync<SettingsModel>(stream);

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return model;
        }

    }
}
