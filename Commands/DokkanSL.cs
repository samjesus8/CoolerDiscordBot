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
        [SlashCommand("passivecreate", "A Dokkan Passive Creator tool")]
        public async Task DokkanPassiveGen(InteractionContext ctx, [Option("PassiveName", "Give your passive a name")] string PassiveName,
                                                                    [Option("HP", "Base HP Value of your Card")] long BaseHPValue,
                                                                    [Option("ATK", "Base ATK Value of your Card")] long BaseATKValue,
                                                                    [Option("DEF", "Base DEF Value of your Card")] long BaseDEFValue,
                                                                    [Option("LeaderSkill", "% Value of your Leader Skill")] long LeaderSkillValue,
                                                                    [Option("PassiveATK", "Total % ATK in passive")] long ATKPassive,
                                                                    [Option("PassiveDEF", "Total % DEF in passive")] long DEFPassive) 
        {
            var message = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()

                .WithTitle("The command is working")
                );
            await ctx.Channel.SendMessageAsync(message);
        }

        [SlashCommand("usepassive", "Use your passive and generate some stats (Must be the SAME NAME) ")]
        public async Task UsePassive(InteractionContext ctx, [Option("PassiveName", "Your PassiveName that you used in /passivecreate")] string PassiveName) 
        {
            await ctx.Channel.SendMessageAsync("poo");
        }
    }
}
