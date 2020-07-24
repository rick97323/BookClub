using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;

namespace PLINQ
{
    class Plinq_Advanced
    {
        public void Run()
        {
            ParallelFind();
        }

        //考慮選擇正確算法以提升效率
        void ParallelFind()
        {
            var comparer = new DelayComparerInt();
            var target = 888;

            var source = Enumerable.Range(0, 1000).ToArray();
            var sw = Stopwatch.StartNew();
            Func<int, bool> findTarget = value => comparer.Compare(value, target) == 0;


            var index = Array.BinarySearch(source, target, comparer);
            sw.Stop();
            Console.WriteLine($"BinarySearch 查詢耗時\t{sw.Elapsed.TotalSeconds:F3}");

            sw.Restart();
            var anyTarget = source.AsParallel().Any(findTarget);
            sw.Stop();
            Console.WriteLine($"PLinq 查詢耗時\t{sw.Elapsed.TotalSeconds:F3}");

            sw.Restart();
            anyTarget = source.Any(findTarget);
            sw.Stop();
            Console.WriteLine($"普通查詢耗時\t{sw.Elapsed.TotalSeconds:F3}");
        }
    }


    class DelayComparerInt : IComparer<int>
    {
        public int Compare([AllowNull] int x, [AllowNull] int y)
        {
            //模擬耗時運算
            Thread.Sleep(5);
            return x.CompareTo(y);
        }
    }
}
