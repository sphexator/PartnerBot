using System;
using System.Threading.Tasks;
using Qmmands;

namespace PartnerBot.Entities
{
    public abstract class CheckAttribute : Qmmands.CheckAttribute
    {
        public override ValueTask<CheckResult> CheckAsync(CommandContext context, IServiceProvider provider)
            => CheckAsync((SocketCommandContext) context, provider);

        public abstract ValueTask<CheckResult> CheckAsync(SocketCommandContext context, IServiceProvider provider);
    }
}