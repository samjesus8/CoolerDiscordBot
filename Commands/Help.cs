using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Threading.Tasks;

namespace DiscordBotTest.Commands
{
    public class Help : BaseCommandModule
    {
        [Command("help")] //This command displays the Categories to use the actual help command
        public async Task HelpMenu(CommandContext ctx)
        {
            var mainMenuBuilder = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithColor(DiscordColor.Azure)
                .WithTitle("Cooler Is Gay Bot | Made by 𝕤𝕒𝕞.𝕛𝕖𝕤𝕦𝕤𝟠#6825")
                .WithDescription("This is a multi-utility bot which just features random stuff \n" +
                                 "The prefix is '>' \n\n" +

                                        "To use the help command you have to specify the type of help you want to use after you type in the command. Here is the list of categories: \n\n" +
                                        "**Calculator Help -> '>help Calculator'** \n" +
                                        "**Fun Commands Help -> '>help Fun'** \n" +
                                        "**Games Help -> '>help Games'** \n" +
                                        "**Tools/Utility Help -> '>help Tools'**")
                .WithImageUrl("https://media.discordapp.net/attachments/969707624784338995/1017188281819086940/unknown.png?width=479&height=268")
                .WithFooter("The day Cooler left Discord for good")
                );
            await ctx.Channel.SendMessageAsync(mainMenuBuilder);
        }

        [Command("help")]
        public async Task HelpMenu(CommandContext ctx, string helpType)
        {
            if (helpType == "Calculator" || helpType == "calculator")
            {
                var basicFunctionMessage = new DiscordMessageBuilder()
                    .AddEmbed(
                    new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("Basic Calculator Functions | **Imagine Cooler trying to do maths seriously!!!**")
                    .WithDescription(">add -> Add 2 numbers together | E.g: >add 2 2, returns 4 \n\n " +
                                    ">subtract -> Subtract 2 numbers together | E.g: >subtract 4 3, returns 1 \n\n " +
                                    ">multiply -> Multiply 2 numbers together | E.g: >multiply 6 4, returns 24 \n\n " +
                                    ">divide -> Divide 2 numbers together | E.g: >divide 5 2, returns 2.5 \n\n " +
                                    ">circlearea -> Gives you the area of a circle with any radius | E.g: >circlearea 2, returns 12.57")
                    );
                await ctx.Channel.SendMessageAsync(basicFunctionMessage);
            }

            if (helpType == "Fun" || helpType == "fun")
            {
                var funFunctionMessage = new DiscordMessageBuilder()
                    .AddEmbed(
                    new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("Fun Commands")
                    .WithDescription(">ducky -> Tell Ducky to shut the fuck up \n\n" +
                                    ">tory -> Slander the torys \n\n" +
                                    ">delet -> Prove that everything is mid with this one command \n\n " +
                                    ">dialogue -> The bot will send you a DM, type anything random to send it back to the channel where you used the command \n\n" +
                                    ">watchyourtone -> Tell someone to watch their tone | Syntax: >watchyourtone @User \n\n" +
                                    ">fortune -> See if your fate is lucky or will it be hell")
                    );
                await ctx.Channel.SendMessageAsync(funFunctionMessage);
            }

            if (helpType == "Games" || helpType == "games")
            {
                var gamesFunctionMessage = new DiscordMessageBuilder()
                    .AddEmbed(
                    new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("Game Commands")
                    .WithDescription(">cardgame -> A simple card game. If you draw higher than the bot, you win the game \n\n " +
                                    ">lottery -> Play the lottery, pick 5 numbers from 1-100 and test your luck \n" +
                                    "Syntax: >lottery num1 num2 num3 num4 num5 \n " +
                                    "Do >lottery on its own to view the instructions \n\n" +
                                    ">mid -> Play a game of Mid or Not Mid. A girl will show up on the screen \n" +
                                    "Server Members can vote if they think the girl is mid or not mid. After the time period, the most votes wins that round \n" +
                                    "Syntax: >mid TIMESPAN (>mid 5s) \n\n" +
                                    ">passive -> Play a game of guess the Passive. A Dokkan Passive skill will be shown on screen. If you guess who the unit is correctly you win")
                    );
                await ctx.Channel.SendMessageAsync(gamesFunctionMessage);
            }

            if (helpType == "Tools" || helpType == "tools")
            {
                var toolsFunctionMessage = new DiscordMessageBuilder()
                    .AddEmbed(
                    new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("Tools Commands")
                    .WithDescription(">timestamp -> After using this command, the next message you send the bot will return the exact time and date you sent it \n\n " +
                    ">status -> Only Sam and Delet can use this command. Sets the 'Playing' status of the bot to any text. There cannot be any spaces \n\n " +
                    ">invite -> Generates an invite link for the bot, use it to add it to other servers of your choice \n\n" +
                    ">changelog -> View the bot changelog. Shows what changed in every update \n\n" +
                    "Use >changelog on its own to view the full list or type in a version to view its specific changes like ***'>changelog 1.1'*** \n" +
                    "Type in ***>changelog latest'*** to view the changelog of the latest version")
                    );
                await ctx.Channel.SendMessageAsync(toolsFunctionMessage);
            }
        }

