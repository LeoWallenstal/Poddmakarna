using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class AppSettings
    {
        public long UpdateIntervalMs { get; private set; }
        public DateTime LastUpdated { get; set; }

        public string UpdateInterval { get; private set; }

        public AppSettings() {
            SetUpdateInterval("Tio Sekunder");
        }

        public void SetUpdateInterval(string interval) {
            switch(interval)
            {
                case "Tio Sekunder":
                    UpdateIntervalMs = 10000;
                    break;
                case "En Dag":
                    UpdateIntervalMs = 86400000;
                    break;
                case "En Vecka":
                    UpdateIntervalMs = 604800000;
                    break;
                case "En Månad":
                    UpdateIntervalMs = 2419200000;
                    break;
                default:
                    UpdateIntervalMs = -1;
                    break;
            };
            UpdateInterval = interval;
            Debug.WriteLine("AppSettings: UpdateInterval: " + UpdateInterval);
        }
    }
}
