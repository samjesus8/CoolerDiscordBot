﻿using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
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
                                        "**Tools/Utility Help -> '>help Tools'** \n" +
                                        "**Dokkan Passive Generator -> '>help dokkanpassive OR dokkanslashcommands'** \n" +
                                        "**Music Player -> '>help Music'**")
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
                    .WithTitle("**Basic Calculator Functions | Imagine Cooler trying to do maths seriously!!!**")
                    .WithDescription("**>add** -> Add 2 numbers together | E.g: >add 2 2, returns 4 \n\n " +
                                    "**>subtract** -> Subtract 2 numbers together | E.g: >subtract 4 3, returns 1 \n\n " +
                                    "**>multiply** -> Multiply 2 numbers together | E.g: >multiply 6 4, returns 24 \n\n " +
                                    "**>divide** -> Divide 2 numbers together | E.g: >divide 5 2, returns 2.5 \n\n " +
                                    "**>circlearea** -> Gives you the area of a circle with any radius | E.g: >circlearea 2, returns 12.57")
                    );
                await ctx.Channel.SendMessageAsync(basicFunctionMessage);
            }

            if (helpType == "Fun" || helpType == "fun")
            {
                var funFunctionMessage = new DiscordMessageBuilder()
                    .AddEmbed(
                    new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("**Fun Commands**")
                    .WithDescription("**>ducky** -> Tell Ducky to STFU \n\n" +
                                    "**>stfu** -> Tell a random user to STFU. You can ping someone or just provide any form of text \n\n" +
                                    "**>delet** -> Prove that everything is mid with this one command \n\n " +
                                    "**>dialogue** -> The bot will send you a DM, type anything random to send it back to the channel where you used the command \n\n" +
                                    "**>watchyourtone** -> Tell someone to watch their tone \n **Syntax: >watchyourtone @User OR RandomText** \n" +
                                    "You can ping someone or use plaintext in the command \n\n" +
                                    "**>choosetone** -> An extension of the >watchyourtone command requested by Coola#5784. \n" +
                                    "***IMPORTANT: The syntax for this command is '>choosetone @User1 @User2 @User3.....' (You can have as many users as you want)*** \n\n" +
                                    "This command takes your users that you passed in and itll choose one of them at random. The bot will tell that chosen person to watch their tone \n\n" +
                                    "**>fortune** -> See if your fate is lucky or will it be hell \n\n" +
                                    "**>supernova** -> Ask the bot a question and it will answer it for you \n" +
                                    "The Syntax for this command is '>supernova YourQuestion' \n\n" +
                                    "**>cooler** -> Insult Coola#5784 with this command")
                    );
                await ctx.Channel.SendMessageAsync(funFunctionMessage);
            }

            if (helpType == "Games" || helpType == "games")
            {
                var gamesFunctionMessage = new DiscordMessageBuilder()
                    .AddEmbed(
                    new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("**Game Commands**")
                    .WithDescription("**>cardgame** -> A simple card game. If you draw higher than the bot, you win the game \n\n " +
                                    "**>lottery** -> Play the lottery, pick 5 numbers from 1-100 and test your luck \n" +
                                    "Syntax: >lottery num1 num2 num3 num4 num5 \n " +
                                    "Do >lottery on its own to view the instructions \n\n" +
                                    "**>mid** -> Play a game of Mid or Not Mid. A girl will show up on the screen \n" +
                                    "Server Members can vote if they think the girl is mid or not mid. After the time period, the most votes wins that round \n\n " +
                                    "**>passive** -> Play a game of guess the Passive. A Dokkan Passive skill will be shown on screen. If you guess who the unit is correctly you win")
                    );
                await ctx.Channel.SendMessageAsync(gamesFunctionMessage);
            }

            if (helpType == "Tools" || helpType == "tools")
            {
                var toolsFunctionMessage = new DiscordMessageBuilder()
                    .AddEmbed(
                    new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("**Tools Commands**")
                    .WithDescription("**>timestamp** -> After using this command, the next message you send the bot will return the exact time and date you sent it \n\n " +
                    "**>status** -> (ADMIN ONLY). Sets the 'Playing' status of the bot to any text \n\n " +
                    "**>invite** -> Generates an invite link for the bot, use it to add it to other servers of your choice \n\n" +
                    "**>server** -> Generated an invite link to join the Official Discord server for this bot \n\n" +
                    "**>changelog** -> View the bot changelog. Shows what changed in every update \n\n" +
                    "Use >changelog and type in a version to view its specific changes \n like ***'>changelog 1.1'*** \n" +
                    "Type in ***>changelog latest'*** to view the changelog of the latest version \n\n" +
                    "**>avatar** -> View the avatar of any user you specify. Simply type in >avatar and then @UserName you want to view \n\n" +
                    "**>membercount** -> View the member count of the server you are in \n\n" +
                    "**>version** -> Views the current version of this bot \n\n" +
                    "**>github** -> View the GitHub repository for this bot \n\n" +
                    "**>dokkanwindows** -> Download the Windows version of the Dokkan Passive Generator used in this bot")
                    );
                await ctx.Channel.SendMessageAsync(toolsFunctionMessage);
            }

            if (helpType == "DokkanSlashCommands" || helpType == "dokkanslashcommands" || helpType == "dokkanpassive") 
            {
                string description = "**You can also click on the title to view the 'PassiveSlashCommands.md' file which contains similar information** \n\n" +
                                     "**/passivecreate** -> This command allows a user to create their own passive. You have to fill out each parameter that it asks you to \n" +
                                     "Once you press enter you will recieve a confirmation message with your details and will be asked if all of it is correct and there are no issues. You will have to react" +
                                     " with a thumbs up or thumbs down if you want to save or not \n\n" +
                                     "**/usepassive** -> This command allows you to use your passive and generate some stats. Think of it as a Dokkan simulator where you can use your unit and see how hard it will hit \n" +
                                     "The bot will generate an ATK stat when supering and some other stats such as defense, or ATK with support, links, different leaders and many other kinds of buffs \n\n" +
                                     "**/passivelist** -> This command allows you to view a list of passives for any user including yourself which is done by typing 'null' into PassiveCreate to get a list of passive names. \n " +
                                     "Not only this, you can also view details of a specific passive by specfying the passive name \n\n" +
                                     "**/deletepassive** -> Deletes a passive from the name you specify. Please bear in mind you can only delete passives that were made by yourself and not others \n\n" +
                                     "**/viewlinks** -> Use this command to search up links that exist in Dokkan. This will help you input links when creating a passive as you can confirm if a link " +
                                     "exists or not";

                var slashFunctionMessage = new DiscordMessageBuilder()
                    .AddEmbed(
                    new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("**Dokkan Passive Generator - Slash Commands**")
                    .WithDescription(description)
                    .WithUrl("https://github.com/samjesus8/CoolerDiscordBot/blob/master/InfoFiles/PassiveSlashCommands.md")
                    );
                await ctx.Channel.SendMessageAsync(slashFunctionMessage);
            }

            if (helpType == "Music" || helpType == "music") 
            {
                string description = "**You can also click on the title to view the 'MusicPlayerCommands.md' file which contains similar information** \n\n" +
                                     "**>play** - You have to specify the VC you are in and the name of the song you want to play \n" +
                                     "Example - '>play The-Blacks Troll Song' \n\n" +
                                     "**>pause** - Pauses the current track \n\n" +
                                     "**>resume** - Resumes playback of the current track \n\n" +
                                     "**>stop** - Stops all playback and leaves the VC \n\n" +
                                     "**>join** - Joins a specific Voice Channel that you specify \n\n" +
                                     "**>leave** - Leaves a specific Voice Channel that you specify";

                var musicMSG = new DiscordMessageBuilder()
                    .AddEmbed(
                    new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("**Music Player - Commands**")
                    .WithDescription(description)
                    .WithUrl("https://github.com/samjesus8/CoolerDiscordBot/blob/master/InfoFiles/MusicPlayerCommands.md")
                    );
                await ctx.Channel.SendMessageAsync(musicMSG);
            }
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
                return;
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
                return;
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
                return;
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
                return;
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
                return;
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
                return;
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
                return;
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
                return;
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
                return;
            }
            if (version == "1.4.1")
            {
                var message = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("V1.4.1 Changelog")
                    .WithDescription("V1.4.1: \n\n" +
                                    "-Added some more characters into the >passive command")
                    );
                await ctx.Channel.SendMessageAsync(message);
                return;
            }
            if (version == "1.4.2")
            {
                var message = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("V1.4.2 Changelog")
                    .WithDescription("-Added ALL Dokkan LRs into the >passive command \n" +
                                    "-Added Cooldowns on certain commands: \n\n " +
                                    ">fortune: 5 Uses every 30 seconds PER USER \n" +
                                    ">passive: 1 Use every 32 seconds PER USER \n" +
                                    ">mid: 10 Uses every 5 min PER USER \n\n" +
                                    "Although a message will not be shown when the command is in cooldown, a solution is being worked on and will come in the next update \n" +
                                    "Make sure to keep these cooldowns in mind when using the commands until a notification system can be put in place")
                    );
                await ctx.Channel.SendMessageAsync(message);
                return;
            }
            if (version == "1.4.3") 
            {
                var message = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithFooter("The latest version is V1.4.3")
                    .WithTitle("V1.4.3 Changelog")
                    .WithDescription("-Added a message to show if cooldown is active for commands \n" +
                                     "-Added any requested fortunes from users \n" +
                                     "-Added all TURs from range 'A, B, C' into the >passive command")
                    );
                await ctx.Channel.SendMessageAsync(message);
                return;
            }
            if (version == "1.4.4")
            {
                var message = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("V1.4.4 Changelog")
                    .WithDescription("-Redesigned the whole of the >mid command: \n\n" +
                                     "Although everything on the user end is the same, the whole database of characters are moved into a JSON file rather than string[] arrays" +
                                     " in order to improve efficiency of the bot as well as reduce the amount of code written for the command \n\n" +
                                     "-Added all TURs from range 'D, E, F' into the >passive command \n" +
                                     "-Increased the cooldown of >fortune from 30s to 60s \n" +
                                     "-Moved all generators to a separate folder in the Bot Project Files")
                    );
                await ctx.Channel.SendMessageAsync(message);
                return;
            }
            if (version == "1.4.5")
            {
                var message = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("V1.4.5 Changelog")
                    .WithDescription("-Added all TURs from range 'G, H, I, J, K, L' into the >passive command \n" +
                                    "-Added some more characters into the >mid command and removed the timespan requirement. \n Users only need to enter >mid and the command will last for 20 seconds \n" +
                                    "**The command now has a cooldown of 25 seconds per user,** eliminating the spamming of the command whilst a previous use is still active. " +
                                    "So after the results are shown you have to wait 5 seconds before using it again \n\n" +
                                    "-Removed the Full changelog command as it takes up too much space, also vunerable to spamming. Instead the new changelog command, you have to provide the version " +
                                    "that you want to view like 1.1 \n\n" +
                                    "-New command >autolottery CURRENTLY IN DEVELOPMENT: \n\n" +
                                    "This is a command which calculates the sucess rate of any 5 numbers that you choose. All you need to do is pass in 5 numbers like you would in the normal lottery command" +
                                    " but this time instead of showing if you won or not, the bot will loop the lottery command until it manages to match 1-5 numbers \n\n" +
                                    "It will then give you a message saying how many attempts it took, and a success rate which tells you how lucky you managed to be \n\n" +
                                    "You cannot use the command but you can do >autolottery on its own to view more info about how the command works")
                    );
                await ctx.Channel.SendMessageAsync(message);
                return;
            }
            if (version == "1.4.6")
            {
                var message = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("V1.4.6 Changelog")
                    .WithDescription("-Fixed the Reaction errors in the >mid command after several weeks of debugging: \n\n" +
                                        "The Results section now properly adds up all the reactions instead of displaying the same emoji X times each with 1 vote \n" +
                                        "There may be some slight timing issues which still ruin the results but it works much better than before \n" +
                                        "It also shows the total number of people who participated in the voting \n\n" +
                                     "-Added all the TURs from range 'M, N, O' into the >passive command")
                    );
                await ctx.Channel.SendMessageAsync(message);
                return;
            }
            if (version == "1.4.7")
            {
                var message = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithAuthor("The latest version is 1.4.7")
                    .WithTitle("V1.4.7 Changelog")
                    .WithDescription("-Code cleanup: Removed any unneccesary packages that were installed on the project \n" +
                                     "Also removed any unused 'Usings' to clean up the code \n\n" +
                                     "-Added requested fortunes into the >fortune command \n" +
                                     "-Added a separate version of the >watchyourtone command that accepts any form of text. Users can still use pings in the command too \n" +
                                     "-Added all TURs from range 'P, Q, R' into the >passive command \n" +
                                     "-Added characters from the following animes into the >mid command: \n\n" +
                                     "(From now on, I will list any additions in this format) \n\n" +
                                     "***My Stepmom's Daughter is my Ex*** \n" +
                                     "***Shikimori is not just a Cutie*** \n" +
                                     "***Kaguya-Sama: Love is War***")
                    );
                await ctx.Channel.SendMessageAsync(message);
                return;
            }
            if (version == "1.4.8")
            {
                var message = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("V1.4.8 Changelog")
                    .WithDescription("-Added a new command requested by Coola#5784 ***'>choosetone'***: \n\n" +
                                     "The command is an extension of the alerady existing >watchyourtone command. Instead it is a random selector \n" +
                                     "You can provide as many users as you want, the bot will then choose someone at random and tell them to watch their tone. See more info by doing '>help fun' \n\n" +
                                     "-Added a new command '>server': \n\n This generates an invite link to join the official discord server for the bot. Here you can request support or suggest improvements for the bot \n\n" +
                                     "-Removed certain fortunes as they may be irrelavant to the current time period \n" +
                                     "-Added all TURs from range 'S - Z' into the passive command \n" +
                                     "**ALL CHARACTERS IN DOKKAN ARE FINALLY IN THE PASSIVE COMMAND!!!!** \n\n" +
                                     "-Added characters from the following shows into the >mid command: \n\n" +
                                     "***Date A Live (Missing Characters Added)***\n" +
                                     "***Horimiya*** \n" +
                                     "***Harem in the Labyrinth of Another World*** \n" +
                                     "***Haiyore: Nyaruko San***")
                    );
                await ctx.Channel.SendMessageAsync(message);
                return;
            }
            if (version == "1.4.9")
            {
                var message = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("V1.4.9 Changelog")
                    .WithDescription("-Added a new command requested by ***ducky#8786*** with name **>supernova** \n\n" +
                                     "This is similar to the 8-Ball command from Sandra, but better since the responses are relatable \n" +
                                     "All you do is do '>changelog YourQuestion' and the bot will give a random response. Have fun using this command!!! \n\n" +
                                     "-The >choosetone command can now accept plaintext as well as user pings \n" +
                                     "So users can do **'>choosetone @UserPing1 @UserPing2'** OR they can do **'>choosetone Text1 Text2 Text3'** and it will still work \n\n" +
                                     "-Increased the cooldown of >fortune to 12 HOURS due to spam/server load and also making it realistic in terms of the context of the fortune \n" +
                                     "-FOR ADMINS - Changed the >status command so that it can accept text with spaces \n" +
                                     "-Scrapped the >autolottery command due to little interest")
                    );
                await ctx.Channel.SendMessageAsync(message);
                return;
            }
            if (version == "1.5.0" || version == "1.5")
            {
                string description = "***THIS DISCORD BOT NOW SUPPORTS SLASH COMMANDS!!!*** \n\n" +
                                     "**-Addition of the bot's first ever slash commands /passivecreator, /usepassive AND /passivelist:** \n\n" +
                                     "This feature was suggested by Cloud Kazami#0001 with the idea behind Dokkan Concept Units design \n" +
                                     "This bot takes any passive design you give it and it will generate some stats as to how your unit will perform if it was " +
                                     "made in real life Dokkan \n" +
                                     "It is a very useful tool for concept makers and people who just want to experiment with their own ideas for Dokkan Passives \n" +
                                     "A lot of effort went into this so please try it and enjoy. More info about the commands can be found by doing **'>help DokkanSlashCommands'** \n\n" +
                                     "**-Updated the >passive command information:** \n\n" +
                                     "Updated INT GT Trio's passive to their Post-EZA passive \n" +
                                     "Updated INT Kid Goku's passive to his Post-EZA passive \n" +
                                     "Added R Bido to the list \n" +
                                     "Changed the photo of 'Joyful Athleticism: Mr. Buu' to the correct photo for display \n\n" +
                                     "**-Redesigned the whole >fortune command to reduce workload: \n\n" +
                                     "The command still does the same thing, however all the fortunes have been moved into a class in order to reduce " +
                                     "workload in the actual command function";

                var message = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("V1.5.0 Changelog")
                    .WithDescription(description)
                    );
                await ctx.Channel.SendMessageAsync(message);
                return;
            }
            if (version == "1.5.1")
            {
                string description = "***Dokkan Passive Generator - Slash Commands*** \n\n" +
                                     "**-Fixed the formatting of the way numbers are shown when doing /usepassive:** \n" +
                                     "Instead of 1000000, the number has been formatted to show 1,000,000 \n\n" +
                                     "**-The Damage Reduction & Support parameters now work: ** \n" +
                                     "You can now add in Damage Reduction & Support buffs for your units. When using /usepassive it will show the stats of your " +
                                     "unit with/without those buffs \n\n" +
                                     "**-Added a NEW parameter 'Rarity':** \n" +
                                     "You can now specify if you want your unit to be an LR or a TUR. LR means you can view your stats at 12 KI or Max 24 KI \n" +
                                     "So far this is all the bot has, the next step is to add the option for EZA units which have a higher Ki & SA Multiplier \n\n" +
                                     "***>mid Command Additions*** \n\n" +
                                     "**The following Titles have been added to the bot with their associated characters:** \n\n" +
                                     "**Genshin Impact -> Yae Miko, Yoimiya, Diona** \n" +
                                     "**Don't Toy with me, Miss Nagatoro** \n" +
                                     "**Hatsune Miku** \n" +
                                     "**Komi Can't Communicate** \n" +
                                     "**Lycoris Recoil**";

                var message = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("V1.5.1 Changelog")
                    .WithDescription(description)
                    );
                await ctx.Channel.SendMessageAsync(message);
                return;
            }
            if (version == "1.5.2")
            {
                string description = "***Dokkan Passive Generator - Slash Commands*** \n\n" +
                                     "**Added a new command '/deletepassive':** \n" +
                                     "Users can now delete their own passives from the system. This command is neccesary as some users " +
                                     "may want to delete passives that may have mistakes in them such as the wrong name or wrong amount of ATK \n\n" +
                                     "***Other Additions*** \n\n" +
                                     "**Added a new command >avatar:** \n" +
                                     "Users can now view the Profile Picture of any user they specify.\n Simply type '>avatar @UserName' \n\n" +
                                     "**Added a new command >cooler:** \n" +
                                     "This command is designed to specifically insult Coola#5784. No other description needed \n\n" +
                                     "**Added a new command >membercount:** \n" +
                                     "This command displays how many members are in your server";

                var message = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("V1.5.2 Changelog")
                    .WithDescription(description)
                    );
                await ctx.Channel.SendMessageAsync(message);
                return;
            }
            if (version == "1.5.3")
            {
                string description = "***NEW - Music Commands*** \n\n" +
                                     "The Bot can now play music in a Voice Channel. Simply give it a YouTube URL or search for what you want to play \n" +
                                     "Addition of the >join, >leave, >play and additional commands. More info can be found using **'>help Music'** \n\n" +
                                     "***Dokkan Passive Generator*** \n\n" +
                                     "Addressed an Issue with the DEF stat as it was reported as being too low \n" +
                                     "The Bot should now take into account all of the Buffs including Leader Skill, Passive Skill and any other buffs when calculating DEF \n\n" +
                                     "***>fortune Command*** \n\n" +
                                     "Added any requested fortunes into the Bot \n\n" +
                                     "***>passive Command Changes*** \n\n" +
                                     "Updated the Passives of the following units: \n **(These units have either been reported as Wrong or have recieved an EZA)** \n\n" +
                                     "-STR Namek Goku (SSJ Form) -> EZA Passive \n" +
                                     "-TEQ TF Frieza (2nd Form) -> Changed from Pre-EZA to EZAd passive \n" +
                                     "-PHY Giant Ape Kid Goku -> EZA Passive \n" +
                                     "-Baba Shop TURs -> EZA Passive";

                var message = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("V1.5.3 Changelog")
                    .WithDescription(description)
                    );
                await ctx.Channel.SendMessageAsync(message);
                return;
            }
            if (version == "1.5.4")
            {
                string description = "***New Commands*** \n\n" +
                                     ">version - Displays the Current version of the Bot \n\n" +
                                     "***>mid Command - Additions*** \n\n" +
                                     "Added the following Titles into the Bot: \n" +
                                     "Lycoris Recoil (Added the IMG URLs) \n" +
                                     "My Dress Up Darling \n" +
                                     "Darling in the FRANXX \n" +
                                     "Testament of a Sister New Devil \n\n" +
                                     "***Music Player*** \n\n" +
                                     "Fixed the issue with the playback being extremely laggy \n" +
                                     "When playing a song it should not crackle or stutter now";

                var message = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("V1.5.4 Changelog")
                    .WithDescription(description)
                    );
                await ctx.Channel.SendMessageAsync(message);
                return;
            }
            if (version == "1.5.5")
            {
                string description = "***>avatar Command*** \n\n" +
                                     "Changed the way the command works. If a user is provided such as '>avatar @Cooler', it will get the avatar of that user. \n" +
                                     "If >avatar is done on its own it'll just get the avatar of the user who called it \n\n" +
                                     "***Dokkan Passive Generator - INTRODUCING THE LINK SYSTEM*** \n\n" +
                                     "I'm proud to announce that the world's first Dokkan Passive Generator is now 100% complete with the addition of the Link System \n\n" +
                                     "This whole project took around a whole month to plan and design the whole system to create custom, user-made Dokkan Passives. Everything " +
                                     "from the maths behind the ATK/DEF calculations and the way its all displayed in Discord \n\n" +
                                     "The Link System now completes the whole project and makes the Stats look more realistic in an actual Dokkan gameplay situation, as they account for a major" +
                                     "portion of damage dealt by the units as well as tanking \n\n" +
                                     "***HOW TO USE -> PLEASE CLICK ON THE TITLE WHERE YOU WILL BE REDIRECTED TO A USER MANUAL ON HOW TO CREATE A PASSIVE WITH LINKS!!!!*** \n" +
                                     "You can also view this file by going to the GitHub project and navigating to '\\CoolerDiscordBot\\InfoFiles\\UsingLinks.md'";

                var message = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("V1.5.5 Changelog - **(Press here to view the UsingLinks.md File)**")
                    .WithDescription(description)
                    .WithUrl("https://github.com/samjesus8/CoolerDiscordBot/blob/master/InfoFiles/PassiveGenerator-UsingLinks.md")
                    );
                await ctx.Channel.SendMessageAsync(message);
                return;
            }
            if (version == "1.5.6")
            {
                string description = "***Dokkan Passive Generator*** \n" +
                                     "-The **'/viewlinks'** slash command now works properly. With this command you can search up a Dokkan Link and view its buffs \n\n" +
                                     "**Cooldowns** \n" +
                                     "-When a Cooldown for a command is activated, users can finally see the time remaining on the cooldown which makes " +
                                     "users more notified when they can use a command again \n" +
                                     "-The >fortune command has been changed to have a cooldown of 12 hours \n\n" +
                                     "**>mid Command - Additions** \n" +
                                     "Added the following Animes to the command \n\n" +
                                     "**-Beast Tamer** \n" +
                                     "**-The Maid I Hired Recently Is Mysterious**";

                var message = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("V1.5.6 Changelog")
                    .WithDescription(description)
                    );
                await ctx.Channel.SendMessageAsync(message);
                return;
            }
            if (version == "1.5.7")
            {
                string description = "***Dokkan Passive Generator - MASSIVE OVERHAUL*** \n" +
                                     "-Added EZA Rarities for both TUR & LR to the bot. You can now make EZA Passives and generate stats at that level. " +
                                     "The EZA passives are calculated at SA LV 15 and at Supreme Damage on the Super Attack \n" +
                                     "-Changed the TUR Super Attack multipliers from **'Immense'** to **'Supreme'** as that is the more realistic scenario \n" +
                                     "-Added the Raise ATK effect on Super Attack. ATK stats will be calculated when supering, not before as that makes no sense. \n" +
                                     "ALL attacks will also be shown under different types of raises (Normal, Great, Massive Raises) to get different scenarios. \n\n" +
                                     "***New Command - >github*** \n" +
                                     "-This command allows users to view the GitHub repository for this bot, providing easier access to users who want " +
                                     "to submit issues or view the code for this bot. \n" +
                                     "This command also has a sub-command '>dokkanwindows' which provides the link to the Windows version of the Dokkan Passive Generator \n\n" +
                                     "***Fortune Command - Changes*** \n" +
                                     "-As per request, the commmand now filters between certain servers: \n" +
                                     "IF the command was called in the **'The Infinite Universe'** server, the fortunes will be normal for members in that server \n" +
                                     "IF the command was called in any other Discord Server, fortunes will be different and will be more realistic";

                var message = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("***V1.5.7 Changelog***")
                    .WithDescription(description)
                    );
                await ctx.Channel.SendMessageAsync(message);
                return;
            }
            if (version == "1.5.8")
            {
                string description = "***The Infinite Universe - Changes*** \n" +
                                     "-All commands associated with this server have been put in a separate class library in code to make things easier \n" +
                                     "-Introduced a new command **>fuck**. I don't need to explain this as it speaks for itself and it can only be used in NSFW channels \n" +
                                     "-Added any requested fortunes to the **>fortune** command for this server \n\n" +
                                     "***>supernova Changes (GLOBAL)*** \n" +
                                     "-Altered some of the responses to make more sense \n" +
                                     "-Added some more responses to the command \n\n" +
                                     "***Command Deletions (GLOBAL)*** \n" +
                                     "-Removed the **>tory** command as it is not used that much";

                var message = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("***V1.5.8 Changelog***")
                    .WithDescription(description)
                    );
                await ctx.Channel.SendMessageAsync(message);
                return;
            }
            if (version == "Latest" || version == "latest")
            {
                string description = "***The Infinite Universe - Changes*** \n" +
                                     "-Added an API to the '>fuck' command. There will be much more GIFs than usual and the server can enjoy doing their things which " +
                                     "I don't need to talk about \n\n" +
                                     "***Other Fixes (GLOBAL)*** \n" +
                                     "-Fixed an issue with the bot causing it to crash every few seconds after start-up. This was an issue " +
                                     "with the media player not being able to connect to the music servers \n" +
                                     "-The >status command (ADMINS ONLY) now displays a confirmation message whenever you execute the command sucessfully \n" +
                                     "-The >help command has gone through a major revision. All the avalible commands are displayed with its associated description and any obsolete " +
                                     "commands have been removed to prevent confusion";

                var message = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithAuthor("The Latest version is 1.5.9")
                    .WithTitle("***V1.5.9 Changelog***")
                    .WithDescription(description)
                    );
                await ctx.Channel.SendMessageAsync(message);
                return;
            }

            else
            {
                var errorMessage = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()

                    .WithColor(DiscordColor.Azure)
                    .WithTitle("Sorry, I didn't recognise that version")
                    .WithDescription("List of versions: \n" +
                    "1.1/1.2/1.2.1 \n " +
                    "1.3/1.3.1/1.3.2/1.3.3/1.3.4 \n " +
                    "1.4/1.4.1/1.4.2/1.4.3/1.4.4/1.4.5/1.4.6/1.4.7/1.4.8/1.4.9 \n " +
                    "1.5.0/1.5.1/1.5.2/1.5.3/1.5.4/1.5.5/1.5.6/1.5.7/1.5.8 \n" +
                    "Or >changelog latest for the latest version info")
                    );
                await ctx.Channel.SendMessageAsync(errorMessage);
            }
        }
    }
}
