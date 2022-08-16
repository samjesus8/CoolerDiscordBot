using DiscordBotTest.Handlers.Dialogue;
using DiscordBotTest.Handlers.Dialogue.Steps;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
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

        [Command("ducky")]
        public async Task Ducky(CommandContext ctx)
        {
            var builder1 = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithTitle("Ducky")
                .WithDescription("Shut the fuck up, noone asked")
                );
            await ctx.Channel.SendMessageAsync(builder1);
        }

        [Command("stfu")]
        public async Task ShutTheFuckUp(CommandContext ctx, string UserName)
        {
            var builder1 = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithTitle("Shut The Fuck Up " + UserName)
                .WithDescription("I dont recall the universe ever asking bozo ffs")
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
            int[] playerNumbers = { num1, num2, num3, num4, num5 };

            var rulesMessage = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithTitle("Welcome to the Lottery!!")
                .WithDescription("Pick any 5 numbers from 1-50 and test your luck \n " +
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
                .WithDescription(num1.ToString() + ", " + num2.ToString() + ", " + num3.ToString() + ", " + num4.ToString() + ", " + num5.ToString())
                );
            await ctx.Channel.SendMessageAsync(yourNumbers);

            var numberGen = new NumberGenerator();

            var botNumbers = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithTitle("The winning numbers are:")
                .WithDescription(numberGen.result[0] + ", " + numberGen.result[1] + ", " + numberGen.result[2] + ", " + numberGen.result[3] + ", " + numberGen.result[4])
                );

            await ctx.Channel.SendMessageAsync(botNumbers);
            
            int count = 0;
            for (int i = 0; i < 5; i++) 
            {
                for (int j = 0; j < 5; j++) 
                {
                    if (playerNumbers[i] == int.Parse(numberGen.result[j]))
                    {
                        Console.WriteLine("Number " + i + " matches in " + j + "th slot");
                        j++;
                        count++;
                        Console.WriteLine("Matches: " + count);
                    }
                    else
                    {
                        Console.WriteLine("Number " + i + " was not matched in " + j + "th slot");
                    }
                }
            }

            if (count == 0) 
            {
                var endMessage = new DiscordMessageBuilder()
                    .AddEmbed(
                    new DiscordEmbedBuilder()
                    .WithTitle("Your Winnings: ")
                    .WithDescription("You lose as you didn't get any matching numbers, try again with different numbers!!")
                    );
                await ctx.Channel.SendMessageAsync(endMessage);
            }
            if (count == 1) 
            {
                var endMessage = new DiscordMessageBuilder()
                    .AddEmbed(
                    new DiscordEmbedBuilder()
                    .WithTitle("You Lose ")
                    .WithDescription("You matched 1 number. You win $100")
                    );
                await ctx.Channel.SendMessageAsync(endMessage);
            }
            if (count == 2) 
            {
                var endMessage = new DiscordMessageBuilder()
                    .AddEmbed(
                    new DiscordEmbedBuilder()
                    .WithTitle("Your Winnings: ")
                    .WithDescription("You matched 2 numbers. You win $200")
                    );
                await ctx.Channel.SendMessageAsync(endMessage);
            }
            if (count == 3) 
            {
                var endMessage = new DiscordMessageBuilder()
                    .AddEmbed(
                    new DiscordEmbedBuilder()
                    .WithTitle("Your Winnings: ")
                    .WithDescription("You matched 3 numbers. You win $300 and you get to Throw '@Rus D. Lation#6905' off a cliff")
                    );
                await ctx.Channel.SendMessageAsync(endMessage);
            }
            if (count == 4) 
            {
                var endMessage = new DiscordMessageBuilder()
                    .AddEmbed(
                    new DiscordEmbedBuilder()
                    .WithTitle("Your Winnings: ")
                    .WithDescription("You matched 4 numbers. You win $400 and Unlimited Bitches for life")
                    );
                await ctx.Channel.SendMessageAsync(endMessage);
            }
            if (count == 5) 
            {
                var endMessage = new DiscordMessageBuilder()
                    .AddEmbed(
                    new DiscordEmbedBuilder()
                    .WithTitle("YOU WON THE LOTTERY!!!")
                    .WithDescription("You matched all 5 numbers. You win $500, Unlimited Bitches and '@Solz#2652' gets killed")
                    );
                await ctx.Channel.SendMessageAsync(endMessage);
            }

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

        [Command("dialogue")]
        public async Task Dialogue(CommandContext ctx) 
        {
            var inputStep = new TextStep("Enter something", null); //Input from the user
            string input = string.Empty; //Storage for the data
            inputStep.OnValidResult += (result) => input = result; //If input is valid, store in the input variable

            var userDM = await ctx.Member.CreateDmChannelAsync(); //Creating User DM

            var dialogueHandler = new DialougeHandler(ctx.Client, userDM, ctx.User, inputStep);

            bool sucess = await dialogueHandler.ProcessDialogue();
            if (!sucess) { return; }

            await ctx.Channel.SendMessageAsync(input);
        }
    }
}
