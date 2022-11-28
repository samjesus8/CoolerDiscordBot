using DiscordBotTest.Builders;
using DiscordBotTest.InternalBuilders;
using DSharpPlus;
using DSharpPlus.CommandsNext.Attributes;
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
                                                                    [Option("Rarity", "Is your unit an TUR/LR/TUR(EZA)/ LR(EZA)")] string Rarity,
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

            if (linksList.Length > 7) //Check to see if user has provided at least 7 Links
            {
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, linksFail);
            }
            else if (Links == "null")
            {
                Links = "No active Links";
            }

            var passiveMessage = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                .WithColor(DiscordColor.Azure)
                .WithTitle("***Passive Generator by 𝕤𝕒𝕞.𝕛𝕖𝕤𝕦𝕤𝟠#6825***")
                .WithDescription("Your entry is as following: \n" +
                                    "**GENERAL DETAILS** \n\n" +
                                    "Passive Name: " + PassiveName + "\n" +
                                    "Passive Author: " + user + "\n" +
                                    "Rarity: " + Rarity + "\n\n" +
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
                    .WithDescription("You can now use **'/usepassive " + PassiveName + "'** to test your passive \n" +
                                     "Or you can view the list of passives you have created by using **'/passivelist'** ")
                    );
                await ctx.Channel.SendMessageAsync(storingMessage);

                var storage = new DokkanUserPassiveBuilder(ctx.User.Username, PassiveName, Rarity, (int)BaseHPValue, (int)BaseATKValue, (int)BaseDEFValue, LeaderSkillName, (int)LeaderSkillValue, (int)ATKPassive, (int)DEFPassive, (int)DMGReductionValue, (int)SupportAllies, Links);
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

                var calculator = new DokkanPassiveCalculator(info.Rarity, info.UnitHP, info.UnitATK, info.UnitDEF, info.UnitLeaderSkill, info.UnitPassiveATK, info.UnitPassiveDEF, info.Support, info.DmgReduction, info.Links);

                var message = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()

                    .WithColor(DiscordColor.Azure)
                    .WithTitle("***Your Stats for " + info.PassiveName + "***")
                    .WithDescription("***PLEASE NOTE THAT ALL PASSIVES ARE CALCULATED ASSUMING THAT THE UNIT IS AT \n" +
                                     "Rainbow, Supreme DMG(TURs), 12/24 KI (Colossal/Mega-Colossal DMG for LRs)*** \n\n" +
                                     calculator.ResultATK + "\n" +
                                     calculator.ResultDEF)
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
                                    "Passive Name: " + check.PassiveName + "\n" +
                                    "Passive Author: " + check.UserName + "\n" +
                                    "Rarity: " + check.Rarity + "\n\n" +
                                    "**Base Card Values** \n\n" +
                                    "Base HP: " + check.UnitHP + "\n" +
                                    "Base ATK: " + check.UnitATK + "\n" +
                                    "Base DEF: " + check.UnitDEF + "\n\n" +
                                    "**Leader Skill** \n\n" +
                                    "Leader Skill Name: " + check.UnitLeaderName + "\n" +
                                    "Leader Skill Buff (%): " + check.UnitLeaderSkill + "\n\n" +
                                    "**Passive Details** \n\n" +
                                    "TOTAL ATK Buff (%): " + check.UnitPassiveATK + "\n" +
                                    "TOTAL DEF Buff (%): " + check.UnitPassiveDEF + "\n\n" +
                                    "**Optional Buffs** \n\n" +
                                    "Damage Reduction (%): " + check.DmgReduction + "\n" +
                                    "Support Buffs from allies (%): " + check.Support + "\n" +
                                    "Links: " + check.Links)

                    );

                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, passiveDetailsMSG);
            }
        }

        [SlashCommand("deletepassive", "Delete a passive from your list")]
        public async Task DeletePassive(InteractionContext ctx, [Option("PassiveName", "Name of the passive you want to delete")] string PassiveName)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("Starting..."));
            var PassiveDetails = new DokkanUserPassiveBuilder();
            PassiveDetails.GetSpecificPassive(PassiveName);

            var failedMessage = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()

                .WithColor(DiscordColor.Red)
                .WithTitle("The command failed")
                .WithDescription("You cannot delete someone else's passive \n" +
                                 "Owner of Passive '" + PassiveDetails.UserName + "' is not equal to '" + ctx.User.Username + "'")
                );


            if (PassiveDetails.UserName == ctx.User.Username)
            {
                bool action = PassiveDetails.DeleteSpecificPassive(PassiveDetails);

                if (action == true)
                {
                    var successMsg = new DiscordMessageBuilder()
                        .AddEmbed(new DiscordEmbedBuilder()

                        .WithColor(DiscordColor.Green)
                        .WithTitle("Success")
                        .WithDescription("Your passive was deleted")
                        );
                    await ctx.Channel.SendMessageAsync(successMsg);
                }
                else
                {
                    var failedMsg = new DiscordMessageBuilder()
                        .AddEmbed(new DiscordEmbedBuilder()

                        .WithColor(DiscordColor.Red)
                        .WithTitle("Error")
                        .WithDescription("Your passive was not deleted \n" +
                                         "Error Message: " + PassiveDetails.Error)
                        );
                    await ctx.Channel.SendMessageAsync(failedMsg);
                }
            }
            else
            {
                await ctx.Channel.SendMessageAsync(failedMessage);
            }
        }

        [SlashCommand("viewlinks", "View a list of avalible links to use or search for a specific link")]
        public async Task LinksBrowser(InteractionContext ctx, [Option("LinkName", "Specify the Link you want to search for. Leave 'null' for the whole list")] string LinkName)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("Starting..."));
            var LinkList = new DokkanLinks();
            var loadLinks = LinkList.LoadLinks();

            if (loadLinks == true)
            {
                if (LinkName != "null")
                {
                    //Search for specific link
                    var linkToSearch = LinkList.Links.TryGetValue(LinkName, out var link);
                    if (linkToSearch == true) 
                    {
                        var linkMsg = new DiscordMessageBuilder()
                            .AddEmbed(new DiscordEmbedBuilder()

                            .WithColor(DiscordColor.Azure)
                            .WithTitle("Link Details for: " + LinkName)
                            .WithDescription("ATK - " + link.ATK + "% \n" +
                                             "DEF - " + link.DEF + "%")
                            );
                        await ctx.Channel.SendMessageAsync(linkMsg);
                    }
                    else 
                    {
                        var linkFailedMsg = new DiscordMessageBuilder()
                            .AddEmbed(new DiscordEmbedBuilder()

                            .WithTitle($"Failed to get link with name '{LinkName}'")
                            .WithDescription("Please refer to the file in the GitHub repo 'CoolerDiscordBot/InfoFiles/Links.txt' for a list of avalible links")
                            );
                        await ctx.Channel.SendMessageAsync(linkFailedMsg);
                    }
                }
                else if (LinkName == "null")
                {
                    var output = string.Join("\n", LinkList.Links.Select(pair => String.Format("{0} = {1}, {2}", pair.Key.ToString(), pair.Value.ATK.ToString(), pair.Value.DEF.ToString())));

                    var listMsg = new DiscordMessageBuilder()
                        .AddEmbed(new DiscordEmbedBuilder()

                        .WithColor(DiscordColor.Azure)
                        .WithTitle("List of Avalible links to use in the bot \n\n**Name = ATK, DEF (In % Values)**")
                        .WithDescription(output)
                        );
                    await ctx.Channel.SendMessageAsync(listMsg);
                }
            }
            else
            {
                var failedMsg = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()

                    .WithColor(DiscordColor.Red)
                    .WithTitle("Error")
                    .WithDescription("Links failed to load \n" +
                                     "Error Message: " + LinkList.Error)
                    );
                await ctx.Channel.SendMessageAsync(failedMsg);
            }
        }

        [SlashCommand("addlink", "Add link to system")]
        [RequireOwner]
        [Hidden]
        public async Task LinksAdd(InteractionContext ctx, [Option("LinkName", "Name of Link to add")] string LinkName,
                                                           [Option("ATK", "Value of ATK Buff")] long ATK,
                                                           [Option("DEF", "Value of DEF Buff")] long DEF) 
        {
            if (ctx.User.Id == 572877986223751188) 
            {
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("Starting..."));
                var LinksList = new DokkanLinks(LinkName, ATK, DEF);
                bool action = LinksList.StoreLinks(LinksList);

                if (action == true)
                {
                    var successMsg = new DiscordMessageBuilder()
                        .AddEmbed(new DiscordEmbedBuilder()

                        .WithColor(DiscordColor.Green)
                        .WithTitle("Success")
                        .WithDescription("Link Stored")
                        );
                    await ctx.Channel.SendMessageAsync(successMsg);
                }
                else
                {
                    var failedMsg = new DiscordMessageBuilder()
                        .AddEmbed(new DiscordEmbedBuilder()

                        .WithColor(DiscordColor.Red)
                        .WithTitle("Error")
                        .WithDescription("Link Failed to store \n" +
                                         "Error Message: " + LinksList.Error)
                        );
                    await ctx.Channel.SendMessageAsync(failedMsg);
                }
            }
            else 
            {
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("Only the owner can use this command"));
            }
        }
    }
}
