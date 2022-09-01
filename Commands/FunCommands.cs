using DiscordBotTest.Handlers.Dialogue;
using DiscordBotTest.Handlers.Dialogue.Steps;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
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
                .WithTitle("Cooler Is Gay")
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
    }
}
