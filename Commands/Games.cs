using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;

namespace DiscordBotTest.Commands
{
    public class Games : BaseCommandModule
    {
        [Command("cardgame")]
        public async Task SimpleCardGame(CommandContext ctx)
        {
            var drawCard = new DiscordMessageBuilder()
                .AddEmbed(
                new DiscordEmbedBuilder()
                .WithTitle("Press the Draw Button to draw a card!!!")
                .WithDescription("If you draw higher than the Bot, you win")
                )
                .AddComponents(
                new DiscordButtonComponent(ButtonStyle.Primary, "drawButton", "Draw Card")
                );
            await ctx.Channel.SendMessageAsync(drawCard);

            ctx.Client.ComponentInteractionCreated += async (a, b) =>
            {
                if (b.Interaction.Data.CustomId == "drawButton") 
                {
                    var cardGen = new CardGenerator();
                    var yourCard = new DiscordMessageBuilder()
                        .AddEmbed(
                        new DiscordEmbedBuilder()
                        .WithTitle("Your Card")
                        .WithDescription("You drew a: " + cardGen.cardHit)
                        );

                    await ctx.Channel.SendMessageAsync(yourCard);


                    var botCardGen = new CardGenerator();
                    var botCard = new DiscordMessageBuilder()
                        .AddEmbed(
                        new DiscordEmbedBuilder()
                        .WithTitle("The bot drew a: ")
                        .WithDescription(botCardGen.cardHit)
                        );

                    await ctx.Channel.SendMessageAsync(botCard);

                    if (cardGen.GenNoRes > botCardGen.GenNoRes)
                    {
                        var winner = new DiscordMessageBuilder()
                            .AddEmbed(
                            new DiscordEmbedBuilder()
                            .WithTitle("YOU WIN")
                            );

                        await ctx.Channel.SendMessageAsync(winner);
                        return;
                    }
                    else 
                    {
                        var lose = new DiscordMessageBuilder()
                            .AddEmbed(
                            new DiscordEmbedBuilder()
                            .WithTitle("YOU LOSE")
                            );

                        await ctx.Channel.SendMessageAsync(lose);
                        return;
                    }
                }
            };
        }
    }
}
