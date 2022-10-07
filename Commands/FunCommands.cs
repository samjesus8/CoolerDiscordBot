using DiscordBotTest.Handlers.Dialogue;
using DiscordBotTest.Handlers.Dialogue.Steps;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscordBotTest.Commands
{
    public class FunCommands : BaseCommandModule
    {
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
            var deletMessage = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithTitle("This is 100% Facts, Delet is never wrong")
                .WithDescription("Midgends Middkan Midcha Mid Everything Midcord Mid Mid Mid L Bozo")
                );
            await ctx.Channel.SendMessageAsync(deletMessage);
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

        [Command("watchyourtone")] //For user pings
        public async Task WatchYourTone(CommandContext ctx, DiscordUser user) 
        {
            var toneMessage = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithTitle("To " + user.Username)
                .WithDescription("WATCH YOUR FUCKING TONE MFER")
                .WithImageUrl("https://cdn.discordapp.com/emojis/1001335925655212062.png?size=1024")
                .WithAuthor("From: " + ctx.User.Username)
                .WithColor(DiscordColor.Black)
                );
            await ctx.Channel.SendMessageAsync(toneMessage);
        }

        [Command("watchyourtone")] //For any string
        public async Task WatchYourTone(CommandContext ctx, string user)
        {
            var toneMessage = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithTitle("To " + user)
                .WithDescription("WATCH YOUR FUCKING TONE MFER")
                .WithImageUrl("https://cdn.discordapp.com/emojis/1001335925655212062.png?size=1024")
                .WithAuthor("From: " + ctx.User.Username)
                .WithColor(DiscordColor.Black)
                );
            await ctx.Channel.SendMessageAsync(toneMessage);
        }

        [Command("choosetone")]
        public async Task ToneChooser(CommandContext ctx, params DiscordUser[] users) 
        {
            var random = new Random();
            List<DiscordUser> userList = new List<DiscordUser>();

            foreach (var user in users) 
            {
                userList.Add(user);
            }

            int index = random.Next(userList.Count);

            var toneMessage = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                .WithTitle("Tone Chooser")
                .WithColor(DiscordColor.Azure)
                .WithDescription("Out of all the mfers that " + ctx.User.Username + " chose \n\n" + "***" + userList[index].Username + "***" + " should watch their tone!!!")
                .WithImageUrl("https://cdn.discordapp.com/emojis/1001335925655212062.png?size=1024")
                );

            await ctx.Channel.SendMessageAsync(toneMessage);
        }

        [Command("fortune")]
        [Cooldown(5, 60, CooldownBucketType.User)]
        public async Task FortuneTeller(CommandContext ctx) 
        {
            var random = new Random();
            string[] badUnits = { "STR Beerus", "TEQ Corrupted Zamasu", "PHY Buu", "STR Janemba", "INT Janemba", "STR Broly (Not the LR)",
                                    "PHY Broly", "TEQ Perfect Form Cell", "INT Boujack", "TEQ Raditz", "STR Kefla", "INT Android 16",
                                        "AGL Super Vegito", "PHY Vegito Blue", "AGL Pan", "AGL SSJ3 Goku" };
            int badUnitsIndex = random.Next(badUnits.Length);

            List<string> fortuneList = new List<string>();

            //Infinite Universe

            fortuneList.Add("You will have zero bitches");
            fortuneList.Add("You will get SSBE on your next multi in Dokkan");
            fortuneList.Add("Joku is gonna come to your house tommorow at 5AM");
            fortuneList.Add("You will get shafted when your favorite unit comes out in either Dokkan/Legends");
            fortuneList.Add("There's an autistic baboon at your window");
            fortuneList.Add("The next Dokkan-Fest Banner you summon on, you will get unfeatured SSRs no matter what");
            fortuneList.Add("You will get bitches");
            fortuneList.Add("You will pull the new LR that's currently out");
            fortuneList.Add("Raditz will come to your house and rad all over your struggles");
            fortuneList.Add("You will do a radillion damage next turn on Red Zone");
            fortuneList.Add("You don't deserve any fortunes");
            fortuneList.Add("Your predictions for the next LR/Dokkan-Fest will be true");
            fortuneList.Add("Joku will end up next to you in bed");
            fortuneList.Add("Tommorrow its gonna be morbin time, get ready to have the best day of your life");
            fortuneList.Add("The person who sent the latest message in edcord. You're fucking gay, leave the server u bozo");
            fortuneList.Add("If you get this fortune, Shallot ain't going SSB");
            fortuneList.Add("Delet will get bitches once he graduates");

            fortuneList.Add("Because of you, Main will get shafted in the next multi he does on dokkan");
            fortuneList.Add("Because of you, Vein will get shafted in the next multi he does on dokkan");
            fortuneList.Add("Because of you, Coola will get shafted in the next multi he does on dokkan");
            fortuneList.Add("Because of you, Cloud will get shafted in the next multi he does on dokkan");
            fortuneList.Add("Because of you, Delet will get shafted in the next multi he does on dokkan");
            fortuneList.Add("Because of you, Ducky will get shafted in the next multi he does on dokkan");
            fortuneList.Add("Because of you, Sam will get shafted in the next multi he does on dokkan");

            fortuneList.Add("If you managed to get this fortune, Loved has a skill issue");
            fortuneList.Add("The next multi you do in dokkan, you will get " + badUnits[badUnitsIndex]);
            fortuneList.Add("The Queen's revive skill will now activate");
            fortuneList.Add("If you get this fortune, you are able to create your own fortune and add it to this list. Please ping @𝕤𝕒𝕞.𝕛𝕖𝕤𝕦𝕤𝟠#6825 with your fortune");
            fortuneList.Add("If you have Legends installed, you are a moron");
            fortuneList.Add("One day, Cooler will return to Discord because of something really dumb");
            fortuneList.Add("The next anime you watch, will have a trap in it");
            fortuneList.Add("You will inevitably get banned from this server because of something stupid");
            fortuneList.Add("ATK & DEF +150%; plus an additional ATK & DEF +50% when performing a Super Attack if facing only 1 enemy and if that enemy's HP is 50% or less when the character performs a Super Attack, " +
                                "plus an additional ATK +100% and high chance of performing a critical hit; performs a critical hit within the same turn after receiving an attack plus an additional DEF +150% when facing 2 or more enemies; Ki +2 for the rest of battle after delivering a final blow");

            //Requested Fortunes

            fortuneList.Add("Joku will go beyond Super Nigger Blue");
            fortuneList.Add("If you get this fortune, you will become as dumb as Cooler");
            fortuneList.Add("Cooler will come to your house thinking there is pussy over there but instead it was just dicks");
            fortuneList.Add("If you get this fortune, you will never enjoy a Dragon Ball Game again");
            fortuneList.Add("Joku will now get attacked by a wild bear");
            fortuneList.Add("Ash will never get shafted in any gacha game");
            fortuneList.Add("Next fortune you do will have the opposite effect");
            fortuneList.Add("If you get this fortune, you will forever suffer from Copium");
            fortuneList.Add("If you get this then Rak should watch his tone");
            fortuneList.Add("Yes");
            fortuneList.Add("No");
            fortuneList.Add("If you get this fortune, Watch Your Tone");
            fortuneList.Add("Upon starting the day on a Sunday, you will gain the ability to ATK & DEF +140%; Ki +1 at start of each turn (up to +3); guards all attacks; " +
                                "plus an additional ATK +40% within the same turn when guard is activated");
            fortuneList.Add("If u get this fortune, tell Coola#5784 to watch his damn tone for maining LoE");
            fortuneList.Add("If you get this fortune then Coola#5784 should watch his tone");

            //Sigma Quotes

            fortuneList.Add("Instead of trying to blend in, Stand out and never blend in");
            fortuneList.Add("Confuse them with your silence. Shock them with your actions");
            fortuneList.Add("80% of boys have Girlfriends, the rest have a brain. Separate yourself from society and you will be above them");
            fortuneList.Add("A true man denies society's standards and expectations");
            fortuneList.Add("People say you can’t live without love… I think oxygen is more important!");
            fortuneList.Add("Don’t avoid them when they’re angry. They’ll run to you when the temperature rises");
            fortuneList.Add("You are already a king. It’s simply a matter of finding out the nature of your kingdom");
            fortuneList.Add("Put more of your energy into listening than talking");
            fortuneList.Add("Always be true to who you are, and ignore what other people have to say about you");
            fortuneList.Add("Until his dream comes true, a man cannot relax");
            fortuneList.Add("Never Fear Anyone, Never Trust Anyone, Never Depend On anyone");
            fortuneList.Add("The future depends on what we do in the present");
            fortuneList.Add("Don't Quit. Suffer now and live the rest of your life above those who made you suffer");
            fortuneList.Add("Education wil make you a living. Self Education makes you a fortune");
            fortuneList.Add("The worst thing for a rich person to have: \n\n A wife");
            fortuneList.Add("The less we deserve good fortune, the more we hope for it");
            fortuneList.Add("Never do things for the earth's pleasure. Do it for yourself");
            fortuneList.Add("The day you disband yourself from today's society, you separate yourself from confinement into the truth");
            fortuneList.Add("The best way out is always through");
            fortuneList.Add("In a gentle way, you can shake the world. You don't have to be famous, or have a world changing idea");
            fortuneList.Add("Today is the only day. Yesterday is gone");
            fortuneList.Add("Give light and people will find the way");
            fortuneList.Add("Believe in living today. Not in yesterday, nor in tomorrow");
            fortuneList.Add("When deeds speak, words are nothing");
            fortuneList.Add("Silence is the last thing the world will ever hear from you");

            int index = random.Next(fortuneList.Count);

            var fortuneMessage = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithTitle("***Cooler's Daily Fortunes for " + ctx.User.Username.ToString() + "***")
                .WithDescription("***" + fortuneList[index] + "***")
                .WithColor(DiscordColor.Blue)
                );
            await ctx.Channel.SendMessageAsync(fortuneMessage);
        }
    }
}
