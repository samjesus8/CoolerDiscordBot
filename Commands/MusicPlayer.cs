﻿using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Lavalink;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBotTest.Commands
{
    public class MusicPlayer : BaseCommandModule
    {
        [Command("join")]
        public async Task JoinVC(CommandContext ctx, DiscordChannel channel) 
        {
            var lavaInstance = ctx.Client.GetLavalink(); //Getting instance of LavaLink

            if (!lavaInstance.ConnectedNodes.Any()) //Check if the bot is connected to LavaLink
            {
                await ctx.RespondAsync("Connection is not Established");
                return;
            }

            var node = lavaInstance.ConnectedNodes.Values.First();

            if (channel.Type != ChannelType.Voice) //Check if the Bot is in a valid Voice Channel
            {
                await ctx.RespondAsync("Invalid Voice Channel");
                return;
            }

            await node.ConnectAsync(channel); //Connect to the channel
            await ctx.RespondAsync($"Joined {channel.Name}");
        }

        [Command("leave")]
        public async Task LeaveVC(CommandContext ctx, DiscordChannel channel) 
        {
            var lavaInstance = ctx.Client.GetLavalink();

            if (!lavaInstance.ConnectedNodes.Any()) //Check if the bot is connected to LavaLink
            {
                await ctx.RespondAsync("Connection is not Established");
                return;
            }

            var node = lavaInstance.ConnectedNodes.Values.First();

            if (channel.Type != ChannelType.Voice) //Check if the Bot is in a valid Voice Channel
            {
                await ctx.RespondAsync("Invalid Voice Channel");
                return;
            }

            var conn = node.GetGuildConnection(channel.Guild);

            if (conn == null) 
            {
                await ctx.RespondAsync("Not Connected");
                return;
            }

            await conn.DisconnectAsync();
            await ctx.RespondAsync($"Left {channel.Name}");
        }

        [Command("play")]
        public async Task PlayMusic(CommandContext ctx, [RemainingText] string search) 
        {
            if (ctx.Member.VoiceState == null || ctx.Member.VoiceState.Channel == null) //Checking to see if user is in a VC
            {
                await ctx.RespondAsync("Please enter a Voice channel");
                return;
            }

            var lavaInstance = ctx.Client.GetLavalink(); //Getting instance of LavaLink
            var node = lavaInstance.ConnectedNodes.Values.First();
            var conn = node.GetGuildConnection(ctx.Member.VoiceState.Guild);

            if (conn == null) //If the Bot failed to connect
            {
                await ctx.RespondAsync("Lavalink failed to connect");
                return;
            }

            var loadResult = await node.Rest.GetTracksAsync(search); //Search for the track using GetTracksAsync
            if (loadResult.LoadResultType == LavalinkLoadResultType.LoadFailed || loadResult.LoadResultType == LavalinkLoadResultType.NoMatches) 
            {
                await ctx.RespondAsync($"Failed to find {search}"); //Check to see if the Server failed or if there was no matches
                return;
            }

            var track = loadResult.Tracks.First(); //Get the first entry in the search as it is the most accurate

            await conn.PlayAsync(track); //Play the track
            await ctx.RespondAsync($"Playing {track.Title}");
        }

        [Command("pause")]
        public async Task PauseMusic(CommandContext ctx) 
        {
            if (ctx.Member.VoiceState == null || ctx.Member.VoiceState.Channel == null) //Check to see if user is in VC
            {
                await ctx.RespondAsync("You are not in a voice channel.");
                return;
            }

            var lava = ctx.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();
            var conn = node.GetGuildConnection(ctx.Member.VoiceState.Guild);

            if (conn == null) //Check to see if Bot is connected to LavaLink
            {
                await ctx.RespondAsync("Lavalink is not connected.");
                return;
            }

            if (conn.CurrentState.CurrentTrack == null) //Check to see if there is anything playing
            {
                await ctx.RespondAsync("There are no tracks loaded.");
                return;
            }

            await conn.PauseAsync();
        }

        [Command("resume")]
        public async Task ResumeMusic(CommandContext ctx)
        {
            if (ctx.Member.VoiceState == null || ctx.Member.VoiceState.Channel == null) //Check to see if user is in VC
            {
                await ctx.RespondAsync("You are not in a voice channel.");
                return;
            }

            var lava = ctx.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();
            var conn = node.GetGuildConnection(ctx.Member.VoiceState.Guild);

            if (conn == null) //Check to see if Bot is connected to LavaLink
            {
                await ctx.RespondAsync("Lavalink is not connected.");
                return;
            }

            if (conn.CurrentState.CurrentTrack == null) //Check to see if there is anything playing
            {
                await ctx.RespondAsync("There are no tracks loaded.");
                return;
            }

            await conn.ResumeAsync();
        }

        [Command("stop")]
        public async Task StopMusic(CommandContext ctx)
        {
            if (ctx.Member.VoiceState == null || ctx.Member.VoiceState.Channel == null) //Check to see if user is in VC
            {
                await ctx.RespondAsync("You are not in a voice channel.");
                return;
            }

            var lava = ctx.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();
            var conn = node.GetGuildConnection(ctx.Member.VoiceState.Guild);

            if (conn == null) //Check to see if Bot is connected to LavaLink
            {
                await ctx.RespondAsync("Lavalink is not connected.");
                return;
            }

            if (conn.CurrentState.CurrentTrack == null) //Check to see if there is anything playing
            {
                await ctx.RespondAsync("There are no tracks loaded.");
                return;
            }

            await conn.StopAsync();
        }
    }
}
