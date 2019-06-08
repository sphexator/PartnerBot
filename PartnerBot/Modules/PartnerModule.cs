using System;
using System.Threading.Tasks;
using Discord;
using Microsoft.Extensions.Configuration;
using PartnerBot.Entities;
using PartnerBot.Preconditions;
using Qmmands;
using CommandContext = PartnerBot.Entities.CommandContext;

namespace PartnerBot.Modules
{
    public class PartnerModule : ModuleBase<CommandContext>
    {
        private readonly IConfiguration _config;
        private readonly ulong _guildId;
        private readonly ulong _channelId;
        public PartnerModule(IConfiguration config)
        {
            _config = config;
            _guildId = ulong.Parse(_config["GuildId"]);
            _channelId = ulong.Parse(_config["ChannelId"]);
        }

        [Name("Partner")]
        [Command("partner")]
        [Description("Replies with information on how to perform 'yes'")]
        [RequiredContextType(ContextType.Dm)]
        public async Task PartnerAsync() => await Context.ReplyAsync(_config["Response"]);

        [Name("Partner")]
        [Command("partner")]
        [Description("Yes")]
        [RequiredContextType(ContextType.Dm)]
        public async Task PartnerAsync([Remainder] string content)
        {
            var guild = Context.Client.GetGuild(_guildId);
            var channel = guild.GetTextChannel(_channelId);
            if (channel == null)
            {
                return;
            }

            await channel.SendMessageAsync(null, false, new EmbedBuilder
            {
                Author = new EmbedAuthorBuilder { Name = Context.User.Username, IconUrl = Context.User.GetAvatarUrl() },
                Description = $"Partner request by: {Context.User.Mention}\n" +
                              $"{content}",
                Timestamp = DateTimeOffset.UtcNow
            }.Build());
        }
    }
}
