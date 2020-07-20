using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace PLINQ
{
    class Plinq_Advanced
    {
        public void Run()
        {
            ParallelAdvanced();
        }

        void ParallelAdvanced()
        {
            var source = Enumerable.Range(0, 100);
            var sw = Stopwatch.StartNew();
            var anyTarget = source.Any(FindTarget);
            sw.Stop();
            Console.WriteLine($"普通查詢耗時\t{sw.Elapsed.TotalSeconds:F3}");

            sw.Restart();
            anyTarget = source.AsParallel().Any(FindTarget);
            sw.Stop();
            Console.WriteLine($"PLinq 查詢耗時\t{sw.Elapsed.TotalSeconds:F3}");
        }

        bool FindTarget(int value)
        {
            //模擬耗時運算
            SpinWait.SpinUntil(() => false, 10);
            return value == 500;
        }
    }
}
