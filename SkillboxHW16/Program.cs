using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace SkillboxHW16
{
    class Program
    {
      
        static void Main(string[] args)
        {
            Test(10, 20);
            Test(1000, 2000);
            Test(10000, 20000);
            Test(100000, 200000);
            Test(1000000, 2000000);
            Test(10000000, 20000000);
            
            Console.ReadLine();
        }

        static void Test(int start, int end)
        {
            var stopWatch = Stopwatch.StartNew();
            Console.WriteLine($"CounterCalc {CounterCalc(start, end)} time {stopWatch.ElapsedMilliseconds}");

            stopWatch.Restart();
            Console.WriteLine($"ParallelRun {ParallelRun(start, end)} time {stopWatch.ElapsedMilliseconds}");
        }

        static int ParallelRun(int start, int end)
        {
            List<Task> tasks = new List<Task>();
            int counter = 0;
            
            int blockSize = (end - start) / Environment.ProcessorCount;
            for (int i = start; i < end; i+= blockSize)
            {
                var startSegment = i;
                var endSegment = Math.Min(startSegment + blockSize, end);
                tasks.Add(Task.Run(() => Interlocked.Add(ref counter, CounterCalc(startSegment, endSegment))));
            }
            Task.WaitAll(tasks.ToArray());
            return counter;
        }

        static int CounterCalc(int start, int end)
        {
            int counter = 0;

            for (int i = start; i < end; i++)
            {
                if (IsMultipleOfLastnumber(i)) counter++;
            }
            return counter;
        }

        static bool IsMultipleOfLastnumber(int number)
        {
            return CalculeteSumOfNumbers(number) % GetLastNumer(number) == 0;
        }

        static int CalculeteSumOfNumbers(int number)
        {
            int n = number;
            int sum = 0;

            do
            {
                sum += n % 10;
                n = n / 10;

            } while (n > 0);
            return sum;
        }

        static int GetLastNumer(int number)
        {
            int n = number;
            int count = 0;
            bool condition = true;

            do
            {
                condition = n % 10 != 0;
                if (condition)
                {
                    n++;
                    count++;
                }

            } while (condition);

            return 10 - count;
        }
    }
}
