using System;
using System.Linq;

namespace _70_483._1_ManageProgramFlow._1_ImplementMultiThreadAndAsync
{
    public static class ParallelLINQ
    {
        private class Person
        {
            public string Name { get; set; }
            public string City { get; set; }
        }

        public static void Test()
        {

            Person[] people = new Person[]
           {
                new Person { Name = "Alan", City = "Hull" },
                new Person { Name = "Beryl", City = "Seattle" },
                new Person { Name = "Charles", City = "London" },
                new Person { Name = "David", City = "Seattle" },
                new Person { Name = "Eddy", City = "Paris" },
                new Person { Name = "Fred", City = "Berlin" },
                new Person { Name = "Gordon", City = "Hull" },
                new Person { Name = "Henry", City = "Seattle" },
                new Person { Name = "Isaac", City = "Seattle" },
                new Person { Name = "James", City = "London" }
           };

            /* var result = from person in people.AsParallel()
                          where person.City == "Seattle" 
                          select person;   //Linq sql style */

            var rresult = people.AsParallel().OrderBy(x => x.Name).Where(x => x.City == "Seattle").Select(x => x.Name).Take(4);//.AsSequential();

            rresult.ForAll(x => Console.WriteLine(x));

            foreach (var person in rresult)
                Console.WriteLine($"one more person from Seattle: {person}");
        }
    }
}
