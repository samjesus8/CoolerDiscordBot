using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
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
        }

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
        public async Task MidOrNotMidRules(CommandContext ctx)
        {
            var rules = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithTitle("Mid or Not Mid Instructions")
                .WithColor(DiscordColor.Azure)
                .WithDescription("IMPORTANT: THE SYNTAX OF THE COMMAND IS \n ***'>mid TimeLimit YourEmojis'*** \n\n" +
                                    "The premise of the game is simple, a good looking anime girl will be displayed on screen \n" +
                                        "If you think she's mid, vote mid. The most votes wins the game \n\n" +
                                            "Have fun and remember, don't make it serious, its just a fucking cartoon figure")
                .WithImageUrl("https://media.discordapp.net/attachments/735858039537795203/1019733895526219826/unknown.png?width=514&height=537")
                );
            await ctx.Channel.SendMessageAsync(rules);
        }

        [Command("mid")]
        public async Task MidOrNotMid(CommandContext ctx, TimeSpan duration)  
        {
            DiscordEmoji[] emojiOptions = { DiscordEmoji.FromName(ctx.Client, ":thumbsup:", true), DiscordEmoji.FromName(ctx.Client, ":thumbsdown:", true) };
            var interactivity = ctx.Client.GetInteractivity();
            var random = new Random();

            List<String[]> messages = new List<String[]>(); //[0] = Name, [1] = Anime Name, [2] = Image URL

            //DxD

            string[] riasGremory = { "Rias Gremory", "High School DxD", "https://media.discordapp.net/attachments/735858039537795203/1019298372735221870/unknown.png?width=513&height=676" };
            messages.Add(riasGremory);

            string[] akenoHimejima = { "Akeno Himejima", "High School DxD", "https://media.discordapp.net/attachments/735858039537795203/1019299165299277884/unknown.png?width=356&height=676" };
            messages.Add(akenoHimejima);

            string[] asiaArgento = { "Asia Argento", "High School DxD", "https://media.discordapp.net/attachments/735858039537795203/1020104142989041734/unknown.png?width=504&height=676" };
            messages.Add(asiaArgento);

            string[] koneko = { "Koneko Toujou", "High School DxD", "https://media.discordapp.net/attachments/735858039537795203/1020104269287927818/unknown.png?width=260&height=260" };
            messages.Add(koneko);

            string[] tsubaki = { "Tsubaki", "High School DxD", "https://media.discordapp.net/attachments/735858039537795203/1020104400045355058/unknown.png?width=207&height=311" };
            messages.Add(tsubaki);

            string[] kuroka = { "Kuroka", "High School DxD", "https://media.discordapp.net/attachments/735858039537795203/1020104592022843402/unknown.png?width=528&height=675" };
            messages.Add(kuroka);

            string[] irina = { "Irina Shidou", "High School DxD", "https://media.discordapp.net/attachments/735858039537795203/1020104826270519346/unknown.png?width=332&height=461" };
            messages.Add(irina);

            string[] xenovia = { "Xenovia Quarta", "High School DxD", "https://media.discordapp.net/attachments/735858039537795203/1020104878636421201/unknown.png?width=314&height=461" };
            messages.Add(xenovia);

            //DAL

            string[] tohka;
            string[] kurumi;
            string[] kotori;
            string[] yoshino;
            string[] ellen;
            string[] kaguyaYamai;
            string[] yuzuruYamai;
            string[] natsumiAdult;
            string[] drMurasume;

            //DBZ, Super, GT

            string[] a18;
            string[] a21;
            string[] caulifla;
            string[] kale;
            string[] kefla;
            string[] launch;
            string[] yurin;
            string[] fasha;

            //Joku Forms

            string[] joku = { "Mommy Joku", "NiggerBall Z", "https://media.discordapp.net/attachments/735858039537795203/1019332220231614464/unknown.png?width=472&height=676" };
            messages.Add(joku);

            string[] ssjJoku = { "Super Nigger Joku", "NiggerBall Z", "https://media.discordapp.net/attachments/969707624784338995/979460300287910008/8B34305B-3D29-4EBB-86B8-3FA210A01FD6.jpg?width=312&height=676" };
            messages.Add(ssjJoku);

            string[] ssj3Joku = { "Super Nigger 3 Joku", "NiggerBall Z", "https://media.discordapp.net/attachments/969707624784338995/979460751574073395/F5756B87-AD97-41A4-B5FD-CA6A7F8A14ED.jpg?width=320&height=676" };
            messages.Add(ssj3Joku);

            string[] ssbJoku = { "Super Nigger Blue Joku", "NiggerBall Z", "https://media.discordapp.net/attachments/969707624784338995/979461591835770910/2B282B59-48F7-46C9-9141-F1FBF3100B32.jpg?width=385&height=676" };
            messages.Add(ssbJoku);

            string[] spJoku = { "Spirit Bomb Joku", "NiggerBall Z", "https://media.discordapp.net/attachments/735858039537795203/1019333744097767504/unknown.png?width=395&height=676" };
            messages.Add(spJoku);

            string[] xenoJoku = { "Xeno Nigger", "NiggerBall Z", "https://media.discordapp.net/attachments/969707624784338995/1019344977119154207/XENO_NIGGER_DOT.png?width=471&height=676" };
            messages.Add(xenoJoku);

            //Fate Characters

            string[] fsnKingArthur = { "King Arthur (Saber)", "Fate Stay/Night", "https://media.discordapp.net/attachments/735858039537795203/1019300735009165352/unknown.png?width=676&height=676" };
            messages.Add(fsnKingArthur);

            string[] astolfoSaber = { "Astolfo (Saber)", "Fate Grand Order", "https://media.discordapp.net/attachments/735858039537795203/1019335359961780337/unknown.png?width=477&height=676" };
            messages.Add(astolfoSaber);

            string[] neroClaudius = { "Nero Claudius", "Fate/EXTRA", "https://media.discordapp.net/attachments/735858039537795203/1020103687261130762/unknown.png?width=320&height=676" };
            messages.Add(neroClaudius);

            string[] neroClaudiusBride = { "Nero Claudius (Bride)", "Fate/EXTRA", "https://media.discordapp.net/attachments/735858039537795203/1020100866902073364/3ae2e463de874c3c450518f382c44f3d.jpg?width=483&height=676" };
            messages.Add(neroClaudiusBride);

            string[] neroClaudiusCaster = { "Nero Claudius (Caster)", "Fate Grand Order", "https://media.discordapp.net/attachments/735858039537795203/1020100787197714493/ed8b02c3f1635b964f79a3b6698bc1ecb6244471v2_hq.jpg?width=626&height=676" };
            messages.Add(neroClaudiusCaster);

            string[] kamaFGO;
            string[] astolfoRider;
            string[] rinToshaka;
            string[] ishtarFGO;
            string[] ereshFGO;
            string[] trueKingArthur;


            int index = random.Next(messages.Count);

            var testEmbed = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithAuthor("MID OR NOT MID?? | Cast your votes below") 
                .WithTitle("***" + messages[index][0] + "***")
                .WithDescription(messages[index][1])
                .WithImageUrl(messages[index][2])
                .WithColor(DiscordColor.Blue)
                );
            var pollMSG = await ctx.Channel.SendMessageAsync(testEmbed);

            foreach (var option in emojiOptions) 
            {
                await pollMSG.CreateReactionAsync(option);
            }

            var result = await interactivity.CollectReactionsAsync(pollMSG, duration);
            var dResult = result.Distinct();
            var results = dResult.Select(x => $"{x.Emoji}: {x.Total}");

            var resultsEmbed = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithTitle("***Results***")
                .WithColor(DiscordColor.Azure)
                .WithDescription(string.Join("\n", results))
                );
            await ctx.Channel.SendMessageAsync(resultsEmbed);
        }
    }
}
