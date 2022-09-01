using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Threading.Tasks;

namespace DiscordBotTest.Commands
{
    public class Games : BaseCommandModule
    {
        [Command("cardgame")]
        public async Task SimpleCardGame(CommandContext ctx)
        {
            var drawCard = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithTitle("Press the Draw Button to draw a card!!!")
                .WithDescription("If you draw higher than the Bot, you win")
                )
                .AddComponents(
                new DiscordButtonComponent(ButtonStyle.Primary, "drawButton", "Draw Card")
                );
            await ctx.Channel.SendMessageAsync(drawCard);

            ctx.Client.ComponentInteractionCreated += async (a, b) =>
            {
                if (b.Interaction.Data.CustomId == "drawButton" && b.Interaction.User == ctx.User)
                {
                    var cardGen = new CardGenerator();
                    var yourCard = new DiscordMessageBuilder()
                        .AddEmbed(
                        new DiscordEmbedBuilder()
                        .WithTitle("Your Card")
                        .WithDescription("You drew a: " + cardGen.cardHit)
                        );

                    await ctx.Channel.SendMessageAsync(yourCard);


                    var botCardGen = new CardGenerator();
                    var botCard = new DiscordMessageBuilder()
                        .AddEmbed(
                        new DiscordEmbedBuilder()
                        .WithTitle("The bot drew a: ")
                        .WithDescription(botCardGen.cardHit)
                        );

                    await ctx.Channel.SendMessageAsync(botCard);

                    if (cardGen.GenNoRes > botCardGen.GenNoRes)
                    {
                        var winner = new DiscordMessageBuilder()
                            .AddEmbed(
                            new DiscordEmbedBuilder()
                            .WithTitle("YOU WIN")
                            );

                        await ctx.Channel.SendMessageAsync(winner);
                        return;
                    }
                    else
                    {
                        var lose = new DiscordMessageBuilder()
                            .AddEmbed(
                            new DiscordEmbedBuilder()
                            .WithTitle("YOU LOSE")
                            );

                        await ctx.Channel.SendMessageAsync(lose);
                        return;
                    }
                }
                else 
                {
                    var deniedMessage = new DiscordMessageBuilder()
                        .AddEmbed(
                        new DiscordEmbedBuilder()
                        .WithTitle("Sorry you cannot use this button " + b.Interaction.User.Username.ToString())
                        .WithDescription("You have to call the command yourself to use this button, you cannot play someone else's game. Please use >cardgame to play")
                        );
                    await ctx.Channel.SendMessageAsync(deniedMessage);
                }
            };
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


    }
}
