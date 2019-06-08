using Discord;
using Discord.WebSocket;

namespace PartnerBot.Extensions
{
    public static class UserExtensions
    {
        public static string GetAvatar(this SocketUser user)
        {
            var avi = user.GetAvatarUrl(ImageFormat.Auto, 2048);
            return avi ?? user.GetDefaultAvatarUrl();
        }
    }
}
