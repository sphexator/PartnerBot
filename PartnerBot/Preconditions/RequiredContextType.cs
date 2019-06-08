using System;
using System.Threading.Tasks;
using Discord.WebSocket;
using PartnerBot.Entities;
using Qmmands;
using CheckAttribute = PartnerBot.Entities.CheckAttribute;

namespace PartnerBot.Preconditions
{
    public class RequiredContextType : CheckAttribute
    {
        private readonly ContextType _type;
        public RequiredContextType(ContextType type) => _type = type;

        public override ValueTask<CheckResult> CheckAsync(SocketCommandContext context, IServiceProvider provider)
        {
            switch (_type)
            {
                case ContextType.Guild:
                    if (context.Channel is SocketTextChannel) return CheckResult.Successful;
                    else return CheckResult.Unsuccessful("Command is required to be ran in a guild");
                case ContextType.Dm:
                    if (context.Channel is SocketDMChannel)
                        return CheckResult.Successful;
                    else return CheckResult.Unsuccessful("Command is required to be ran in a DM"); 
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
