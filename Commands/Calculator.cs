using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace DiscordBotTest.Commands
{
    public class Calculator : BaseCommandModule
    {
        [Command("add")]
        public async Task Addition(CommandContext ctx, int number1, int number2) 
        {
            await ctx.Channel.SendMessageAsync("I can't do maths cause im a dumb fuck");
            await ctx.Channel.SendMessageAsync((number1 + number2).ToString());
        }

        [Command("subtract")]
        public async Task Subtract(CommandContext ctx, int number1, int number2) 
        {
            await ctx.Channel.SendMessageAsync((number1 - number2).ToString());
        }

        [Command("multiply")]
        public async Task Multiply(CommandContext ctx, int number1, int number2) 
        {
            await ctx.Channel.SendMessageAsync((number1 * number2).ToString());
        }

        [Command("divide")]
        public async Task Divide(CommandContext ctx, double number1, double number2) 
        {
            await ctx.Channel.SendMessageAsync((number1 / number2).ToString());
        }
    }
}
