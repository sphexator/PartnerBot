using System;
using System.Threading.Tasks;
using Qmmands;

namespace PartnerBot.Entities
{
    public abstract class CheckAttribute : Qmmands.CheckAttribute
    {
        public override ValueTask<CheckResult> CheckAsync(Qmmands.CommandContext context, IServiceProvider provider)
            => CheckAsync((CommandContext) context, provider);

        public abstract ValueTask<CheckResult> CheckAsync(CommandContext context, IServiceProvider provider);
    }
}