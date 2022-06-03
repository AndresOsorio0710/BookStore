using System;

namespace Backend.Infrastructure.Data
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create data base");
            Backend.Infrastructure.Data.Contexts.BookAppContext appContext = new Backend.Infrastructure.Data.Contexts.BookAppContext();
            appContext.Database.EnsureCreated();
            Console.WriteLine("OK");
        }
    }
}
