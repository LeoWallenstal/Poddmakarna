using System.Diagnostics;

namespace BL
{
    public class PodUpdater
    {
        private Timer _updateTimer;
        public AppSettings AppSettings { get; }
        public PodUpdater(AppSettings appSettings) { 
            AppSettings = appSettings;
            CheckForUpdate();

            appSettings.OnUpdateIntervalChanged += CheckForUpdate;
        }

        public Action OnUpdatePodcasts;

        public void Start(TimeSpan duration) {

            _updateTimer ??= new Timer(UpdateTimerCallback, null, Timeout.Infinite, Timeout.Infinite);
            _updateTimer.Change((int)(duration.TotalMilliseconds), Timeout.Infinite);
        }

        private void UpdateTimerCallback(object? state) {
            //EVENT - OnNotifyPodUpdate 
            Debug.WriteLine("Callback: Pods are updating!");
            OnUpdatePodcasts?.Invoke();
            AppSettings.LastUpdated = DateTime.Now;
            SettingsSerializer.Serialize(AppSettings);


            //Starta timer med intervallets längd
            TimeSpan interval = TimeSpan.FromMilliseconds(AppSettings.UpdateIntervalMs);
            Start(interval);
        }

        private void CheckForUpdate() {
            TimeSpan elapsed = DateTime.Now - AppSettings.LastUpdated;
            TimeSpan interval = TimeSpan.FromMilliseconds(AppSettings.UpdateIntervalMs);

            if (elapsed >= interval || elapsed < TimeSpan.Zero)
            {
                Debug.WriteLine($"[PodUpdater]CheckForUpdate(): Update interval: {interval}\nElapsed: {elapsed}");
                //EVENT - OnNotifyPodUpdate
                Debug.WriteLine("[PodUpdater]CheckForUpdate(): Pods are checking for update!");
                OnUpdatePodcasts?.Invoke();
                AppSettings.LastUpdated = DateTime.Now;
                SettingsSerializer.Serialize(AppSettings);
                Start(interval);
            }
            else {
                Start(interval - elapsed);
                Debug.WriteLine("[PodUpdater]CheckForUpdate(): Not time for update yet!");
            }
        }
    }
}
