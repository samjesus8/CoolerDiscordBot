using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Security.Cryptography.X509Certificates;

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
        public async Task MidOrNotMidRules(CommandContext ctx)
        {
            var rules = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithTitle("Mid or Not Mid Instructions")
                .WithColor(DiscordColor.Azure)
                .WithDescription("IMPORTANT: THE SYNTAX OF THE COMMAND IS \n ***'>mid TimeLimit'*** \n\n" +
                                    "The premise of the game is simple, a good looking anime girl will be displayed on screen \n" +
                                        "If you think she's mid, vote mid. Thumbs Down = Mid & Thumbs Up = Not Mid \n The most votes wins the game \n\n" +
                                            "Have fun and remember, don't make it serious, its just a fucking cartoon figure")
                .WithImageUrl("https://media.discordapp.net/attachments/735858039537795203/1019733895526219826/unknown.png?width=514&height=537")
                );
            await ctx.Channel.SendMessageAsync(rules);
        } //Rules

        [Command("mid")]
        public async Task MidOrNotMid(CommandContext ctx, TimeSpan duration)
        {
            DiscordEmoji[] emojiOptions = { DiscordGuildEmoji.FromName(ctx.Client, ":MID_EMOTE:", true), DiscordGuildEmoji.FromName(ctx.Client, ":NOT_MID_EMOTE:", true) };
            var interactivity = ctx.Client.GetInteractivity();
            var random = new Random();

            List<String[]> messages = new List<String[]>(); //[0] = Name, [1] = Anime Name, [2] = Image URL

            //DxD

            string[] riasGremory = { "Rias Gremory", "High School DxD", "https://media.discordapp.net/attachments/735858039537795203/1019298372735221870/unknown.png?width=513&height=676" };
            messages.Add(riasGremory);

            string[] akenoHimejima = { "Akeno Himejima", "High School DxD", "https://media.discordapp.net/attachments/735858039537795203/1019299165299277884/unknown.png?width=356&height=676" };
            messages.Add(akenoHimejima);

            string[] asiaArgento = { "Asia Argento", "High School DxD", "https://media.discordapp.net/attachments/1020110665161113610/1020111383964172358/asia.png?width=499&height=670" };
            messages.Add(asiaArgento);

            string[] koneko = { "Koneko Toujou", "High School DxD", "https://media.discordapp.net/attachments/1020110665161113610/1020111385377640448/koneko.png" };
            messages.Add(koneko);

            string[] tsubaki = { "Tsubaki", "High School DxD", "https://media.discordapp.net/attachments/1020110665161113610/1020111387223150682/tsubaki.png" };
            messages.Add(tsubaki);

            string[] kuroka = { "Kuroka", "High School DxD", "https://media.discordapp.net/attachments/1020110665161113610/1020111385960665139/kuroka.png?width=523&height=670" };
            messages.Add(kuroka);

            string[] irina = { "Irina Shidou", "High School DxD", "https://cdn.discordapp.com/attachments/1020110665161113610/1020111384916271135/ireena.png" };
            messages.Add(irina);

            string[] xenovia = { "Xenovia Quarta", "High School DxD", "https://media.discordapp.net/attachments/1020110665161113610/1020111387592245318/xenovia.png" };
            messages.Add(xenovia);

            //DAL

            string[] tohka = { "Tohka", "Date A Live", "https://media.discordapp.net/attachments/1020110665161113610/1020130641154678784/tohka.jpg?width=500&height=669" };
            messages.Add(tohka);

            string[] kurumi = { "Kurumi Tokisaki", "Date A Live", "https://media.discordapp.net/attachments/1020110665161113610/1020130640030597130/kurumi.jpg?width=558&height=670" };
            messages.Add(kurumi);

            string[] kotori = { "Kotori Itsuka", "Date A Live", "https://media.discordapp.net/attachments/1020110665161113610/1020130639632158771/kotori.jpg?width=376&height=669" };
            messages.Add(kotori);

            string[] yoshino = { "Yoshino", "Date A Live", "https://media.discordapp.net/attachments/1020110665161113610/1020130641490219088/yoshino.jpg?width=463&height=669" };
            messages.Add(yoshino);

            string[] ellen = { "Ellen Mathers", "Date A Live", "https://media.discordapp.net/attachments/1020110665161113610/1020130639074299904/ellen_mathers.jpg?width=658&height=670" };
            messages.Add(ellen);

            string[] kaguyaYamai = { "Kaguya Yamai", "Date A Live", "https://media.discordapp.net/attachments/1020110665161113610/1020130639317565533/kaguya.jpg" };
            messages.Add(kaguyaYamai);

            string[] yuzuruYamai = { "Yuzuru Yamai", "Date A Live", "https://media.discordapp.net/attachments/1020110665161113610/1020130641846730762/yuzuru.jpg" };
            messages.Add(yuzuruYamai);

            string[] natsumiAdult = { "Natsumi", "Date A Live", "https://media.discordapp.net/attachments/1020110665161113610/1020130640814936167/natsumi.jpg?width=376&height=669"};
            messages.Add(natsumiAdult);

            string[] drMurasume = { "Reine Murasume", "Date A Live", "https://media.discordapp.net/attachments/1020110665161113610/1020130640575864842/murasume.jpg?width=376&height=669" };
            messages.Add(drMurasume);

            //DBZ, Super, GT

            string[] a18 = { "Android 18", "DragonBall Z", "https://media.discordapp.net/attachments/1020110665161113610/1020122369836191785/a18.jpg?width=376&height=669" };
            messages.Add(a18);

            string[] a21 = { "Android 21", "DragonBall FighterZ", "https://media.discordapp.net/attachments/1020110665161113610/1020122370125594784/a21.jpg?width=376&height=669" };
            messages.Add(a21);

            string[] caulifla = { "Caulifla", "DragonBall Super", "https://media.discordapp.net/attachments/1020110665161113610/1020122370435981322/caulifla.png?width=566&height=670" };
            messages.Add(caulifla);

            string[] kale = { "Kale", "DragonBall Super", "https://media.discordapp.net/attachments/1020110665161113610/1020122371052544021/kale.jpg?width=376&height=669" };
            messages.Add(kale);

            string[] kefla = { "Kefla", "DragonBall Super", "https://media.discordapp.net/attachments/1020110665161113610/1020122371371307088/kefla.jpg?width=502&height=669" };
            messages.Add(kefla);

            string[] launch = { "Launch", "DragonBall", "https://media.discordapp.net/attachments/1020110665161113610/1020122371727831091/launch.jpg?width=502&height=669" };
            messages.Add(launch);

            string[] yurin = { "Yurin", "DragonBall Super", "https://media.discordapp.net/attachments/1020110665161113610/1020122372038213703/yuin.jpg?width=376&height=669" };
            messages.Add(yurin);

            string[] fasha = { "Fasha", "DragonBall Z", "https://media.discordapp.net/attachments/1020110665161113610/1020122370800877688/fasha.jpg?width=521&height=670" };
            messages.Add(fasha);

            string[] towa = { "Towa", "DragonBall Xenoverse 2", "https://media.discordapp.net/attachments/1020110665161113610/1021134845386162176/Towa.png?width=348&height=676" };
            messages.Add(towa);

            string[] supremeKaiTime = { "Supreme Kai of Time", "DragonBall Heroes", "https://media.discordapp.net/attachments/1020110665161113610/1021134844647968768/supremekaioftime.png?width=225&height=300" };
            messages.Add(supremeKaiTime);

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

            string[] neroClaudius = { "Nero Claudius", "Fate/EXTRA", "https://media.discordapp.net/attachments/1020110665161113610/1020111386384285768/nero.jpg?width=317&height=670" };
            messages.Add(neroClaudius);

            string[] neroClaudiusBride = { "Nero Claudius (Bride)", "Fate/EXTRA", "https://media.discordapp.net/attachments/1020110665161113610/1020111386765955192/nerobride.jpg?width=478&height=669" };
            messages.Add(neroClaudiusBride);

            string[] neroClaudiusCaster = { "Nero Claudius (Caster)", "Fate Grand Order", "https://media.discordapp.net/attachments/1020110665161113610/1020111384446505030/casternero.jpg?width=621&height=670" };
            messages.Add(neroClaudiusCaster);

            string[] kamaFGO = { "Kama", "Fate Grand Order", "https://media.discordapp.net/attachments/1020110665161113610/1020742165455978656/kama.jpg?width=478&height=676" };
            messages.Add(kamaFGO);

            string[] astolfoRider = { "Astolfo (Rider)", "Fate/Aprocypha", "https://media.discordapp.net/attachments/1020110665161113610/1020742164206059540/astolfo.jpg?width=478&height=676" };
            messages.Add(astolfoRider);

            string[] rinToshaka = { "Rin Toshaka", "Fate Stay/Night", "https://media.discordapp.net/attachments/1020110665161113610/1020742166181597184/rintoshaka.jpg?width=558&height=675" };
            messages.Add(rinToshaka);

            string[] ishtarFGO = { "Ishtar", "Fate Grand Order", "https://media.discordapp.net/attachments/1020110665161113610/1020742165212713100/ishtar.jpg?width=380&height=675" };
            messages.Add(ishtarFGO);

            string[] ereshFGO = { "Ereshkigal", "Fate Grand Order", "https://media.discordapp.net/attachments/1020110665161113610/1020742164621312121/ereshkigal.png?width=458&height=675" };
            messages.Add(ereshFGO);

            string[] trueKingArthur = { "Arthur Pendragon (Lancer)", "Fate Grand Order", "https://media.discordapp.net/attachments/1020110665161113610/1020742165829271663/lionking.png?width=481&height=675" };
            messages.Add(trueKingArthur);

            string[] jeanneDArc = { "Jeanne D'Arc", "Fate/Aprocypha", "https://media.discordapp.net/attachments/1020110665161113610/1021133189168758934/jeanne_darc.jpg?width=328&height=675" };
            messages.Add(jeanneDArc);

            string[] jeanneDArcAlter = { "Jeanne D'Arc (Alter)", "Fate Grand Order", "https://media.discordapp.net/attachments/1020110665161113610/1021133189776945152/JeanneAlter.png?width=461&height=652" };
            messages.Add(jeanneDArcAlter);

            string[] arthurAlter = { "King Arthur (Saber Alter)", "Fate Stay/Night", "https://media.discordapp.net/attachments/1020110665161113610/1021133190217355364/kingarthuralter.png?width=481&height=675" };
            messages.Add(arthurAlter);

            string[] sakura = { "Sakura Matou", "Fate Stay/Night", "https://media.discordapp.net/attachments/1020110665161113610/1021133190968123554/sakura.png?width=475&height=676" };
            messages.Add(sakura);

            string[] nitocris = { "Nitocris", "Fate Grand Order", "https://media.discordapp.net/attachments/1020110665161113610/1021133190632570992/Nitocris.png?width=461&height=652" };
            messages.Add(nitocris);

            string[] spaceIshtar = { "Space Ishtar", "Fate Grand Order", "https://media.discordapp.net/attachments/1020110665161113610/1021133191387545670/spaceishtar.png?width=380&height=676" };
            messages.Add(spaceIshtar);

            //Genshin

            string[] lumine = { "Lumine (Traveller)", "Genshin Impact", "https://media.discordapp.net/attachments/1020110665161113610/1020792605816135801/lumine_art.png?width=479&height=676" };
            messages.Add(lumine);

            string[] amber = { "Amber", "Genshin Impact", "https://media.discordapp.net/attachments/1020110665161113610/1020792605526720563/amber.jpg?width=477&height=676" };
            messages.Add(amber);

            string[] jean = { "Jean", "Genshin Impact", "https://media.discordapp.net/attachments/1020110665161113610/1021137635630137374/jean.png?width=422&height=675" };
            messages.Add(jean);

            string[] babara = { "Babara", "Genshin Impact", "https://media.discordapp.net/attachments/1020110665161113610/1021137634736750693/babara.jpg?width=515&height=675" };
            messages.Add(babara);

            string[] ganyu = { "Ganyu", "Genshin Impact", "https://media.discordapp.net/attachments/1020110665161113610/1021137635344912494/ganyu.jpg?width=1059&height=676" };
            messages.Add(ganyu);

            string[] venti = { "Venti", "Genshin Impact", "https://media.discordapp.net/attachments/1020110665161113610/1021137665795571803/venti.jpg?width=478&height=676" };
            messages.Add(venti);

            string[] yelan = { "Yelan", "Genshin Impact", "https://media.discordapp.net/attachments/1020110665161113610/1021137666043031803/yelan.jpg?width=422&height=675" };
            messages.Add(yelan);

            string[] ayaka = { "Kamisato Ayaka", "Genshin Impact", "https://media.discordapp.net/attachments/1020110665161113610/1021137634480889947/ayaka.jpg?width=478&height=676" };
            messages.Add(ayaka);

            string[] raidenShogun = { "Raiden Shogun", "Genshin Impact", "https://media.discordapp.net/attachments/1020110665161113610/1021137637639200919/raiden.jpg?width=1201&height=676" };
            messages.Add(raidenShogun);

            string[] paimon = { "Paimon", "Genshin Impact", "https://media.discordapp.net/attachments/1020110665161113610/1021137637253316618/paimon.png?width=380&height=676" };
            messages.Add(paimon);

            string[] nilou = { "Nilou", "Genshin Impact", "https://media.discordapp.net/attachments/1020110665161113610/1021137636552872057/nilou.png?width=448&height=675" };
            messages.Add(nilou);

            string[] eula = { "Eula", "Genshin Impact", "https://media.discordapp.net/attachments/1020110665161113610/1021137635084882041/eula.jpg?width=380&height=675" };
            messages.Add(eula);

            string[] ningguang = { "Ningguang", "Genshin Impact", "https://media.discordapp.net/attachments/1020110665161113610/1021137636942950460/ningguang.jpg?width=380&height=676" };
            messages.Add(ningguang);

            string[] klee = { "Klee", "Genshin Impact", "https://media.discordapp.net/attachments/1020110665161113610/1021137636024385776/klee.jpg?width=478&height=676" };
            messages.Add(klee);

            //Other

            string[] walter = { "Walter White", "Breaking Bad", "https://media.discordapp.net/attachments/1020110665161113610/1020783882594942976/walter.png?width=175&height=234" };
            messages.Add(walter);

            string[] saulGoodman = { "Saul", "Breaking Bad", "https://media.discordapp.net/attachments/1020110665161113610/1020784615989968906/saul.png?width=461&height=461" };
            messages.Add(saulGoodman);


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

        [Command("passive")]
        public async Task GuessThePassive(CommandContext ctx) 
        {
            var random = new Random();
            var interactivity = ctx.Client.GetInteractivity();
            var timeLimit = TimeSpan.FromSeconds(20);

            List<string[]> passivesList = new List<string[]>(); //[0] = Name of Unit, [1] = Passive, [2] ImageURL

            //LRs

            string[] tapionMinosha = { "True Heroes: Tapion & Minotia", "ATK & DEF +130%; Storied Figures Category allies' Ki +2 and ATK & DEF +20%; " +
                                                                            "Siblings' Bond Category allies' Ki +2 and ATK & DEF +20%; medium chance of launching an additional Super Attack when performing an Ultra Super Attack; " +
                                                                                "reduces damage received by 13% within the same turn with each Super Attack performed", 
                                                 "https://media.discordapp.net/attachments/1020832099068018748/1020832124074467348/unknown.png?width=382&height=527" };

            passivesList.Add(tapionMinosha);

            string[] brolyTrio = { "A new life on Vampa: Broly, Cheelai & Lemo", "ATK & DEF +15% per Ki Sphere obtained; plus an additional ATK & DEF +5% and Ki +2 per Ki Sphere with 2 or more PHY Ki Spheres obtained; " +
                                                                                    "all allies' ATK +39% with 2 or more AGL or STR Ki Spheres obtained; all allies' DEF +39% with 2 or more TEQ or INT Ki Spheres obtained; " +
                                                                                        "evades enemy's attack (including Super Attack) with 7 or more Ki Spheres obtained",
                                                    "https://media.discordapp.net/attachments/1020832099068018748/1020836752434417714/unknown.png?width=365&height=516"};
            passivesList.Add(brolyTrio);

            var index = random.Next(passivesList.Count);

            var messageBuilder = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithTitle("Guess this passive")
                .WithDescription(passivesList[index][1])
                );
            var messageCheck = await ctx.Channel.SendMessageAsync(messageBuilder);

            var wait = await interactivity.CollectReactionsAsync(messageCheck, timeLimit);

            var answer = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                .WithTitle("The unit was: " + passivesList[index][0])
                .WithImageUrl(passivesList[index][2])
                );
            await ctx.Channel.SendMessageAsync(answer);
        }
    }
}
