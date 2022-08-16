using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;

namespace DiscordBotTest.Commands
{
    public class Tools : BaseCommandModule
    {
        [Command("setactivity")]
        public async Task SetBotStatus(CommandContext ctx, string message) 
        {
            if (ctx.User.Id == 572877986223751188 || ctx.User.Id == 327845261692895232) 
            {
                DiscordActivity activity = new DiscordActivity();
                DiscordClient discord = ctx.Client;

                activity.Name = message;
                await discord.UpdateStatusAsync(activity);
                return;
            }
            else 
            {
                var notAllowed = new DiscordMessageBuilder()
                    .AddEmbed(
                    new DiscordEmbedBuilder()
                    .WithTitle("You are not @𝕤𝕒𝕞.𝕛𝕖𝕤𝕦𝕤𝟠#6825 or @Delet#9054 so get denied m8")
                    .WithDescription("If you want access to this command ask @𝕤𝕒𝕞.𝕛𝕖𝕤𝕦𝕤𝟠#6825")
                    );
                await ctx.Channel.SendMessageAsync(notAllowed);
                return; 
            }
        }
    }
}
