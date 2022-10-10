using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscordBotTest.Commands
{
    public class Games : BaseCommandModule
    {
        [Command("cardgame")]
        public async Task SimpleCardGame(CommandContext ctx)
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

        [Command("lottery")]
        public async Task LotteryRules(CommandContext ctx)
        {
            var rulesMessage = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithTitle("Welcome to the Lottery!!")
                .WithColor(DiscordColor.Azure)
                .WithDescription("Pick any 5 numbers from 1-100 and test your luck. For example: >lottery 1 2 3 4 5 \n " +
                                    "The bot will then randomly generate 5 numbers. If any of your numbers match you win a prize \n\n" +
                                    "The prizes are as following: \n" +
                                    "1 number = $100 \n" +
                                    "2 numbers = $200 \n" +
                                    "3 numbers = $300 + Rusty Gets thrown off a cliff \n" +
                                    "4 numbers = $400 + Unlimited Bitches for life \n" +
                                    "5 numbers = $500 + Unlimited Bitches + Mad gets killed")
                            );
            await ctx.Channel.SendMessageAsync(rulesMessage);
        } //Rules

        [Command("lottery")]
        public async Task LotteryGame(CommandContext ctx, int num1, int num2, int num3, int num4, int num5)
        {
            var random = new Random();
            var interactivity = ctx.Client.GetInteractivity();
            int[] playerNumbers = { num1, num2, num3, num4, num5 };

            var yourNumbers = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithTitle("Your numbers are:")
                .WithDescription(num1.ToString() + ", " + num2.ToString() + ", " + num3.ToString() + ", " + num4.ToString() + ", " + num5.ToString())
                .WithColor(DiscordColor.Azure)
                );
            await ctx.Channel.SendMessageAsync(yourNumbers);

            var numberGen = new NumberGenerator();

            var botNumbers = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithTitle("The winning numbers are:")
                .WithDescription(numberGen.result[0] + ", " + numberGen.result[1] + ", " + numberGen.result[2] + ", " + numberGen.result[3] + ", " + numberGen.result[4])
                .WithColor(DiscordColor.Violet)
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
                        Console.WriteLine();

                        j++;
                        count++;
                        Console.WriteLine("Matches: " + count);
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("Number " + i + " was not matched in " + j + "th slot");
                        Console.WriteLine();
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
                    .WithColor(DiscordColor.Red)
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
                    .WithColor(DiscordColor.Green)
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
                    .WithColor(DiscordColor.Green)
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
                    .WithColor(DiscordColor.Green)
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
                    .WithColor(DiscordColor.Green)
                    );
                await ctx.Channel.SendMessageAsync(endMessage);
            }
            if (count == 5)
            {
                var endMessage = new DiscordMessageBuilder()
                    .AddEmbed(
                    new DiscordEmbedBuilder()
                    .WithTitle("YOU WON THE LOTTERY!!! " + ctx.User.Username.ToString())
                    .WithDescription("You matched all 5 numbers. You win $500, Unlimited Bitches and '@Solz#2652' gets killed")
                    .WithColor(DiscordColor.Green)
                    );
                await ctx.Channel.SendMessageAsync(endMessage);
            }
        }

        [Command("mid")]
        public async Task MidOrNotMidRules(CommandContext ctx, string input)
        {
            if (input == "rules" || input == "info" || input == "Info") 
            {
                var rules = new DiscordMessageBuilder()
                    .AddEmbed(
                    new DiscordEmbedBuilder()
                    .WithTitle("Mid or Not Mid Instructions")
                    .WithColor(DiscordColor.Azure)
                    .WithDescription("The premise of the game is simple, a good looking anime girl will be displayed on screen \n" +
                                            "If you think she's mid, vote mid. The most votes wins the game \n\n" +
                                                "Have fun and remember, don't make it serious, its just a fucking cartoon figure")
                    .WithImageUrl("https://media.discordapp.net/attachments/1020110665161113610/1025399022845956126/unknown.png?width=572&height=597")
                    );
                await ctx.Channel.SendMessageAsync(rules);
            }
        } //Rules

        [Command("mid")]
        [Cooldown(1, 25, CooldownBucketType.User)]
        public async Task MidOrNotMid(CommandContext ctx)
        {
            DiscordEmoji[] emojiOptions = { DiscordGuildEmoji.FromName(ctx.Client, ":MID_EMOTE:", true), DiscordGuildEmoji.FromName(ctx.Client, ":NOT_MID_EMOTE:", true) };
            var interactivity = ctx.Client.GetInteractivity();
            var random = new Random();
            var duration = TimeSpan.FromSeconds(20);

            List<String[]> messages = new List<String[]>(); //[0] = Name, [1] = Anime Name, [2] = Image URL

            var waifuCard = new WaifuCardBuilder();

            string[] card = { waifuCard.Name, waifuCard.AnimeName, waifuCard.ImageURL };
            messages.Add(card);

            int index = random.Next(messages.Count);

            var testEmbed = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithAuthor("MID OR NOT MID?? | Cast your votes below | You have 20 seconds to vote")
                .WithTitle("***" + messages[index][0] + "***")
                .WithDescription(messages[index][1])
                .WithImageUrl(messages[index][2])
                .WithColor(DiscordColor.Blue)
                .WithFooter("Use '>mid info' to view the instructions on this command")
                );
            var pollMSG = await ctx.Channel.SendMessageAsync(testEmbed);

            foreach (var option in emojiOptions)
            {
                await pollMSG.CreateReactionAsync(option);
            }

            var result = await interactivity.CollectReactionsAsync(pollMSG, duration);

            int midCount = 0;
            int notMidCount = 0;

            foreach (var emoji in result) 
            {
                if (emoji.Emoji == emojiOptions[0]) 
                {
                    midCount++;
                }
                if (emoji.Emoji == emojiOptions[1]) 
                {
                    notMidCount++;
                }
            }

            int totalVotes = midCount + notMidCount;
            var resultsEmbed = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithTitle("***Results***")
                .WithColor(DiscordColor.Azure)
                .WithDescription(emojiOptions[0] + ": " + midCount + " Votes" + "\n" +
                                 emojiOptions[1] + ": " + notMidCount + " Votes" + "\n\n" +
                                 "A total of " + totalVotes + " users voted in this session")
                );
            await ctx.Channel.SendMessageAsync(resultsEmbed);

            if (midCount > notMidCount)
            {
                var midWin = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Green)
                    .WithTitle("The winner for " + messages[index][0])
                    .WithDescription("MID wins with " + midCount + " votes")
                    );
                await ctx.Channel.SendMessageAsync(midWin);
            }
            if (midCount < notMidCount)
            {
                var notMidWin = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Red)
                    .WithTitle("The winner for " + messages[index][0])
                    .WithDescription("NOT MID wins with " + notMidCount + " votes")
                    );
                await ctx.Channel.SendMessageAsync(notMidWin);
            }
        }

        [Command("passive")]
        [Cooldown(1, 32, CooldownBucketType.User)]
        public async Task GuessThePassive(CommandContext ctx)
        {
            var random = new Random();
            var interactivity = ctx.Client.GetInteractivity();
            var timeLimit = TimeSpan.FromSeconds(30);

            List<string[]> passivesList = new List<string[]>(); //[0] = Name of Unit, [1] = Passive, [2] ImageURL

            var dokkanCard = new DokkanCardBuilder();

            string[] dokkanUnit = { dokkanCard.Name, dokkanCard.Passive, dokkanCard.ImageURL };
            passivesList.Add(dokkanUnit);

            var index = random.Next(passivesList.Count); //Chooses from list at random

            var messageBuilder = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithColor(DiscordColor.Azure)
                .WithTitle("Guess this passive skill | You have 30 seconds till the answer is shown")
                .WithDescription(passivesList[index][1]) //Displays Passive
                );
            var messageCheck = await ctx.Channel.SendMessageAsync(messageBuilder);

            var wait = await interactivity.CollectReactionsAsync(messageCheck, timeLimit); //Waits for 30s

            var answer = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                .WithColor(DiscordColor.Green)
                .WithTitle("The unit was -> " + passivesList[index][0]) //Name
                .WithImageUrl(passivesList[index][2])//ImageURL
                );
            await ctx.Channel.SendMessageAsync(answer);
        }
    }
}
