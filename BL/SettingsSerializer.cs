using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BL
{
    public static class SettingsSerializer
    {
        public static string Filepath { get; } = "app_settings.json";

        public static void Serialize<AppSettings>(AppSettings settings) {
            string jsonContent = JsonSerializer.Serialize(settings);

            File.WriteAllText(Filepath, jsonContent);
        }

        public static AppSettings? Deserialize() {
            string jsonContent = "";
            if (File.Exists(Filepath))
            {
                jsonContent = File.ReadAllText(Filepath);
                AppSettings? appSettings = JsonSerializer.Deserialize<AppSettings>(jsonContent);
                return appSettings;
            }
            return null;
        }
    }
}
