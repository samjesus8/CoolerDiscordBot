using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.EventArgs;
using Newtonsoft.Json;

namespace DiscordBotTest.Commands
{
    public class Help : BaseCommandModule
    {
        [Command("cooler")]
        public async Task TestCommand(CommandContext ctx) 
        {
            ctx.Channel.SendMessageAsync("Cooler Is Gay").ConfigureAwait(false);
        }
    }
}
