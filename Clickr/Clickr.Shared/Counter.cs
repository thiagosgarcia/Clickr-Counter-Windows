using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;

namespace Clickr
{
    public class Counter
    {
        public Counter(int init)
        {
            Count = init;
            ResetPrediction();
            ConfigureSaveTimer();
        }

        private void ConfigureSaveTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += SaveCount;
        }

        public Persistence Persistence { get { return Persistence.GetInstance(); } }
        private static Counter _counter = null;
        public static Counter GetInstance(int init = 0)
        {
            return _counter = init > 0 ? new Counter(init) : _counter ?? new Counter(init);
        }
        public int Count { get; set; }
        public int MaxPace { get; set; }
        public DateTime SaveTime { get; set; }
        public string CountString { get { return Count.ToString("00000"); } }

        public int Click()
        {
            ++Count;

            if (Count == 1 || LastClickShowPredictions != ShowPredictions)
            {
                StartPrediction(Count);
            }

            LastClickShowPredictions = ShowPredictions;
            return Count = Count > 99999 ? 0 : Count;
        }

        private void SaveCount(object sender, object e)
        {
            if (SaveTime.AddSeconds(3) > DateTime.Now)
                return;
            Persistence.SaveLast(Count.ToString());
            SaveTime = DateTime.Now;
        }

        private void StartPrediction(int count)
        {
            StartTime = DateTime.Now;
            StartCount = count;
        }

        public int Reset()
        {
            ResetPrediction();
            Persistence.SaveLast("0");
            return Count = 0;
        }

        private DateTime StartTime;
        private int StartCount;
        public bool LastClickShowPredictions { get; set; }
        public bool ShowPredictions { get; set; }
        public string Predict()
        {
            if (Count == 0 || !ShowPredictions)
                ResetPrediction();
            if (Count - StartCount < 2)
                return "Pace: ...";

            var now = DateTime.Now;
            var actual = Count;

            var diff = now - StartTime;
            var pace = (actual - StartCount) / (diff.TotalSeconds / 60);

            if (MaxPace < pace)
                MaxPace = (int)pace;

            return string.Concat("Pace: ", pace.ToString("#0"), " clicks/min");
        }

        private void ResetPrediction()
        {
            StartTime = DateTime.Now;
            StartCount = Count;
        }
    }
}
