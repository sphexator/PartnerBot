using System;
using System.Threading.Tasks;
using PartnerBot.Entities;
using Qmmands;
using CheckAttribute = PartnerBot.Entities.CheckAttribute;
using CommandContext = PartnerBot.Entities.CommandContext;

namespace PartnerBot.Preconditions
{
    public class RequiredContextType : CheckAttribute
    {
        private readonly ContextType _type;
        public RequiredContextType(ContextType type) => _type = type;

        public override ValueTask<CheckResult> CheckAsync(CommandContext context, IServiceProvider provider)
        {
            switch (_type)
            {
                case ContextType.Guild:
                    return context.Guild == null 
                        ? CheckResult.Unsuccessful("Command is required to be ran in a guild")
                        : CheckResult.Successful;
                case ContextType.Dm:
                    return context.Guild == null 
                        ? CheckResult.Successful 
                        : CheckResult.Unsuccessful("Command is required to be ran in a dm");
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
