﻿using DiscordBotTest.Commands;
using DSharpPlus;
using DSharpPlus.CommandsNext;
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
                AutoReconnect = true,
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
                Hostname = "127.0.0.1",
                Port = 2333
            };
            var lavaLinkConfig = new LavalinkConfiguration
            {
                Password = "1234",
                RestEndpoint = endpoint,
                SocketEndpoint = endpoint,
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
            if (e.Exception is ChecksFailedException) 
            {
                Console.Error.WriteLine("Error " + e.Exception);

                var cooldownErrorMessage = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("You must wait for the cooldown to end")
                    );

                await e.Context.Channel.SendMessageAsync(cooldownErrorMessage);
            }
        }
    }
}
