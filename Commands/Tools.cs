﻿using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System.Threading.Tasks;

namespace DiscordBotTest.Commands
{
    public class Tools : BaseCommandModule
    {
        [Command("status")]
        public async Task SetBotStatus(CommandContext ctx, params string[] message) 
        {
            if (ctx.User.Id == 572877986223751188 || ctx.User.Id == 327845261692895232) 
            {
                string finalMessage = string.Join(" ", message);
                DiscordActivity activity = new DiscordActivity();
                DiscordClient discord = ctx.Client;

                activity.Name = finalMessage;
                await discord.UpdateStatusAsync(activity);
                return;
            }
            else 
            {
                var notAllowed = new DiscordMessageBuilder()
                    .AddEmbed(
                    new DiscordEmbedBuilder()
                    .WithTitle("Access Denied")
                    .WithDescription("If you want access to this command ask @𝕤𝕒𝕞.𝕛𝕖𝕤𝕦𝕤𝟠#6825")
                    );
                await ctx.Channel.SendMessageAsync(notAllowed);
                return; 
            }
        }

        [Command("invite")]
        public async Task SendInviteLink(CommandContext ctx) 
        {
            var inviteMessage = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithTitle("Invite the bot to other servers using this link")
                .WithDescription("Click on the title to open the invite link")
                .WithUrl("https://discord.com/api/oauth2/authorize?client_id=1001273087158919228&permissions=534790011200&scope=bot%20applications.commands")
                );
            await ctx.Channel.SendMessageAsync(inviteMessage);
        }

        [Command("server")]
        public async Task SendServerLink(CommandContext ctx) 
        {
            var discordMessage = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                .WithTitle("Official Discord Server")
                .WithColor(DiscordColor.Azure)
                .WithDescription("Join the Official Discord server for the bot \n" +
                                    "Click the title to join!!")
                .WithUrl("https://discord.gg/kfaVgHN7zv")
                );
            await ctx.Channel.SendMessageAsync(discordMessage);
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


            var message = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel);
            await ctx.Channel.SendMessageAsync("Your message was sent at: " + message.Result.Timestamp.ToString());
        }
    }
}
