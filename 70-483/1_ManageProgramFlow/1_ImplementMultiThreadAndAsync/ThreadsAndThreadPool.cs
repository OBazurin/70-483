using System;
using System.Threading;

namespace _70_483._1_ManageProgramFlow._1_ImplementMultiThreadAndAsync
{
    public static class ThreadsAndThreadPool
    {
        private static void ThreadHello()
        {
            System.Console.WriteLine("Hello from the thread");
            Thread.Sleep(2000);
        }

        public static void TestCreatingThreads()
        {
            //ThreadStart ts = new ThreadStart(ThreadHello);
            //Thread th = new Thread(ts);  --- OLD WAY
            Thread thread = new Thread(ThreadHello); //new way
            thread.Start();
        }

        public static void TestThreadsAndlambda()
        {
            Thread thread = new Thread(() =>  //no need to have additional method here
            {
                System.Console.WriteLine("Hello from thread");
                Thread.Sleep(2000);
            });
            thread.Start();
        }

        private static void WorkOnData(object data)
        {
            System.Console.WriteLine($"Working with {data} data");
            Thread.Sleep(2000);
            System.Console.WriteLine($"Finished with {data} data");
        }

        public static void TestPassingDataIntoThread()
        {
            //1
            //Thread thread = new Thread(WorkOnData);
            //thread.Start(99);
            //2
            ParameterizedThreadStart ps = new ParameterizedThreadStart(WorkOnData);
            Thread thread = new Thread(ps);
            thread.Start(99);
            //3
            Thread tr = new Thread((data) => WorkOnData(data));
            tr.Start(88);
        }

        public static void TestAbortThread()
        {
            /*
            Thread tickThread = new Thread(() =>
            {
                while (true)
                {
                    System.Console.WriteLine("Tick");
                    Thread.Sleep(1000);
                }
            });
            tickThread.Start();
            System.Console.WriteLine("Stop that f*cking ticking!");
            System.Console.ReadKey();
            tickThread.Abort(); // not so good to user with .net core */
            bool isTicking = true;
            Thread tickThread = new Thread(() =>
            {
                while (isTicking)
                {
                    System.Console.WriteLine("Tick");
                    Thread.Sleep(1000);
                }
            });
            tickThread.Start();
            System.Console.WriteLine("Stop that f*cking ticking!");
            System.Console.ReadKey();
            isTicking = false;
        }

        public static void TestThreadSyncUsingJoin()
        {
            Thread threadToWaitFor = new Thread(() =>
            {
                System.Console.WriteLine("Thread wait for joining starting");
                Thread.Sleep(2000);
                System.Console.WriteLine("Thread wait for joining finished");
            });

            threadToWaitFor.Start();
            System.Console.WriteLine("Joining");
            threadToWaitFor.Join();
        }

        private static ThreadLocal<Random> randomGenerator =
            new ThreadLocal<Random>(() =>
            {
                return new Random(2);
            });

        public static void TestDataStorageAndThreadLocal()
        {
            Thread t1 = new Thread(() => 
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine($"t1: {randomGenerator.Value.Next(100)}");
                    Thread.Sleep(1000);
                }
            });
            Thread t2 = new Thread(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine($"t2: {randomGenerator.Value.Next(100)}");
                    Thread.Sleep(1000);
                }
            });
            t1.Start();
            t2.Start();
        }

        private static void ShowThreadInfo(Thread thread)
        {
            Console.WriteLine($"Name : {thread.Name}");
            Console.WriteLine($"Culture : {thread.CurrentCulture}");
            Console.WriteLine($"Priority : {thread.Priority}");
            Console.WriteLine($"Context : {thread.ExecutionContext}");
            Console.WriteLine($"Is background? : {thread.IsBackground}");
            Console.WriteLine($"Is pool? : {thread.IsThreadPoolThread}");
        }

        public static void TestThreadExecutionContext()
        {
            Thread.CurrentThread.Name = "TestThreadExecutionContext";
            ShowThreadInfo(Thread.CurrentThread);
        }

        public static void ThreadPools()
        {
            for (int i = 0; i < 50; i++)
            {
                int stateNumber = i;
                ThreadPool.QueueUserWorkItem(state => WorkOnData(stateNumber));
            }
        }
    }
}
