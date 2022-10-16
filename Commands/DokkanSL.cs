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
                                                                    [Option("LeaderSkillName", "Give your Leader skill a name")] string LeaderSkillName,
                                                                    [Option("LeaderSkill", "% Value of your Leader Skill")] long LeaderSkillValue,
                                                                    [Option("PassiveATK", "Total % ATK in passive")] long ATKPassive,
                                                                    [Option("PassiveDEF", "Total % DEF in passive")] long DEFPassive,
                                                                    [Option("DMGReduction", "% Value of Damage Reduction")] long DMGReductionValue,
                                                                    [Option("Support", "Total % Value of Support Buffs from Allies")] long SupportAllies,
                                                                    [Option("Links", "MAX 7 LINKS AND MUST BE SEPARATED BY 1 SPACE")] string Links) 
        {
            var linksFail = new DiscordInteractionResponseBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                .WithColor(DiscordColor.Red)
                .WithTitle("Command Failed")
                .WithDescription("You entered too many links. The max is 7 links per passive")
                );

            string user = ctx.User.Username.ToString();
            string[] linksList = Links.Split(' ');

            if (linksList.Length > 7) //Check to see if user has provided exactly 7 Links
            {
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, linksFail);
            }

            var passiveMessage = new DiscordInteractionResponseBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                .WithColor(DiscordColor.Azure)
                .WithTitle("***Passive Generator by 𝕤𝕒𝕞.𝕛𝕖𝕤𝕦𝕤𝟠#6825***")
                .WithDescription("Your entry is as following: \n" +
                                    "**GENERAL DETAILS** \n\n" +
                                    "Passive Name: " + PassiveName + "\n\n" +
                                    "Passive Author: " + user + "\n\n" +
                                    "**Base Card Values** \n\n" +
                                    "Base HP: " + BaseHPValue + "\n" +
                                    "Base ATK: " + BaseATKValue + "\n" +
                                    "Base DEF: " + BaseDEFValue + "\n\n" +
                                    "**Leader Skill** \n\n" +
                                    "Leader Skill Name: " + LeaderSkillName + "\n" +
                                    "Leader Skill Buff (%): " + LeaderSkillValue + "\n\n" +
                                    "**Passive Details** \n\n" +
                                    "TOTAL ATK Buff (%): " + ATKPassive + "\n" +
                                    "TOTAL DEF Buff (%): " + DEFPassive + "\n" +
                                    "TOTAL DMG REDUCTION (%): " + DMGReductionValue + "\n\n" +
                                    "**Optional Buffs** \n\n" +
                                    "Support Buffs from allies (%): " + SupportAllies + "\n" +
                                    "Links: " + Links + "\n\n" +
                                    "**PLEASE CONFIRM YOU WANT TO CREATE A UNIT WITH THESE DETAILS**"));

            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, passiveMessage);                                                                                
        }

        [SlashCommand("usepassive", "Use your passive and generate some stats (Must be the SAME NAME)")]
        public async Task UsePassive(InteractionContext ctx, [Option("PassiveName", "Your PassiveName that you used in /passivecreate")] string PassiveName) 
        {
            await ctx.Channel.SendMessageAsync("poo");
        }

        [SlashCommand("passivelist", "View a list of your passives that you created")]
        public async Task ViewPassiveList(InteractionContext ctx, [Option("User", "Specify the user you want to view the list for")] DiscordUser user,
                                                                  [Option("PassiveName", "Name of Passive to view or type 'null' for the list")] string PassiveName) 
        {
            string discordUserName = user.Username.ToString();

            if (PassiveName == "null")
            {
                var userPassiveListMSG = new DiscordInteractionResponseBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()

                    .WithColor(DiscordColor.Azure)
                    .WithTitle("Passive List for User " + discordUserName)
                    );

                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, userPassiveListMSG);
            }
            else 
            {
                var passiveDetailsMSG = new DiscordInteractionResponseBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()

                    .WithColor(DiscordColor.Azure)
                    .WithAuthor("You are viewing a passive that was made by " + discordUserName)
                    .WithTitle("Passive Details for " + "**" + PassiveName + "**")
                    );

                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, passiveDetailsMSG);
            }
        }
    }
}
