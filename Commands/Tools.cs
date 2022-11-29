using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System.Threading.Tasks;

namespace DiscordBotTest.Commands
{
    public class Tools : BaseCommandModule
    {
        [Command("status")]
        public async Task SetBotStatus(CommandContext ctx, params string[] message) 
        {
            if (ctx.User.Id == 572877986223751188 || ctx.User.Id == 327845261692895232) 
            {
                string finalMessage = string.Join(" ", message);
                DiscordActivity activity = new DiscordActivity();
                DiscordClient discord = ctx.Client;

                activity.Name = finalMessage;
                await discord.UpdateStatusAsync(activity);
                return;
            }
            else 
            {
                var notAllowed = new DiscordMessageBuilder()
                    .AddEmbed(
                    new DiscordEmbedBuilder()
                    .WithTitle("Access Denied")
                    .WithDescription("If you want access to this command ask @𝕤𝕒𝕞.𝕛𝕖𝕤𝕦𝕤𝟠#6825")
                    );
                await ctx.Channel.SendMessageAsync(notAllowed);
                return; 
            }
        }

        [Command("invite")]
        public async Task SendInviteLink(CommandContext ctx) 
        {
            var inviteMessage = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithTitle("Invite the bot to other servers using this link")
                .WithDescription("Click on the title to open the invite link")
                .WithUrl("https://discord.com/api/oauth2/authorize?client_id=1001273087158919228&permissions=534790011200&scope=bot%20applications.commands")
                );
            await ctx.Channel.SendMessageAsync(inviteMessage);
        }

        [Command("server")]
        public async Task SendServerLink(CommandContext ctx) 
        {
            var discordMessage = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                .WithTitle("Official Discord Server")
                .WithColor(DiscordColor.Azure)
                .WithDescription("Join the Official Discord server for the bot \n" +
                                    "Click the title to join!!")
                .WithUrl("https://discord.gg/kfaVgHN7zv")
                );
            await ctx.Channel.SendMessageAsync(discordMessage);
        }

        [Command("timestamp")]
        public async Task Response(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();

            var timestampBuilder = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithTitle("Type in and send a message")
                .WithDescription("The command will then send back the exact time you sent the message")
                );
            await ctx.Channel.SendMessageAsync(timestampBuilder);


            var message = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel);
            await ctx.Channel.SendMessageAsync("Your message was sent at: " + message.Result.Timestamp.ToString());
        }

        [Command("avatar")]
        public async Task GetUserAvatar(CommandContext ctx, DiscordUser user = null) 
        {
            if (user == null) //If no user is provided, show the avatar of the user who executed it
            {
                var nullAvatar = ctx.User.AvatarUrl;

                var message = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()

                    .WithColor(DiscordColor.Azure)
                    .WithTitle("Avatar for " + ctx.User.Username)
                    .WithImageUrl(nullAvatar)
                    );

                await ctx.Channel.SendMessageAsync(message);
            }
            else 
            {
                var userAvatar = user.AvatarUrl;
                var message = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()

                    .WithColor(DiscordColor.Azure)
                    .WithTitle("Avatar for " + user.Username)
                    .WithImageUrl(userAvatar)
                    );

                await ctx.Channel.SendMessageAsync(message);
            }
        }

        [Command("membercount")]
        public async Task GetServerMemberCount(CommandContext ctx) 
        {
            var message = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()

                .WithColor(DiscordColor.Azure)
                .WithTitle("Server Member Count for " + ctx.Guild.Name.ToString())
                .WithDescription(ctx.Guild.MemberCount.ToString())
                );

            await ctx.Channel.SendMessageAsync(message);
        }

        [Command("version")]
        public async Task VersionShow(CommandContext ctx) 
        {
            var MSG = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()

                .WithColor(DiscordColor.Azure)
                .WithTitle("Current Version")
                .WithDescription("**V1.5.7**")
                .WithFooter("For more information on this version, type in '>changelog latest'")
                );

            await ctx.Channel.SendMessageAsync(MSG);
        }

        [Command("github")]
        public async Task GitHub(CommandContext ctx) 
        {
            var msg = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()

                .WithColor(DiscordColor.Azure)
                .WithTitle("GitHub Repository for the Bot")
                .WithDescription("Click the title to access the GitHub repository for this bot")
                .WithUrl("https://github.com/samjesus8/CoolerDiscordBot")
                .WithImageUrl("https://media.discordapp.net/attachments/1020110665161113610/1046892445209731132/monke_middle_finger.jpg?width=518&height=670")
                );
            await ctx.Channel.SendMessageAsync(msg);
        }

        [Command("dokkanwindows")]
        public async Task GitHubDokkan(CommandContext ctx) 
        {
            var msg = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()

                .WithColor(DiscordColor.Azure)
                .WithTitle("GitHub Repository for the **Dokkan Passive Generator - Windows Version**")
                .WithDescription("Click the title to access the GitHub repository for the Windows Edition of the Dokkan Passive Generator" +
                " featured in this bot. This application is based off the Generator but in a Windows Forms style application with an easy-to-use GUI \n\n" +
                "To download this application go to the **'Releases'** area and download the Setup.exe of the latest version")
                .WithUrl("https://github.com/samjesus8/DokkanPassiveGenerator")
                .WithImageUrl("https://media.discordapp.net/attachments/1020110665161113610/1046892807241093211/85b85dd4-55f1-455c-bc5a-e5df541598bb.png?width=115&height=115")
                );
            await ctx.Channel.SendMessageAsync(msg);
        }
    }
}
