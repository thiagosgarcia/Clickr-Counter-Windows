using System;
using System.Collections.Generic;
using System.Text;

namespace Clickr
{
    public class Counter
    {
        public Counter()
        {
            Count = 0;
            ResetPrediction();
        }

        private static Counter _counter = null;
        public static Counter GetInstance()
        {
            return _counter = _counter ?? new Counter();
        }
        public int Count { get; set; }

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

        private void StartPrediction(int count)
        {
            StartTime = DateTime.Now;
            StartCount = count;
        }

        public int Reset()
        {
            ResetPrediction();
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

            return string.Concat("Pace: ", pace.ToString("#0"), " clicks/min");
        }

        private void ResetPrediction()
        {
            StartTime = DateTime.Now;
            StartCount = Count;
        }
    }
}
