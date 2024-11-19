using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Windows.Forms;
using DataAccess;
using Microsoft.Extensions.Configuration;

namespace ApiTestClient
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var host = CreateHostBuilder().Build();

            // Start the application with DI
            ApplicationConfiguration.Initialize();
            var services = host.Services;
            Application.Run(services.GetRequiredService<FormApiTesting>());
        }

        // Configure the Host and Dependency Injection
        static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    // Add the DbContext with connection string
                    services.AddDbContext<AppDbContext>(options =>
                        options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));

                    // Add Windows Forms
//                    services.AddSingleton<FormApiTesting>();
                    services.AddScoped<FormApiTesting>();
                });
    }
}
