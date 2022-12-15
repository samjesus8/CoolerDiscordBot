using DiscordBotTest.InternalBuilders;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Threading.Tasks;

namespace DiscordBotTest.Commands
{
    public class TheInfiniteUniverse : BaseCommandModule
    {
        //COMMANDS ONLY FOR THE INFINITE UNIVERSE

        [Command("whip")]
        public async Task WhippingTask(CommandContext ctx, DiscordUser user)
        {
            if (ctx.Guild.Id == 922382235334750259) //ONLY USED IN THE INFINITE UNIVERSE SERVER
            {
                var whipMSG = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()

                    .WithColor(DiscordColor.Azure)
                    .WithTitle(ctx.User.Username + " whipped " + user.Username)
                    .WithImageUrl("https://cdn.discordapp.com/attachments/1020110665161113610/1047277797237854218/toby-kunta-kinte.gif")
                    );
                await ctx.Channel.SendMessageAsync(whipMSG);
            }
            else if (ctx.Guild.Id == 1015010557591572560)
            {
                var whipMSG = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()

                    .WithColor(DiscordColor.Azure)
                    .WithTitle(ctx.User.Username + " whipped " + user.Username)
                    .WithImageUrl("https://cdn.discordapp.com/attachments/1020110665161113610/1047277797237854218/toby-kunta-kinte.gif")
                    );
                await ctx.Channel.SendMessageAsync(whipMSG);
            }
            else
            {
                var deniedMSG = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()

                    .WithColor(DiscordColor.Red)
                    .WithTitle("Sorry but you cannot use this command in this server \n" +
                               "This can only be used in 'The Infinite Universe'")
                    );
                await ctx.Channel.SendMessageAsync(deniedMSG);
            }
        }

        [Command("fuck")]
        public async Task FuckCommand(CommandContext ctx, DiscordUser user)
        {
            //VIEWER DISCRETION. THIS CLASS CONTAINS NSFW IMAGES FOR ITS ASSOCIATED COMMANDS
            //ALL COMMANDS THAT REQUIRE NSFW HAVE BEEN THOROUGLY CHECKED AND WILL NOT EXECUTE IN ANY OTHER CHANNEL THAT IS NOT NSFW
            //PLEASE PROCEED WITH THIS IN MIND, IT IS YOUR DECISION NOT THE DEVELOPER'S

            var GIF = new NSFWGif();
            if (ctx.Channel.IsNSFW != true)
            {
                var notNSFW = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()

                    .WithColor(DiscordColor.DarkRed)
                    .WithTitle("Sorry this command cannot be used here")
                    .WithDescription("You can only use this command in NSFW channels")
                    );
                await ctx.Channel.SendMessageAsync(notNSFW);
            }
            else
            {
                var random = new Random();
                int index = random.Next(GIF.gifLinks.Count);

                var msg = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()

                    .WithColor(DiscordColor.DarkRed)
                    .WithTitle(ctx.User.Username + " fucked " + user.Username)
                    .WithDescription("Hope you had a good time getting railed")
                    .WithImageUrl(GIF.gifLinks[index])
                    );
                await ctx.Channel.SendMessageAsync(msg);
            }
        }
    }
}
