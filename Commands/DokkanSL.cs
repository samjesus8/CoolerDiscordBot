using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace DiscordBotTest.Commands
{
    public class DokkanSL : ApplicationCommandModule
    {
        [SlashCommand("test", "Test Command")]
        public async Task SlashCommand(InteractionContext ctx, [Option("option1", "Option1Desc")] string var1, [Option("option2", "Option2Desc")] string var2) 
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent(var1 + " " + var2));
        }

        [SlashCommand("passivegen", "A Dokkan Passive generator")]
        public async Task DokkanPassiveGen(InteractionContext ctx) 
        {

        }
    }
}
