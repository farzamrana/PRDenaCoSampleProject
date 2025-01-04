using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using System.IO;

namespace PRDenaCo.Web
{
    public class Program
    {

        public static void Main(string[] args)
        {

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {

            var configSettings = new ConfigurationBuilder()
       .AddJsonFile("appsettings.json")
       .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configSettings)
                .CreateLogger();

            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    
                    webBuilder.ConfigureAppConfiguration(config =>
                     {
                         config.AddConfiguration(configSettings);
                     });
                    webBuilder.ConfigureLogging(logger =>
                    {
                        logger.ClearProviders();
                        logger.AddEventLog();
                        logger.AddFile("logs/logtext.txt");
                        logger.AddSerilog();
                    });
                    webBuilder.UseStartup("PRDenaCo.Web");
                    webBuilder.UseIISIntegration();
                    webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
                   
                });
        }
    }
}
