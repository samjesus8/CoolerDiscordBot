using DiscordBotTest.Builders;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
using System;
using System.Linq;
using System.Threading.Tasks;

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
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("Starting..."));

            DiscordEmoji[] emojis = { DiscordEmoji.FromName(ctx.Client, ":thumbsup:", true), DiscordEmoji.FromName(ctx.Client, ":thumbsdown:", true) };
            var interactivity = ctx.Client.GetInteractivity();

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
            else if (Links == "null") 
            {
                Links = "No Links Provided by the user";
            }

            var passiveMessage = new DiscordMessageBuilder()
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
                                    "TOTAL DMG REDUCTION (%): " + "CURRENTLY UNDER DEVELOPMENT" + "\n\n" +
                                    "**Optional Buffs** \n\n" +
                                    "Support Buffs from allies (%): " + SupportAllies + "\n" +
                                    "Links: " + "CURRENTLY UNDER DEVELOPMENT" + "\n\n" +
                                    "**PLEASE CONFIRM YOU WANT TO CREATE A UNIT WITH THESE DETAILS**"));

            var confirmation = await ctx.Channel.SendMessageAsync(passiveMessage);

            foreach (var emoji in emojis) 
            {
                await confirmation.CreateReactionAsync(emoji);
            }

            var timeout = TimeSpan.FromMinutes(2);
            var confirm = await confirmation.WaitForReactionAsync(ctx.User, timeout);

            if (ctx.User == confirm.Result.User && confirm.Result.Emoji == emojis[0])
            {
                var storingMessage = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()

                    .WithColor(DiscordColor.Green)
                    .WithTitle("Storing your passive")
                    .WithDescription("You can now use '/usepassive " + PassiveName + " to test your passive \n" +
                                     "Or you can view the list of passives you have created by using '/passivelist' ")
                    );
                await ctx.Channel.SendMessageAsync(storingMessage);

                var storage = new DokkanUserPassiveBuilder(ctx.User.Username, PassiveName, (int)BaseHPValue, (int)BaseATKValue, (int)BaseDEFValue, LeaderSkillName, (int)LeaderSkillValue, (int)ATKPassive, (int)DEFPassive, (int)SupportAllies, Links);
                storage.StoreUserPassives(storage);
                
            }
            else if (ctx.User == confirm.Result.User && confirm.Result.Emoji == emojis[1]) 
            {
                var commandCancelled = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()

                    .WithColor(DiscordColor.Red)
                    .WithTitle("Command has been cancelled")
                    .WithDescription("Please use the command again if you want to rewrite your entry")
                    );

                await ctx.Channel.SendMessageAsync(commandCancelled);
            }
            else
            {
                var wrongUserMessage = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()

                    .WithColor(DiscordColor.Green)
                    .WithTitle("Sorry, only " + ctx.User.Username + " can do this")
                    .WithDescription("Other users cannot use an instance of a command that someone else is using \n" +
                                        "Please call the command yourself to use the functions")
                    );

                await ctx.Channel.SendMessageAsync(wrongUserMessage);
            }
        }

        [SlashCommand("usepassive", "Use your passive and generate some stats (Must be the SAME NAME)")]
        public async Task UsePassive(InteractionContext ctx, [Option("PassiveName", "Your PassiveName that you used in /passivecreate")] string PassiveName) 
        {
            var info = new DokkanUserPassiveBuilder(PassiveName); //Getting passive from provided name
            try
            {
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("Starting..."));

                var calculator = new DokkanPassiveCalculator(info.UnitHP, info.UnitATK, info.UnitDEF, info.UnitLeaderSkill, info.UnitPassiveATK, info.UnitPassiveDEF);

                var ATK = calculator.GetATK(info.UnitATK, info.UnitLeaderSkill, info.UnitPassiveATK);
                var DEF = calculator.GetDEF(info.UnitDEF, info.UnitLeaderSkill, info.UnitPassiveDEF);

                var message = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()

                    .WithColor(DiscordColor.Azure)
                    .WithTitle("Your Stats for " + info.PassiveName)
                    .WithDescription("**Main Stats** \n\n" +
                                     "ATK Stat When supering: " + ATK + "\n" +
                                     "DEF Pre Super: " + DEF.Item1 + "\n\n" +
                                     DEF.Item2)
                    );

                await ctx.Channel.SendMessageAsync(message);
            }
            catch (Exception ex)
            {
                var errorMessage = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()

                    .WithColor(DiscordColor.Red)
                    .WithTitle("Error")
                    .WithDescription(info.Error + "\n\n" +
                                     "Error in Code: \n\n" + ex.Message)
                    );

                await ctx.Channel.SendMessageAsync(errorMessage);
            }

        }

        [SlashCommand("passivelist", "View a list of your passives that you created")]
        public async Task ViewPassiveList(InteractionContext ctx, [Option("User", "Specify the user you want to view the list for")] DiscordUser user,
                                                                  [Option("PassiveName", "Name of Passive to view or type 'null' for the list")] string PassiveName) 
        {
            string discordUserName = user.Username.ToString();
            var check = new DokkanUserPassiveBuilder(discordUserName, PassiveName);

            string[] tempList = new string[check.membersJSONList.Count];
            string output;

            if (PassiveName == "null")
            {
                for (int i = 0; i < check.membersJSONList.Count; i++) 
                {
                    tempList[i] = check.membersJSONList[i].PassiveName.ToString();
                }
                output = string.Join("\n", tempList);

                var userPassiveListMSG = new DiscordInteractionResponseBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()

                    .WithColor(DiscordColor.Azure)
                    .WithTitle("Passive List for User " + discordUserName + "\n\n" +
                               output)
                    );

                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, userPassiveListMSG);
            }
            else 
            {
                check.GetSpecificPassive(PassiveName);

                var passiveDetailsMSG = new DiscordInteractionResponseBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()

                    .WithColor(DiscordColor.Azure)
                    .WithAuthor("You are viewing a passive that was made by " + discordUserName)
                    .WithTitle("Passive Details for " + "**" + PassiveName + "**")
                    .WithDescription("**GENERAL DETAILS** \n\n" +
                                    "Passive Name: " + check.PassiveName + "\n\n" +
                                    "Passive Author: " + check.UserName + "\n\n" +
                                    "**Base Card Values** \n\n" +
                                    "Base HP: " + check.UnitHP + "\n" +
                                    "Base ATK: " + check.UnitATK + "\n" +
                                    "Base DEF: " + check.UnitDEF + "\n\n" +
                                    "**Leader Skill** \n\n" +
                                    "Leader Skill Name: " + check.UnitLeaderName + "\n" +
                                    "Leader Skill Buff (%): " + check.UnitLeaderSkill + "\n\n" +
                                    "**Passive Details** \n\n" +
                                    "TOTAL ATK Buff (%): " + check.UnitPassiveATK + "\n" +
                                    "TOTAL DEF Buff (%): " + check.UnitPassiveDEF + "\n" +
                                    "TOTAL DMG REDUCTION (%): " + "CURRENTLY UNDER DEVELOPMENT" + "\n\n" +
                                    "**Optional Buffs** \n\n" +
                                    "Support Buffs from allies (%): " + check.Support + "\n" +
                                    "Links: " + "CURRENTLY UNDER DEVELOPMENT")

                    );

                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, passiveDetailsMSG);
            }
        }
    }
}
