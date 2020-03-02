using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Speech.Synthesis;
using System.Threading;
using System.Threading.Tasks;
namespace Griefer
{
    class Program
    {
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();
        private DiscordSocketClient _client;
        private CommandService _commands;
        private CommandServiceConfig _config;
        private IServiceProvider _services;
        public SocketGuild Guild { get; }
        public async Task RunBotAsync()
        {
            /// /// /// Extra /// /// /// 
            String Token = $"<Token Here>";
            string Progresbar = "Griefer";
            var title = "";
            for (int i = 0; i < Progresbar.Length; i++)
            {
                title += Progresbar[i];
                Console.Title = title;
                Thread.Sleep(100);
            }
            title = "";
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"   /// ///          /// /// ");
            Console.WriteLine($";) /// /// Griefer /// /// ;)");
            Console.WriteLine($"  /// ///         /// /// ");
            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _config = new CommandServiceConfig();
            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_config)
                .AddSingleton(_commands)
                .BuildServiceProvider();
            /// /// Running /// ///
            _client.Log += Log;
            await _client.LoginAsync(Discord.TokenType.Bot, Token);
            await RegisterCommandsAsync();
            await _client.StartAsync();
            await Task.Delay(-1);
        }
        /// /// Log In /// /// 
        private Task Log(LogMessage arg)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }
        /// ///  Register Commands /// /// 
        public async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }
        /// ///  Command Handler /// ///
        public async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            if (message is null || message.Author.IsBot) return;
            int argPos = 0;
            if (message.HasStringPrefix($"/", ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var context = new SocketCommandContext(_client, message);
                var result = await _commands.ExecuteAsync(context, argPos, _services);
                if (!result.IsSuccess)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(result.ErrorReason);
                }
            }
        }
    }
}
