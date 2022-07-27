using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity.Extensions;

namespace DiscordBotTest.Commands
{
    public class Games : BaseCommandModule
    {
        [Command("maths")]
        public async Task MathsGame(CommandContext ctx) 
        {
            var message = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithTitle("According to Cooler, what does pussy look like")
                .WithDescription("Options are below:")
                )
                .AddComponents(
                new DiscordButtonComponent(ButtonStyle.Primary, "dick", "Dick"),
                new DiscordButtonComponent(ButtonStyle.Primary, "plane", "Plane")
                );
            await ctx.Channel.SendMessageAsync(message);

            ctx.Client.ComponentInteractionCreated += async (a, b) =>
            {
                if (b.Interaction.Data.CustomId == "dick")
                {
                    await ctx.Channel.SendMessageAsync("Correct Answer");
                    Environment.Exit(0);
                    
                }
                if (b.Interaction.Data.CustomId == "plane")
                {
                    await ctx.Channel.SendMessageAsync("The hell how does pussy look like planes. Oh wait cooler fucks planes");
                    Environment.Exit(0);
                }
            };
        }
    }
}
