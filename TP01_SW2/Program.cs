using ExemploAula03;
using Microsoft.AspNetCore.Hosting;
using System;
using TP01_SW2.Negocio;
using TP01_SW2.Repository;

namespace TP01_SW2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new BookTest();
            new BookRepositoryCSV();

            IWebHost host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();
            host.Run();
        }
    }
}
