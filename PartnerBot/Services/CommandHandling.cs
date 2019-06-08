using System;
using System.Threading.Tasks;
using Discord.WebSocket;
using Qmmands;

namespace PartnerBot.Services
{
    public class CommandHandling
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _command;
        public CommandHandling(DiscordSocketClient client, CommandService command)
        {
            _client = client;
            _command = command;

            _client.MessageReceived += ClientOnMessageReceived;
        }

        private async Task ClientOnMessageReceived(SocketMessage arg)
        {
            if (!(arg is SocketUserMessage message)) return;
            if (message.Author.IsBot) return;

            if (!CommandUtilities.HasPrefix(message.Content, ";", StringComparison.CurrentCultureIgnoreCase, out var output)) return;
            await _command.ExecuteAsync(output, new Entities.CommandContext(_client, message, message.Author));
        }
    }
}