using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace BL
{
    public class AppSettings
    {
        public long UpdateIntervalMs { get; set; }
        public DateTime LastUpdated { get; set; }

        public UpdateInterval UpdateInterval { get; set; }

        public Action OnUpdateIntervalChanged;

        public AppSettings() {
            Debug.WriteLine("AppSettings: Loaded from JSON");
        } //Needed for JSON Deserialization

        public AppSettings(UpdateInterval interval) {
            SetUpdateInterval(interval); 
            LastUpdated = DateTime.Now;
            Debug.WriteLine("APPSETTINGS: Saving appsettings");
            SettingsSerializer.Serialize(this);
        }

        public void SetUpdateInterval(UpdateInterval interval) {
            switch (interval)
            {
                case UpdateInterval.TenSeconds: //DEBUG
                    UpdateIntervalMs = (long)(TimeSpan.FromSeconds(10).TotalMilliseconds);
                    break;
                case UpdateInterval.OneDay:
                    UpdateIntervalMs = (long)(TimeSpan.FromDays(1).TotalMilliseconds);
                    break;
                case UpdateInterval.OneWeek:
                    UpdateIntervalMs = (long)(TimeSpan.FromDays(7).TotalMilliseconds);
                    break;
                case UpdateInterval.OneMonth:
                    UpdateIntervalMs = (long)(TimeSpan.FromDays(28).TotalMilliseconds);
                    break;
                default:
                    UpdateIntervalMs = -1;
                    break;
            };
            UpdateInterval = interval;
            Debug.WriteLine($"AppSettings: UpdateInterval: {interval.ToDisplayString()}");
            OnUpdateIntervalChanged?.Invoke();
        }
    }

    public enum UpdateInterval { 
        TenSeconds, //DEBUG
        OneDay,
        OneWeek,
        OneMonth
    }

    public static class UpdateIntervalExtensions {

        public static List<UpdateInterval> Values => new List<UpdateInterval>() {
            UpdateInterval.TenSeconds, //DEBUG
            UpdateInterval.OneDay,
            UpdateInterval.OneWeek,
            UpdateInterval.OneMonth
        };

        public static string ToDisplayString(this UpdateInterval interval) {
            return interval switch
            {
                UpdateInterval.TenSeconds => "Tio Sekunder",
                UpdateInterval.OneDay => "En Dag",
                UpdateInterval.OneWeek => "En Vecka",
                UpdateInterval.OneMonth => "En Månad",
                _ => "Unknown"
            };
        } 
    }
}
