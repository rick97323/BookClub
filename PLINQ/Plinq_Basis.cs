using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static PLINQ.Program;

namespace PLINQ
{
    class Plinq_Basic
    {
        public void Run()
        {
            var source = Enumerable.Range(0, 10);
            NormalWhere(source);
            ParallelWhere(source);
            ParallelPrintThread2();
        }


        //按照順序存取
        void NormalWhere(IEnumerable<int> source)
        {
            var result = source.Where(o => o % 2 == 1);
            PrintEnumerable(result);
            WriteSpace();
        }

        //平行化後輸出的序列順序會打亂
        void ParallelWhere(IEnumerable<int> source)
        {
            ConcurrentQueue<string> printData = new ConcurrentQueue<string>();
            var temp = source.Select(value =>
            {
                var thread = Thread.CurrentThread;
                //運行時會以執行緒池安排工作
                printData.Enqueue($"Take {value,2} With Thread {thread.ManagedThreadId,2} Pool={thread.IsThreadPoolThread}");
                return value;
            });

            var result = temp.AsParallel().WithDegreeOfParallelism(10).Where(value =>
            {
                return value % 2 == 1;
            }).ToArray();

            Console.WriteLine("集合順序\n");
            PrintEnumerable(result);

            Console.WriteLine("\n取得順序\n");
            while (printData.TryDequeue(out var line))
            {
                Console.WriteLine(line);
            }
            WriteSpace();
        }

        //試著更改 splitCount 的參數，該參數會限制平行化作業的最大數量
        //嘗試註解Thread.Sleep 及調高Source數量，觀察執行續數量及工作分配數量的變化
        //https://docs.microsoft.com/zh-tw/dotnet/api/system.linq.parallelenumerable.withdegreeofparallelism?view=netcore-3.1
        void ParallelPrintThread2()
        {
            var splitCount = 5;
            ConcurrentDictionary<int, int> TakeThreadTemp = new ConcurrentDictionary<int, int>();
            var source = Enumerable.Range(0, 100);
            var temp = source.Select(value =>
            {
                var thread = Thread.CurrentThread.ManagedThreadId;
                if (TakeThreadTemp.TryGetValue(thread, out var count))
                {
                    TakeThreadTemp.TryUpdate(thread, count + 1, count);
                }
                else
                {
                    TakeThreadTemp.TryAdd(thread, 1);
                }
                return value;
            });

            temp.AsParallel().WithDegreeOfParallelism(splitCount).Where(value =>
            {
                Thread.Sleep(5);
                return value % 2 == 1;
            }).ToArray();   //立即執行

            CustomPrint(TakeThreadTemp, item => $"Thread {item.Key,3} {item.Value,3}");
            Console.WriteLine($"Thread Count {TakeThreadTemp.Count}");

            WriteSpace();
        }
    }
}
