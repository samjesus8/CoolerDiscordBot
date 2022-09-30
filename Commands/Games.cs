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

        [Command("autolottery")]
        public async Task AutoLottoInfo(CommandContext ctx) 
        {
            var info = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                .WithColor(DiscordColor.Azure)
                .WithTitle("Auto Lotto command info")
                .WithDescription("This is a command that calculates the success rate at any random 5 numbers in the >lottery command \n" +
                                    "It loops infinitely through the command until it manages to find matching numbers of which you specify \n\n" +
                                    "**IMPORTANT -> The syntax to use this command is \n >autolottery num1 num2 num3 num4 num5 matches** \n\n" +
                                    "**Where num1 - num5 are your numbers & matches is the number of matches you want it to find, this has to be from 1-5 since there are 5 numbers you are checking for** \n\n" +
                                    "You can choose to check for 3 matches and it will give you a success rate, same goes for 1-5 checks \n\n" +
                                    "When the loop is done, you will eventually get an embedded message saying how many attempts it took to get X number of matches and the bot will calculate a success rate for you")
                .WithImageUrl("https://media.discordapp.net/attachments/1020110665161113610/1024722576385257482/unknown.png?width=389&height=162")
                );
            await ctx.Channel.SendMessageAsync(info);
        }

        [Command("autolottery")]
        [RequireOwner]
        public async Task AutoLotto(CommandContext ctx, int n1, int n2, int n3, int n4, int n5, int matches) 
        {
            var random = new Random();
            var interactivity = ctx.Client.GetInteractivity();
            var numberGen = new NumberGenerator();
            int[] playerNumbers = { n1, n2, n3, n4, n5 };

            int count = 0;
            int tries = 0;

            while (count <= matches) 
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (playerNumbers[i] == int.Parse(numberGen.result[j]))
                        {
                            j++;
                            count++;
                            tries++;
                            Console.WriteLine("Matches: " + count);
                            if (count == matches) { break; }
                        }
                        else
                        {
                            tries++;
                            if (count == matches) { break; }
                        }
                    }
                    if (count == matches) { break; }
                }
                if (count == matches) { break; }
            }

            if (count == matches) 
            {
                var results = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithTitle("Results for numbers " + n1 + " " + n2 + " " + n3 + " " + n4 + " " + n5)
                    .WithDescription("It took " + tries + " times for your numbers to get " + matches + " matches")
                    );
                await ctx.Channel.SendMessageAsync(results);
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
