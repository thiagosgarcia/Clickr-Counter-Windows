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
        }

        private static Counter _counter = null;
        public static Counter GetInstance()
        {
            return _counter = _counter ?? new Counter();
        }
        public int Count { get; set; }

        public int Click()
        {
            return Count = Count > 99999 ? 0 : ++Count;
        }

        public int Reset()
        {
            return Count = 0;
        }
    }
}
