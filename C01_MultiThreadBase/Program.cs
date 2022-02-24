using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreadCS
{
    class Program
    {
        static void Main01(string[] args)
        {
            PrintNumbers();
            Thread t = new Thread(DelayPrintNumbers);
            t.Start();
            //线程同步，主线程阻塞，等待t线程完成
            t.Join();
            Console.WriteLine("Thread Completed!");
            Console.ReadKey();

            List<int> a = new List<int>();
            a.Sort(Sort1);
        }

        static int Sort1(int v1, int v2)
        {
            return v1 - v2;
        }

        static void Main02(string[] args)
        {
            Thread t = new Thread(DelayPrintNumbers);
            t.Start();

            Thread.Sleep(TimeSpan.FromSeconds(5));
            /*终止线程，注入Thread AortException方法，导致线程被终结这非常危险，因为该异常可
            以在任何时刻发生并可能彻底摧毁应用程序,另外，使用该技术也不一定总能终止线程
            标线程可以通过处理该异常并调用Thread.ResetAort方法来拒绝被终止因此并不推荐使用Abort方法来关闭线程*/
            t.Abort();
            Console.WriteLine("Thread Abort");

            Thread t2 = new Thread(PrintNumbers);
            t2.Start();
            PrintNumbers();
            Console.ReadLine();
        }

        static void Main03(string[] args)
        {
            Console.WriteLine("Main_Start.. .");
            Thread t = new Thread(DelayPrintNumbersWithState);
            Thread t2 = new Thread(DoNothing);
            Console.WriteLine(t.ThreadState.ToString());
            t2.Start();
            t.Start();
            for (int i = 1; i < 30; i++)
                Console.WriteLine(t.ThreadState.ToString() + "_" + i);
            Thread.Sleep(TimeSpan.FromSeconds(6));
            t.Abort();
            Console.WriteLine("A thread has been aborted");
            Console.WriteLine("t1:" + t.ThreadState.ToString());
            Console.WriteLine("t2:" + t2.ThreadState.ToString());
            Console.ReadLine();
        }

        static void Main(string[] args)
        {

        }

        static void PrintNumbers()
        {
            Console.WriteLine("Start...");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
            }
        }

        static void DelayPrintNumbers()
        {
            Console.WriteLine("Start...");

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                Console.WriteLine(i);
            }
        }

        static void DelayPrintNumbersWithState()
        {
            Console.WriteLine("T1_Start...");
            Console.WriteLine("State1:" + Thread.CurrentThread.ThreadState.ToString());
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                Console.WriteLine(i);
            }
        }

        static void DoNothing()
        {
            Thread.Sleep(TimeSpan.FromSeconds(2));
        }

        static void RunRhreads()
        {

        }

        class ThreadSample
        {
            private bool is_Stop = false;
            public void Stop()
            {
                is_Stop = true;
            }
            public void CountNumbers()
            {
                long counter = 0;
                while (!is_Stop)
                {
                    counter++;
                }
                Console.WriteLine("{O} with {1.11} priority " +
                "has a count = {2.13}",
                Thread.CurrentThread.Name,
                Thread.CurrentThread.Priority,
                counter.ToString("NO"));
            }
        }
    }
}
