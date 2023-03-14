using _70_483._1_ManageProgramFlow._1_ImplementMultiThreadAndAsync;

namespace _70_483._1_ManageProgramFlow
{
    public static class ImplementMultiThreadAndAsync
    {
        public static void Test_TaskParallerLibrary()
        {
            MenuBuilder.BuildMenu(typeof(TaskParallerLibrary));
        }
        public static void Test_ParallelLINQ()
        {
            MenuBuilder.BuildMenu(typeof(ParallelLINQ));
        }
        public static void Test_Tasks()
        {
            MenuBuilder.BuildMenu(typeof(Tasks));
        }
        public static void Test_ContinuationTasks()
        {
            MenuBuilder.BuildMenu(typeof(ContinuationTasks));
        }
        public static void Test_ThreadAndThreadPool()
        {
            MenuBuilder.BuildMenu(typeof(ThreadsAndThreadPool));
        }
        public static void Test_TasksAndUI()
        {
            throw new System.NotImplementedException();
        }
        public static void Test_AsyncAwait()
        {
            throw new System.NotImplementedException();
        }
        public static void Test_ConcurrentCollections()
        {
            throw new System.NotImplementedException();
        }
    }
}
