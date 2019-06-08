using System;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PartnerBot.Modules;
using PartnerBot.Services;
using Qmmands;

namespace PartnerBot
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly DiscordSocketClient _client;
        private readonly CommandService _command;
        private readonly IConfiguration _config;
        private readonly IServiceProvider _provider;

        public Worker(ILogger<Worker> logger, DiscordSocketClient client, IConfiguration config, CommandService command, IServiceProvider provider)
        {
            _logger = logger;
            _client = client;
            _config = config;
            _command = command;
            _provider = provider;

            _client.Log += ClientOnLog;
            _command.CommandErrored += OnCommandError;
            _client.Ready += () => _client.SetGameAsync("DM ;partner to partner");
        }

        private Task OnCommandError(CommandErroredEventArgs log)
        {
           // _logger.Log();
            return Task.CompletedTask;
        }

        private Task ClientOnLog(LogMessage log)
        {
            _logger.Log(LogSevToLogLevel(log.Severity), log.Message, log.Exception);
            return Task.CompletedTask;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _command.AddModule<PartnerModule>();
            _provider.GetRequiredService<CommandHandling>();
            await _client.LoginAsync(TokenType.Bot, _config["Token"]);
            await _client.StartAsync();
            await Task.Delay(-1, stoppingToken);
        }

        private LogLevel LogSevToLogLevel(LogSeverity log)
        {
            switch (log)
            {
                case LogSeverity.Critical:
                    return LogLevel.Critical;
                case LogSeverity.Error:
                    return LogLevel.Error;
                case LogSeverity.Warning:
                    return LogLevel.Warning;
                case LogSeverity.Info:
                    return LogLevel.Information;
                case LogSeverity.Verbose:
                    return LogLevel.Trace;
                case LogSeverity.Debug:
                    return LogLevel.Trace;
                default:
                    return LogLevel.None;
            }
        }
    }
}
