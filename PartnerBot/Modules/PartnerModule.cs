using System;
using System.Threading.Tasks;
using Discord;
using Microsoft.Extensions.Configuration;
using PartnerBot.Entities;
using PartnerBot.Extensions;
using PartnerBot.Preconditions;
using Qmmands;

namespace PartnerBot.Modules
{
    public class PartnerModule : ModuleBase<SocketCommandContext>
    {
        private readonly IConfiguration _config;
        public PartnerModule(IConfiguration config) => _config = config;

        [Name("Partner")]
        [Command("partner")]
        [Description("Replies with information on how to perform 'yes'")]
        [RequiredContextType(ContextType.Dm)]
        public async Task PartnerAsync() => await Context.ReplyAsync(_config["Response"]);

        [Name("Apply")]
        [Command("apply")]
        [Description("Yes")]
        [RequiredContextType(ContextType.Dm)]
        public async Task PartnerAsync([Remainder] string content)
        {
            var guild = Context.Client.GetGuild(ulong.Parse(_config["GuildId"]));
            var channel = guild.GetTextChannel(ulong.Parse(_config["ChannelId"]));
            if (channel == null)
            {
                return;
            }

            await channel.SendMessageAsync(null, false, new EmbedBuilder
            {
                Author = new EmbedAuthorBuilder { Name = Context.User.Username, IconUrl = Context.User.GetAvatar() },
                Description = $"Partner request by: {Context.User.Mention}\n" +
                              $"{content}",
                Timestamp = DateTimeOffset.UtcNow,
                Color = Color.Purple
            }.Build());
        }
    }
}
