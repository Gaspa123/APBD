using LinqTutorials.Models;
using System;

namespace LinqTutorials
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = LinqTasks.Task12();

            foreach (Emp d in t)
            {
                Console.WriteLine(d.Empno);
            }

        }
    }
}
