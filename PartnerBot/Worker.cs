using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PartnerBot
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly DiscordSocketClient _client;
        private readonly IConfiguration _config;

        public Worker(ILogger<Worker> logger, DiscordSocketClient client, IConfiguration config)
        {
            _logger = logger;
            _client = client;
            _config = config;
            _client.Ready += () => _client.SetGameAsync("DM ;partner to partner");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _client.LoginAsync(TokenType.Bot, _config["token"]);
            await _client.StartAsync();
            await Task.Delay(-1, stoppingToken);
        }
    }
}
