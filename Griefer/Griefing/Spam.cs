using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Linq;
using System.Threading.Tasks;

namespace Griefer.Griefing
{
    public class Spam : ModuleBase<SocketCommandContext>
    {
        [Command("Spam")]
        public async Task SpamAsync([Remainder] string Message = null)
        {
            await Context.Message.DeleteAsync();
            var everyone = Context.Guild.EveryoneRole;
            var current = Context.Client.CurrentUser;
            var id = Context.Guild.Id;
            SocketGuild guild = Context.Client.GetGuild(id) as SocketGuild;
            ITextChannel channel = guild.TextChannels.FirstOrDefault(x => x.Name == $"{Context.Channel.Name}") as ITextChannel;
            if (Message == null)
            {
                await ReplyAsync($"**Message Argument is Missing**");
            }
            else
            {
                foreach (var spam in Context.Guild.TextChannels)
                {
                    await channel.AddPermissionOverwriteAsync(everyone, new OverwritePermissions(sendTTSMessages: PermValue.Allow));
                    await channel.AddPermissionOverwriteAsync(current, new OverwritePermissions(sendTTSMessages: PermValue.Allow));
                    await spam.SendMessageAsync($"{Message}", true);
                    await spam.SendMessageAsync($"{Message}", true);
                    await spam.SendMessageAsync($"{Message}", true);
                    await spam.SendMessageAsync($"{Message}", true);
                    await spam.SendMessageAsync($"{Message}", true);
                }
            }
        }
    }
}
