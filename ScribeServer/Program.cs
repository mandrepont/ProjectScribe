using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace ScribeServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Checking database");
            Models.Database.CheckDatabase();
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://localhost:5001")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseApplicationInsights()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
