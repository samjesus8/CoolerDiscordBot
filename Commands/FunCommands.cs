using DiscordBotTest.Builders;
using DiscordBotTest.Handlers.Dialogue;
using DiscordBotTest.Handlers.Dialogue.Steps;
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
        public async Task ShutTheFuckUp(CommandContext ctx, DiscordUser UserName)
        {
            var builder1 = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithColor(DiscordColor.Azure)
                .WithTitle("Shut The Fuck Up " + UserName.Username.ToString())
                .WithDescription("I don't recall the universe ever asking bozo ffs")
                );
            await ctx.Channel.SendMessageAsync(builder1);
        }

        [Command("stfu")]
        public async Task ShutTheFuckUp(CommandContext ctx, string UserName)
        {
            var builder1 = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithColor(DiscordColor.Azure)
                .WithTitle("Shut The Fuck Up " + UserName)
                .WithDescription("I don't recall the universe ever asking bozo ffs")
                );
            await ctx.Channel.SendMessageAsync(builder1);
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
                .WithColor(DiscordColor.Azure)
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
                .WithColor(DiscordColor.Azure)
                .WithTitle("To " + user)
                .WithDescription("WATCH YOUR FUCKING TONE MFER")
                .WithImageUrl("https://cdn.discordapp.com/emojis/1001335925655212062.png?size=1024")
                .WithAuthor("From: " + ctx.User.Username)
                .WithColor(DiscordColor.Black)
                );
            await ctx.Channel.SendMessageAsync(toneMessage);
        }

        [Command("choosetone")] //For user pings
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
                .WithColor(DiscordColor.Azure)
                .WithTitle("Tone Chooser")
                .WithColor(DiscordColor.Azure)
                .WithDescription("Out of all the mfers that " + ctx.User.Username + " chose \n\n" + "***" + userList[index].Username + "***" + " should watch their tone!!!")
                .WithImageUrl("https://cdn.discordapp.com/emojis/1001335925655212062.png?size=1024")
                );

            await ctx.Channel.SendMessageAsync(toneMessage);
        }

        [Command("choosetone")] //For any string
        public async Task ToneChooserString(CommandContext ctx, params string[] users) 
        {
            var random = new Random();
            int index = random.Next(users.Length);

            var toneMessage = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()

                .WithTitle("Tone Chooser")
                .WithColor(DiscordColor.Azure)
                .WithDescription("Out of all the mfers that " + ctx.User.Username + " chose \n\n" + "***" + users[index] + "***" + " should watch their tone!!!")
                .WithImageUrl("https://cdn.discordapp.com/emojis/1001335925655212062.png?size=1024")
                );
            await ctx.Channel.SendMessageAsync(toneMessage);
        }

        [Command("supernova")]
        public async Task Cooler8Ball(CommandContext ctx, params string[] text) 
        {
            var random = new Random();
            string completeText = string.Join(" ", text);

            List<string> responses = new List<string>();

            responses.Add("Probably");
            responses.Add("Are u retarded");
            responses.Add("Never in the history of the world");
            responses.Add("Instead of answering you, imma tell u to WATCH YOUR TONE!!!!");
            responses.Add("If its about Cooler, No");
            responses.Add("If its about Cooler, Yes");
            responses.Add("If Coola#5784 is mentionned here, then the answer is never");
            responses.Add("Maybe");
            responses.Add("In 2 weeks, Yes");
            responses.Add("Yes");
            responses.Add("No");

            int index = random.Next(responses.Count);

            var message = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()

                .WithColor(DiscordColor.DarkRed)
                .WithTitle("Cooler's Supernova of Death")
                .WithDescription("***Question:*** " + completeText + " \n" +
                                 "***Answer:*** " + responses[index])
                );
            await ctx.Channel.SendMessageAsync(message);
        }

        [Command("fortune")]
        [Cooldown(5, 43200, CooldownBucketType.User)]
        public async Task FortuneTeller(CommandContext ctx) 
        {
            if (ctx.Guild.Id == 922382235334750259) //Checks if it was called from The Infinite Universe
            {
                var random = new Random();
                var FortuneList = new FortuneStorage();
                FortuneList.AddFortunesTheInfiniteUniverse();

                var fortuneList = FortuneList.Fortunes;
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
            else if (ctx.Guild.Id == 1015010557591572560) 
            {
                var random = new Random();
                var FortuneList = new FortuneStorage();
                FortuneList.AddFortunesTheInfiniteUniverse();

                var fortuneList = FortuneList.Fortunes;
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
            else //Post normal fortunes for other servers
            {
                var random = new Random();
                var FortuneList = new FortuneStorage();
                FortuneList.AddFortunesGlobalServers();

                var fortuneList = FortuneList.Fortunes;
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

        [Command("cooler")]
        [Cooldown(10, 60, CooldownBucketType.User)]
        public async Task CoolerInsultMachine(CommandContext ctx) 
        {
            var random = new Random();

            List<string> coolerInsults = new List<string>();

            coolerInsults.Add("你这个臭黑鬼，一直叫叫叫，笨黑鬼去自杀");
            coolerInsults.Add("You are a fucking idiot");
            coolerInsults.Add("Make sure to watch your tone");
            coolerInsults.Add("闭嘴黑鬼 没有人跟你讲话 不要打嘴");
            coolerInsults.Add("You will get shafted in Legends");
            coolerInsults.Add("How is that Red LF Cooler doing eh. He must like getting raped by Beast Gohan");
            coolerInsults.Add("You can't do maths mfer");
            coolerInsults.Add("Watch your tone shitface");
            coolerInsults.Add("You are a cunt");
            coolerInsults.Add("You are a twat");
            int index = random.Next(1, coolerInsults.Count);

            var message = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()

                .WithColor(DiscordColor.Azure)
                .WithTitle("**Coola#5784**")
                .WithDescription(coolerInsults[index])
                );

            await ctx.Channel.SendMessageAsync(message);
        }
    }
}
