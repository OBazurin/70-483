using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace _70_483._1_ManageProgramFlow._1_ImplementMultiThreadAndAsync
{
    public class TaskParallerLibrary
    {
        /// <summary>
        /// Method for testing parallel execution of two methods
        /// </summary>
        private static void ParallelTaskOne()
        {
            Console.WriteLine("Start Task 1");
            Thread.Sleep(2000);
            Console.WriteLine("End Task 1");
        }

        /// <summary>
        /// Method for testing parallel execution of two methods
        /// </summary>
        private static void ParallelTaskTwo()
        {
            Console.WriteLine("Start Task 2");
            Thread.Sleep(2000);
            Console.WriteLine("End Task 2");
        }

        /// <summary>
        /// Method for testing parallel execution of Foreach
        /// </summary>
        private static void WorkForItem(Object obj)
        {
            //Console.WriteLine($"Start working with {obj} item");
            Thread.Sleep(100);
            //Console.WriteLine($"End working with {obj} item");
        }

        public static void TestParallelInvoke()
        {
            Console.WriteLine("\t\t###### Threading! ######");
            Console.WriteLine("\t## Parallel Invoke ##");
            Parallel.Invoke(
                () => ParallelTaskOne(),
                () => ParallelTaskTwo()
                );
            Console.WriteLine("\t## End of invoke ##");
        }

        public static void TestParallelForEach()
        {
            var items = Enumerable.Range(0, 100);
            Parallel.ForEach(items, item =>
            {
                WorkForItem(item);
            });
        }
        public static void TestParallelFor()
        {
            var items = Enumerable.Range(1,200).ToArray();

            Parallel.For(
                0,
                items.Length,
                i =>
                    {
                        WorkForItem(items[i]);
                    }
            );
        }

        public static void TestManageParallelForAndForEach()
        {
            var items = Enumerable.Range(0, 300).ToArray();

            ParallelLoopResult result =
               Parallel.For(
                   0,
                   items.Count(),
                   (int i) =>
                   {
                       WorkForItem(items[i]);
                   }
                   );
            Console.WriteLine("Default Completed: " + result.IsCompleted);//true
            Console.WriteLine("Default Items: " + result.LowestBreakIteration);


            ParallelLoopResult resultStop =
                Parallel.For(
                    0, 
                    items.Count(),
                    (int i, ParallelLoopState state) =>
                        {
                            if (i == 200)
                                state.Stop();//will stop immediately

                            WorkForItem(items[i]);
                        }
                    );
            Console.WriteLine("with Stop Completed: " + resultStop.IsCompleted); //false
            Console.WriteLine("with Stop Items: " + resultStop.LowestBreakIteration); //-

            ParallelLoopResult resultBreak =
                Parallel.For(
                    0,
                    items.Count(),
                    (int i, ParallelLoopState state) =>
                    {
                        if (i == 200)
                            state.Break(); //will guaranted iterates up to 200

                        WorkForItem(items[i]);
                    }
                    );
            Console.WriteLine("with Break Completed: " + resultBreak.IsCompleted); //false
            Console.WriteLine("with Break Items: " + resultBreak.LowestBreakIteration); //200
        }
    }
}
