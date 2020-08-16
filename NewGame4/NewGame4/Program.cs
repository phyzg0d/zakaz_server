using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace NewGame4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Dictionary<string, object> test = new Dictionary<string, object>
            {
                { "123", 11 },    
                { "1234", 111 },
                { "12345", 1111 }
            };
            
            string exampleString = JsonConvert.SerializeObject(test);
            Console.WriteLine(exampleString);
            
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nServer started\n");
            CreateHostBuilder(args).Build().Run();
            
            Console.Read();
        }
        
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}


