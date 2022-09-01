﻿using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;

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
                .WithTitle("Cooler Is Gay Bot - Made by Samuel J \n Copyright ©SamuelJesuthas \n\n HELP MENU")
                .WithDescription("This is a multi-utility bot which just features random stuff \n Click on a category to view its list of commands \n The prefix is '>' ")
                )
                .AddComponents(new DiscordComponent[] 
                {
                    new DiscordButtonComponent(ButtonStyle.Primary, "calculatorFunction", "Calculator"),
                    new DiscordButtonComponent(ButtonStyle.Primary, "funFunction", "Fun Commands"),
                    new DiscordButtonComponent(ButtonStyle.Primary, "gamesFunction", "Games"),
                    new DiscordButtonComponent(ButtonStyle.Primary, "toolsFunction", "Tools"),
                    new DiscordButtonComponent(ButtonStyle.Danger, "exitFunction", "Exit")
                }
                );

            await ctx.Channel.SendMessageAsync(mainMenuBuilder);

            ctx.Client.ComponentInteractionCreated += async (a, b) =>
            {
                if (b.Interaction.Data.CustomId == "calculatorFunction") 
                {
                    var basicFunctionMessage = new DiscordMessageBuilder()
                        .AddEmbed(
                        new DiscordEmbedBuilder()
                        .WithTitle("Basic Calculator Functions")
                        .WithDescription(">add -> Add 2 numbers together E.g: >add 2 2, returns 4 \n\n " +
                                        ">subtract -> Subtract 2 numbers together E.g: >subtract 4 3, returns 1 \n\n " +
                                        ">multiply -> Multiply 2 numbers together E.g: >multiply 6 4, returns 24 \n\n " +
                                        ">divide -> Divide 2 numbers together E.g: >divide 5 2, returns 2.5")
                        );
                    await ctx.Channel.SendMessageAsync(basicFunctionMessage);
                }

                if (b.Interaction.Data.CustomId == "funFunction") 
                {
                    var funFunctionMessage = new DiscordMessageBuilder()
                        .AddEmbed(
                        new DiscordEmbedBuilder()
                        .WithTitle("Fun Commands")
                        .WithDescription(">cooler -> Returns an embedded 'Cooler is Gay' message \n\n " +                                       
                                        ">tory -> Slander the torys \n\n" +
                                        ">delet -> Prove that everything is mid with this one command \n\n " +
                                        ">question -> Answer a random question about cooler \n\n " +
                                        ">dialogue -> The bot will send you a DM, type anything random to send it back to the channel where you used the command \n\n" +
                                        ">watchyourtone -> Tell someone to watch their tone, you have to mention someone or else the command won't work. Syntax: >watchyourtone @User")
                        );
                    await ctx.Channel.SendMessageAsync(funFunctionMessage);
                }

                if (b.Interaction.Data.CustomId == "gamesFunction") 
                {
                    var gamesFunctionMessage = new DiscordMessageBuilder()
                        .AddEmbed(
                        new DiscordEmbedBuilder()
                        .WithTitle("Game Commands")
                        .WithDescription(">cardgame -> Play a simple card game. Press the button to draw a card \n " +
                                                       "If your card is higher than what the bot draws, You Win \n\n" +
                                        ">lottery -> Play the lottery, pick 5 numbers from 1-50 and test your luck. " +
                                        "Syntax: >lottery num1 num2 num3 num4 num5")
                        );
                    await ctx.Channel.SendMessageAsync(gamesFunctionMessage);
                }

                if (b.Interaction.Data.CustomId == "toolsFunction") 
                {
                    var toolsFunctionMessage = new DiscordMessageBuilder()
                        .AddEmbed(
                        new DiscordEmbedBuilder()
                        .WithTitle("Tools Commands")
                        .WithDescription(">timestamp -> After using this command, the next message you send the bot will return the exact time and date you sent it \n\n " +
                        ">status -> Only Sam and Delet can use this command. Sets the 'Playing' status of the bot to any text. There cannot be any spaces \n\n " +
                        ">invite -> Generates an invite link for the bot, use it to add it to other servers of your choice")
                        );
                    await ctx.Channel.SendMessageAsync(toolsFunctionMessage);
                }

                if (b.Interaction.Data.CustomId == "exitFunction") 
                {
                    var exitMessage = new DiscordMessageBuilder()
                        .AddEmbed(
                        new DiscordEmbedBuilder()
                        .WithTitle("You have Exit this command \n Please type in >help to use the menu again")
                        );
                    await ctx.Channel.SendMessageAsync(exitMessage);
                }
            };
        }
    }
}
