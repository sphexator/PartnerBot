using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Qmmands;

namespace PartnerBot
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddSingleton(BuildConfig());
                    services.AddHostedService<Worker>();
                    services.AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
                    {
                        AlwaysDownloadUsers = true,
                        LogLevel = LogSeverity.Info,
                        MessageCacheSize = 10
                    }));
                    services.AddSingleton(new CommandService(new CommandServiceConfiguration 
                        { DefaultRunMode = RunMode.Parallel }));
                });

        public static IConfiguration BuildConfig()
            => new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json")
                .Build();
    }
}
