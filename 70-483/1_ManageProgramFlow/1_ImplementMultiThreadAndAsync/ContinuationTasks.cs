using System.Threading;
using System.Threading.Tasks;

namespace _70_483._1_ManageProgramFlow._1_ImplementMultiThreadAndAsync
{
    public static class ContinuationTasks
    {
        private static void FirstTask()
        {
            Thread.Sleep(2000);
            System.Console.WriteLine("Hello ");
        }
        private static void SecondTask()
        {
            Thread.Sleep(2000);
            System.Console.WriteLine("World");
        }

        public static void TestCreateContinuationTasks()
        {
            Task task = Task.Run(() => FirstTask());
            task.ContinueWith((prevTask) => SecondTask(), TaskContinuationOptions.OnlyOnRanToCompletion);
            task.Wait(); //will wait for 1st task
            System.Console.ReadKey(); //will finish imidiatly (not waiting for second)
        }

        private static void ChildJob(object state)
        {
            System.Console.WriteLine($"Job {state} starting");
            Thread.Sleep(2000);
            System.Console.WriteLine($"Job {state} finished");
        }

        public static void TestChildTasks()
        {
            Task parent = Task.Factory.StartNew(() =>
                {
                    System.Console.WriteLine("Parent starting");
                    for (int i = 0; i < 10; i++)
                    {
                        int taskNum = i;
                        Task.Factory.StartNew(
                            (x) => ChildJob(x), 
                            state: taskNum, 
                            TaskCreationOptions.AttachedToParent);
                    }
                });
            parent.Wait();
            System.Console.WriteLine("Parent finished");
        }
    }
}
