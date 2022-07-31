using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotTest.Commands
{
    public class FunCommands : BaseCommandModule
    {
        [Command("cooler")]
        public async Task TestCommand(CommandContext ctx)
        {
            var builder1 = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithTitle("Test Embed")
                .WithDescription("Use this command to show that cooler is indeed gay")
                );
            await ctx.Channel.SendMessageAsync(builder1);
        }

        [Command("tory")]
        public async Task Tory(CommandContext ctx) 
        {
            await ctx.Channel.SendMessageAsync("Fuck boris, fuck the opps, fuck dem man, fuck flippin rishi sunak the dirkhead, fuck liz, fuck every man in that government");
        }

        [Command("delet")]
        public async Task Delet(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Midgends Middkan Midcha Mid Everything Midcord Mid Mid Mid L Bozo");
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

        [Command("lottery")]
        public async Task LotteryGame(CommandContext ctx, int num1, int num2, int num3, int num4, int num5) 
        {
            var random = new Random();
            var interactivity = ctx.Client.GetInteractivity();

            var rulesMessage = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithTitle("Welcome to the Lottery!!")
                .WithDescription("For single digit numbers like 1, you must type in '01' \n " +
                                "The bot will then randomly generate 5 numbers. If any of your numbers match you win a prize \n\n" +
                                "The prizes are as following: \n" +
                                "1 number = $100 \n" +
                                "2 numbers = $200 \n" +
                                "3 numbers = $300 + Rusty Gets thrown off a cliff \n" +
                                "4 numbers = $400 + Unlimited Bitches for life \n" +
                                "5 numbers = $500 + Unlimited Bitches + Mad gets killed")
                );
            await ctx.Channel.SendMessageAsync(rulesMessage);

            var yourNumbers = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithTitle("Your numbers are:")
                .WithDescription(num1.ToString() + "," + num2.ToString() + "," + num3.ToString() + "," + num4.ToString() + "," + num5.ToString())
                );
            await ctx.Channel.SendMessageAsync(yourNumbers);

            var numberGen = new NumberGenerator();

            var botNumbers = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithTitle("The winning numbers are:")
                .WithDescription(numberGen.result.ToString())
                );

            await ctx.Channel.SendMessageAsync(botNumbers);
        }

        [Command("question")]
        public async Task Question(CommandContext ctx)
        {
            var message = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithTitle("What does cooler think STDs are?")
                .WithDescription("Options are below:")
                )
                .AddComponents(
                new DiscordButtonComponent(ButtonStyle.Primary, "dick", "Pussy"),
                new DiscordButtonComponent(ButtonStyle.Primary, "plane", "Fuck knows he hasnt even heard of it")
                );
            await ctx.Channel.SendMessageAsync(message);

            ctx.Client.ComponentInteractionCreated += async (a, b) =>
            {
                if (b.Interaction.Data.CustomId == "dick")
                {
                    await ctx.Channel.SendMessageAsync("Cooler cant get any");
                    return;

                }
                if (b.Interaction.Data.CustomId == "plane")
                {
                    await ctx.Channel.SendMessageAsync("Correct. U think he knows that shit");
                    return;
                }
            };
        }
    }
}
