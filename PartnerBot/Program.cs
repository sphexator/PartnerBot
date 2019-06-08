using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PartnerBot.Services;
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
                    services.AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
                        { LogLevel = LogSeverity.Info }));
                    services.AddSingleton(new CommandService(new CommandServiceConfiguration
                        { DefaultRunMode = RunMode.Parallel }));
                    services.AddSingleton<CommandHandling>();
                    services.AddHostedService<Worker>();
                });
    }
}