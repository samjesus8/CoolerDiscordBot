using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity.Extensions;
using Newtonsoft.Json;

namespace DiscordBotTest.Commands
{
    public class Help : BaseCommandModule
    {
        [Command("cooler")]
        public async Task TestCommand(CommandContext ctx) 
        {
            ctx.Channel.SendMessageAsync("Cooler Is Gay").ConfigureAwait(false);
        }

        [Command("embed")]
        public async Task TestEmbed(CommandContext ctx) 
        {
            var builder1 = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithTitle("Test Embed")
                .WithDescription("Use this command to show that cooler is indeed gay")
                );
            await ctx.Channel.SendMessageAsync(builder1);
        }

        [Command("timestamp")]
        public async Task Response(CommandContext ctx) 
        {
            var interactivity = ctx.Client.GetInteractivity();
           
            var timestampBuilder = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithTitle("Type in and send a message")
                .WithDescription("The command will then send back the exact time you sent the message")
                );
            await ctx.Channel.SendMessageAsync(timestampBuilder);


            var message = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);
            await ctx.Channel.SendMessageAsync("Your message was sent at: " + message.Result.Timestamp.ToString());
        }
    }
}
