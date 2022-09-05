using DiscordBotTest.Handlers.Dialogue;
using DiscordBotTest.Handlers.Dialogue.Steps;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
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

        [Command("watchyourtone")]
        public async Task WatchYourTone(CommandContext ctx, DiscordUser user) 
        {
            var toneMessage = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithTitle(user.Username)
                .WithDescription("WATCH YOUR FUCKING TONE MFER")
                .WithImageUrl("https://cdn.discordapp.com/emojis/1001335925655212062.png?size=1024")
                .WithAuthor("From: " + ctx.User.Username)
                );
            await ctx.Channel.SendMessageAsync(toneMessage);
        }

        [Command("fortune")]
        public async Task FortuneTeller(CommandContext ctx) 
        {
            var random = new Random();

            List<string> fortuneList = new List<string>();

            fortuneList.Add("You will have zero bitches");
            fortuneList.Add("Tommorow you will get shafted");
            fortuneList.Add("You will get SSBE on your next multi in Dokkan");
            fortuneList.Add("Joku is gonna come to your house tommorow at 5AM");
            fortuneList.Add("You will get shafted when your favorite unit comes out in either Dokkan/Legends");
            fortuneList.Add("There's an autistic baboon at your window");
            fortuneList.Add("Person below will get LF Beerus in their next multi on a Step Up Banner");
            fortuneList.Add("The person above and below should watch their tone");
            fortuneList.Add("The next Dokkan-Fest Banner you summon on, you will get unfeatured SSRs no matter what");
            fortuneList.Add("The next time you summon on genshin, you will not get a 5* unit");
            fortuneList.Add("You will get bitches");
            fortuneList.Add("You will pull the new LR that's currently out");
            fortuneList.Add("You will ratio someone");
            fortuneList.Add("You will get ratioed by someone you hate");
            fortuneList.Add("Raditz will come to your house and rad all over your struggles");
            fortuneList.Add("You will do a radillion damage next turn on Red Zone");
            fortuneList.Add("You don't deserve any fortunes");
            fortuneList.Add("You will one day be able to ratio everyone in this server");
            fortuneList.Add("The person who previously got banned will one day be richer than all of us");
            fortuneList.Add("Your predictions for the next LR/Dokkan-Fest will be true");
            fortuneList.Add("Joku will end up next to you in bed");
            fortuneList.Add("Tommorrow its gonna be morbin time, get ready to have the best day of your life");
            fortuneList.Add("The person who sent the latest message in edcord. You're fucking gay, leave the server u bozo");
            fortuneList.Add("If you do link-levelling, you won't get any units to LL10 in 50 years");
            fortuneList.Add("If you play legends, you will end up uninstalling the game next month");

            int index = random.Next(fortuneList.Count);

            var fortuneMessage = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithTitle("Cooler's Daily Fortunes")
                .WithDescription(fortuneList[index])
                );

            await ctx.Channel.SendMessageAsync(fortuneMessage);
        }
    }
}
