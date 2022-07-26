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
using Newtonsoft.Json;

namespace DiscordBotTest.Commands
{
    public class Help : BaseCommandModule
    {
        [Command("help")]
        public async Task HelpMenu(CommandContext ctx) 
        {
            var mainMenuBuilder = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithTitle("Cooler Is Gay Bot - Made by Samuel J \n HELP MENU")
                .WithDescription("This is a multi-utility bot which just features random stuff \n Click on a category to view its list of commands")
                )
                .AddComponents(new DiscordComponent[] 
                {
                    new DiscordButtonComponent(ButtonStyle.Primary, "calculatorFunction", "Calculator"),
                    new DiscordButtonComponent(ButtonStyle.Primary, "funFunction", "Fun Commands"),
                    new DiscordButtonComponent(ButtonStyle.Danger, "exitFunction", "Exit")
                }
                );

            await ctx.Channel.SendMessageAsync(mainMenuBuilder);

            ctx.Client.ComponentInteractionCreated += async (a, b) =>
            {

            };
        }
    }
}
