using DiscordBotTest.Commands;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.Lavalink;
using DSharpPlus.Net;
using DSharpPlus.SlashCommands;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotTest
{
    public class Bot
    {
        public DiscordClient Client { get; private set; } 
        public InteractivityExtension Interactivity { get; private set; }
        public CommandsNextExtension Commands { get; private set; }

        public async Task RunAsync() 
        {
            var json = string.Empty;
            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);

            var configJson = JsonConvert.DeserializeObject<ConfigJSON>(json);

            var config = new DiscordConfiguration()
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true
            };

            Client = new DiscordClient(config);

            Client.UseInteractivity(new InteractivityConfiguration
            {
                Timeout = TimeSpan.FromMinutes(2)
            });

            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { configJson.Prefix },
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = false,
            };

            var slashCommandsConfig = Client.UseSlashCommands();

            Commands = Client.UseCommandsNext(commandsConfig);
            Commands.RegisterCommands<Help>();
            Commands.RegisterCommands<Calculator>();
            Commands.RegisterCommands<FunCommands>();
            Commands.RegisterCommands<Games>();
            Commands.RegisterCommands<Tools>();
            Commands.RegisterCommands<MusicPlayer>();
            slashCommandsConfig.RegisterCommands<DokkanSL>();
            Commands.CommandErrored += OnCommandError;

            var endpoint = new ConnectionEndpoint
            {
                Hostname = "lavalink.oops.wtf",
                Port = 443,
                Secured = true
            };
            var lavaLinkConfig = new LavalinkConfiguration
            {
                Password = "www.freelavalink.ga",
                RestEndpoint = endpoint,
                SocketEndpoint = endpoint
            };
            var lavalink = Client.UseLavalink();

            await Client.ConnectAsync();
            await lavalink.ConnectAsync(lavaLinkConfig);
            await Task.Delay(-1);
        }

        private Task OnClientReady(ReadyEventArgs e) 
        {
            return Task.CompletedTask;
        }

        private async Task OnCommandError(CommandsNextExtension sender, CommandErrorEventArgs e)
        {
            if (e.Exception is ChecksFailedException) //Checking if its a failed command 
            {
                var cast = (ChecksFailedException)e.Exception; //Converting the exception into type ChecksFailedException
                string timeLeftCooldown = ""; //Variable to store cooldown remaining time

                foreach (var check in cast.FailedChecks) //Checking for any Cooldown Related errors
                {
                    var cooldown = (CooldownAttribute)check; //Casting as a Cooldown Attribute
                    TimeSpan timeLeft = cooldown.GetRemainingCooldown(e.Context); //Getting remaining cooldown
                    timeLeftCooldown = timeLeft.ToString(@"hh\:mm\:ss");
                }

                Console.Error.WriteLine("Error " + e.Exception);

                var cooldownErrorMessage = new DiscordMessageBuilder() //Message to send when this exception is thrown
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.DarkRed)
                    .WithTitle("You must wait for the cooldown to end!!")
                    .WithDescription("Remaining Time (HH:MM:SS) -> ***" + timeLeftCooldown + "***")
                    );

                await e.Context.Channel.SendMessageAsync(cooldownErrorMessage);
            }
        }
    }
}
