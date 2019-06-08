using System;
using System.Threading.Tasks;
using Discord.WebSocket;
using PartnerBot.Entities;
using Qmmands;

namespace PartnerBot.Services
{
    public class CommandHandling
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _command;
        private readonly IServiceProvider _provider;
        public CommandHandling(DiscordSocketClient client, CommandService command, IServiceProvider provider)
        {
            _client = client;
            _command = command;
            _provider = provider;

            _client.MessageReceived += ClientOnMessageReceived;
        }

        private async Task ClientOnMessageReceived(SocketMessage arg)
        {
            if (!(arg is SocketUserMessage msg)) return;
            if (arg.Author.IsBot) return;

            if (!CommandUtilities.HasPrefix(arg.Content, ";", StringComparison.CurrentCultureIgnoreCase, out var output)) return;
            await _command.ExecuteAsync(output, new SocketCommandContext(_client, msg, msg.Author), _provider);
        }
    }
}