        [Command("changelog")]
        public async Task ChangeLogFull(CommandContext ctx)
        {
            var changeLogEmbed = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithAuthor("***This bot was made by @𝕤𝕒𝕞.𝕛𝕖𝕤𝕦𝕤𝟠#6825***")
                .WithColor(DiscordColor.Azure)
                .WithTitle("***Changelog | The latest version is V1.4.1***")
                .WithDescription("V1.4.1: \n\n" +
                                    "-Added some more characters into the >passive command \n\n" +

                                 "V1.4: \n\n" +
                                    "-Introducting the '>passive' command!!!!! \n\n" +
                                    "This command was requested by the man Delet#9054 himself. It is a guessing game based off Dokkan Unit Passive Skills \n" +
                                    "All you have to do is simply call the command. You will be displayed a random passive skill of a UR/LR unit. Server members have 20 seconds to guess \n" +
                                    "After 20 seconds the answer will be displayed in an embedded message. Have fun testing your dokkan knowledge!!!! \n\n " +

                                 "V1.3.4: \n\n" +
                                    "-Added some more characters to the list for the >mid command \n" +
                                    "-Changed the reactions for the >mid command to custom emotes \n" +
                                    "-Started Development for the >passive command (COMING SOON) \n\n" +

                                 "V1.3.3: \n\n" +
                                    "-Added any requested fortunes from users \n" +
                                    "-Added some more fortunes to the list \n" +
                                    "-Fixed any wrong info in any of the menus \n" +
                                    "-Added some more characters to >mid list \n\n" +

                                 "V1.3.2: \n\n" +
                                    "-Added any requested fortunes from users \n" +
                                    "-Fixed any wrong info in the >help section \n\n" +

                                 "V1.3.1: \n\n" +
                                    "-Added any requested fortunes from users \n" +
                                    "-Added a new changelog feature where you can view specific versions or the full log (See ***>help tools*** for more info) \n" +
                                    "-Added more entries to the >mid command and fixed errors pertaining to the results: \n\n" +

                                    "Fixed and redesigned the way results are shown \n" +
                                    "Removed the need to add your own emotes as a paramater in the command. Users only need to set the time \n\n" +

                                    "Removed >lottorules and integrated it with >lottery to view the rules and play the game on the same command \n" +
                                    "Removed >midrules and integrated with >mid to view instructions for same command \n\n" +

                                 "V1.3: \n\n" +
                                    "-Added JayVezzy and Brandon's requested fortunes to the list as well as some new fortunes. Also removed certain fortunes" +
                                    "as they are irrelavant or not needed \n\n" +
                                    "-Introducing the '>mid' command -> This is a new game introduced into the bot. A anime girl will be shown on screen. Server members" +
                                    " can vote if they think the girl is mid or not mid. \n" +
                                    "You can set your own emojis for members to react to, as well as a custom time period (Normally 5-10s) \n" +
                                    "Once the time period is over, results will be shown and the highest votes wins the round \n\n" +
                                    "Over time, more entries will be added to the list for this gamemode. If you would like to submit an entry please DM @𝕤𝕒𝕞.𝕛𝕖𝕤𝕦𝕤𝟠#6825 \n\n" +

                                 "V1.2.1: \n\n" +
                                    "-Added JayVezzy's fortunes to the list of fortunes \n" +
                                    "-Fixed the Help command by correcting a few mistakes in the information \n\n" +

                                 "V1.2: \n\n" +
                                    "-Redesigned the >help command -> Removed the buttons and instead added a condition for each category \n" +
                                    "-Cleanup of the code -> Removed unnecesary usings \n" +
                                    "-Removed the button to draw the card on >cardgame. You just have to call the command and it'll start automatically \n" +
                                    "-Added more fortunes to the >fortune command \n" +
                                    "-Addition of the >changelog command \n\n" +

                                 "V1.1: \n\n" +
                                    "-Code Cleanup \n" +
                                    "-Removed the >cooler command \n" +
                                    "-Addition of the >fortune command \n" +
                                    "-Increased the number limit from 50 to 100 in the >lottery command \n")
                );
            await ctx.Channel.SendMessageAsync(changeLogEmbed);
        }

        [Command("changelog")]
        public async Task ChangeLogSpecific(CommandContext ctx, string version) 
        {
            if (version == "1.1") 
            {
                var message = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("V1.1 Changelog")
                    .WithDescription("-Code Cleanup \n" +
                                    "-Removed the >cooler command \n" +
                                    "-Addition of the >fortune command \n" +
                                    "-Increased the number limit from 50 to 100 in the >lottery command")
                                   
                    );

                await ctx.Channel.SendMessageAsync(message);
            }
            if (version == "1.2") 
            {
                var message = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("V1.2 Changelog")
                    .WithDescription("-Redesigned the >help command -> Removed the buttons and instead added a condition for each category \n" +
                                    "-Cleanup of the code -> Removed unnecesary usings \n" +
                                    "-Removed the button to draw the card on >cardgame. You just have to call the command and it'll start automatically \n" +
                                    "-Added more fortunes to the >fortune command \n" +
                                    "-Addition of the >changelog command")
                    );

                await ctx.Channel.SendMessageAsync(message);
            }
            if (version == "1.2.1") 
            {
                var message = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("V1.2.1 Changelog")
                    .WithDescription("-Added JayVezzy's fortunes to the list of fortunes \n" +
                                    "-Fixed the Help command by correcting a few mistakes in the information")
                    );

                await ctx.Channel.SendMessageAsync(message);
            }
            if (version == "1.3") 
            {
                var message = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("V1.3 Changelog")
                    .WithDescription("-Added JayVezzy and Brandon's requested fortunes to the list as well as some new fortunes. Also removed certain fortunes" +
                                    "as they are irrelavant or not needed \n\n" +
                                    "-Introducing the '>mid' command -> This is a new game introduced into the bot. A anime girl will be shown on screen. Server members" +
                                    " can vote if they think the girl is mid or not mid. \n" +
                                    "You can set your own emojis for members to react to, as well as a custom time period (Normally 5-10s) \n" +
                                    "Once the time period is over, results will be shown and the highest votes wins the round \n\n" +
                                    "Over time, more entries will be added to the list for this gamemode. If you would like to submit an entry please DM @𝕤𝕒𝕞.𝕛𝕖𝕤𝕦𝕤𝟠#6825")
                    );

                await ctx.Channel.SendMessageAsync(message);
            }
            if (version == "1.3.1") 
            {
                var message = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("V1.3.1 Changelog")
                    .WithDescription("-Added any requested fortunes from users \n" +
                                    "-Added more entries to the >mid command and fixed errors pertaining to the results: \n\n" +

                                    "Fixed and redesigned the way results are shown \n" +
                                    "Removed the need to add your own emotes as a paramater in the command. Users only need to set the time \n\n" +

                                    "Removed >lottorules and integrated it with >lottery to view the rules and play the game on the same command \n" +
                                    "Removed >midrules and integrated with >mid to view instructions for same command")
                    );

                await ctx.Channel.SendMessageAsync(message);
            }
            if (version == "1.3.2") 
            {
                var message = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithAuthor("***The latest version is V1.3.2***")
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("V1.3.2 Changelog")
                    .WithDescription("V1.3.2: \n\n" +
                                    "-Added any requested fortunes from users \n" +
                                    "-Fixed any wrong info in the >help section")
                    );
                await ctx.Channel.SendMessageAsync(message);
            }
            if (version == "1.3.3") 
            {
                var message = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("V1.3.3 Changelog")
                    .WithDescription("V1.3.3: \n\n" +
                                    "-Added any requested fortunes from users \n" +
                                    "-Added some more fortunes to the list \n" +
                                    "-Fixed any wrong info in any of the menus \n" +
                                    "-Added some more characters to >mid list")
                    );
                await ctx.Channel.SendMessageAsync(message);
            }
            if (version == "1.3.4") 
            {
                var message = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("V1.3.4 Changelog")
                    .WithDescription("V1.3.4: \n\n" +
                                    "-Added some more characters to the list for the >mid command \n" +
                                    "-Changed the reactions for the >mid command to custom emotes \n" +
                                    "-Started Development for the >passive command (COMING SOON)")
                    );
                await ctx.Channel.SendMessageAsync(message);
            }
            if (version == "1.4")
            {
                var message = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("V1.4 Changelog")
                    .WithDescription("V1.4: \n\n" +
                                    "-Introducting the '>passive' command!!!!! \n\n" +
                                    "This command was requested by the man Delet#9054 himself. It is a guessing game based off Dokkan Unit Passive Skills \n" +
                                    "All you have to do is simply call the command. You will be displayed a random passive skill of a UR/LR unit. Server members have 20 seconds to guess \n" +
                                    "After 20 seconds the answer will be displayed in an embedded message. Have fun testing your dokkan knowledge!!!!")
                    );
                await ctx.Channel.SendMessageAsync(message);
            }
            if (version == "Latest" || version == "latest")
            {
                var message = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithAuthor("***The latest version is V1.4.1***")
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("V1.4.1 Changelog")
                    .WithDescription("V1.4.1: \n\n" +
                                    "-Added some more characters into the >passive command")
                    );
                await ctx.Channel.SendMessageAsync(message);
            }
        }
    }
}
