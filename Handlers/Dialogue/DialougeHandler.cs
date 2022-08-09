using DiscordBotTest.Handlers.Dialogue.Steps;
using DSharpPlus;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotTest.Handlers.Dialogue
{
    public class DialougeHandler
    {
        private readonly DiscordClient _client;
        private readonly DiscordChannel _channel;
        private readonly DiscordUser _user;
        private IDialogueStep _currentStep;

        public DialougeHandler(DiscordClient client, DiscordChannel channel, DiscordUser user, IDialogueStep startingStep) 
        {
            _client = client;
            _channel = channel;
            _user = user;
            _currentStep = startingStep;
        }

        private readonly List<DiscordMessage> messages = new List<DiscordMessage>();

        public async Task<bool> ProcessDialogue() 
        {
            while (_currentStep != null) 
            {
                _currentStep.OnMessageAdded += (message) => messages.Add(message);
                bool canceled = await _currentStep.ProcessStep(_client, _channel, _user);

                if (canceled) 
                {
                    await DeleteMessages();
                    var cancelEmbed = new DiscordMessageBuilder()
                        .AddEmbed(
                        new DiscordEmbedBuilder()
                        .WithTitle("Cancelled")
                        .WithDescription(_user.Mention)
                        );

                    await _channel.SendMessageAsync(cancelEmbed);
                    return false;
                }

                _currentStep = _currentStep.NextStep;
            }

            await DeleteMessages();
            return true;
        }

        private async Task DeleteMessages() 
        {
            if (_channel.IsPrivate) { return; }

            foreach (var message in messages) 
            {
                await message.DeleteAsync();
            }
        }
    }
}
