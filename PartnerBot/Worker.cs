using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PartnerBot.Modules;
using Qmmands;

namespace PartnerBot
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly DiscordSocketClient _client;
        private readonly CommandService _command;
        private readonly IConfiguration _config;

        public Worker(ILogger<Worker> logger, DiscordSocketClient client, IConfiguration config, CommandService command)
        {
            _logger = logger;
            _client = client;
            _config = config;
            _command = command;
            _client.Ready += () => _client.SetGameAsync("DM ;partner to partner");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _command.AddModule<PartnerModule>();
            await _client.LoginAsync(TokenType.Bot, _config["Token"]);
            await _client.StartAsync();
            await Task.Delay(-1, stoppingToken);
        }
    }
}
