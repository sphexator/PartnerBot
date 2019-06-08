using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace PartnerBot.Entities
{
    public class SocketCommandContext : Qmmands.CommandContext
    {
        public SocketCommandContext(DiscordSocketClient client, SocketUserMessage msg, SocketUser user)
        {
            Client = client;
            Message = msg;
            User = user;
            Guild = (user as SocketGuildUser)?.Guild;
            Channel = msg.Channel;
        }

        public SocketUserMessage Message { get; }
        public DiscordSocketClient Client { get; }
        public SocketUser User { get; }
        public SocketGuild Guild { get; }
        public ISocketMessageChannel Channel { get; }

        public async Task ReplyAsync(string content, uint? color = null)
        {
            if (!color.HasValue) color = Color.Purple.RawValue;
            await Channel.SendMessageAsync(null, false, new EmbedBuilder
            {
                Description = content,
                Color = new Color(color.Value)
            }.Build());
        }

        public async Task ReplyAsync(EmbedBuilder embed, uint? color = null)
        {
            if (!color.HasValue) color = Color.Purple.RawValue;
            await Channel.SendMessageAsync(null, false, embed.Build());
        }

        public async Task ErrorAsync(string content)
        {

        }
    }
}
