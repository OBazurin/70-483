using System;
using System.Threading;
using System.Threading.Tasks;

namespace _70_483._1_ManageProgramFlow._1_ImplementMultiThreadAndAsync
{
    public static class Tasks
    {
        private static void DoWork()
        {
            Console.WriteLine("Job starting");
            Thread.Sleep(2000);
            Console.WriteLine("Job finished");
        }
        public static void TestCreateTask()
        {
            Task someTask = new Task(
                () => DoWork()
                );

            someTask.Start();
            Console.WriteLine("first");
            someTask.Wait();
            Console.WriteLine("second");

            Task anotherTask = Task.Run(
               () => DoWork()
               );

            anotherTask.Wait();
        }

        private static int DoAmazingCalcualations(int a, int b)
        {
            Console.WriteLine("Starting calculations");
            Thread.Sleep(2000);
            Console.WriteLine("Calculating finished");
            return a + b;
        }

        public static void TestReturnValueFromTask()
        {
            int a = 2;
            int b = 3;
            Task<int> task = Task.Run(()=> {
                return DoAmazingCalcualations(a,b);
            });
            Console.WriteLine(task.Result);
        }

        private static void DoNWait(int i)
        {
            Console.WriteLine($"Job #{i} Starting");
            Thread.Sleep(2000);
            Console.WriteLine($"Job #{i} Finished");
        }

        public static void TestWaitItAll()
        {
            Task[] tasks = new Task[10];
            for (int i = 0; i < 10; i++)
            {
                int taskNum = i; //need to have local copy of counter
                tasks[i] = Task.Run(() => DoNWait(taskNum));
            }

            Task.WaitAll(tasks);
            Console.WriteLine("All Jobs finished well");
        }
    }
}
