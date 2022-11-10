# Music Player Commands

This page contains all the commands you need in order to use the Music Player system implemented into the Bot
Release Date - 10/11/2022
Released on Version - 1.5.3

## Commands

### >join

The Parameters for this command is '>join [ChannelName]', where "ChannelName" is the name of the Voice Channel you want the bot to join.
When the bot joins it won't do anything other than await a command to play music or leave the channel

### >leave

The Parameters for this command is '>leave [ChannelName]' where "ChannelName" is the name of the Voice Channel you want the bot to leave
The bot will simply leave the specified channel

### >play

The Parameters for this command is '>play [ChannelName Search]' where "ChannelName" is the name of the Voice Channel you want the bot to join and 
"Search" is the Song you are looking for

Please note that when Pausing and Resuming playback, don't use >play to resume playback as it will attempt to search for something else. Use >pause and >resume instead

### >pause
Pauses the current track

### >resume
Resumes playback of the current track

### >stop
Stops playback of the current track and leaves the VC