# Dokkan Passive Generator - Using the Link System
 
 Release Date - 14/11/2022
 
 Release Version - 1.5.5
 
 ## Welcome
 
 Welcome to the Link System of the Dokkan Passive Generator, part of the CoolerIsGay Discord Bot. This following article will show you how exactly the Link System works within the bot and how to add on links when creating a custom passive using /passivecreate
 
 ## Step 1 - Create a Passive
 
 Using the /passivecreate Slash Command, fill out its requirements. Below is an example of LR AGL Gohan:
 
 ![image](https://user-images.githubusercontent.com/98812930/201784768-b9b71a19-82a8-43e1-9cfe-ccc8e1372df8.png)
 
 Confirmation Message:
 
 ![image](https://user-images.githubusercontent.com/98812930/201784953-2c4b763b-9a5a-48c5-855f-325be8702887.png)

## Step 2 - The Link Parameter

When using /passivecreate, you will see a parameter called "Links". This is where you need to put your ACTIVE links in

When you use a passive, it is based on the scenario that those links you provided are ACTIVE, so please do not put any links you know are not going to be activated

You do not have to put 7/7 links and this makes life easier since you only need to put in the Links that will be active when using this creation command

### Structure (MUST FOLLOW)

When inputting in these Links, EACH link must be separated by a SINGLE SPACE

Links with multiple words must be joined together like in the below example and each word Must start with a Capital Letter

For example -> Links[PreparedForBattle GoldenWarrior TournamentOfPower]

If you do 'Links[Tournament Of Power Golden Warrior]' it will treat each word as a SEPARATE LINK so please make sure it is in the correct format or else it won't work

Inputting in 6/7 ACTIVE Links for AGL Gohan:

![image](https://user-images.githubusercontent.com/98812930/201785791-672d88c2-27ac-4e39-9a5e-7bf26685364e.png)


## Step 3 - Using the passive

Now that you have created the passive, when you come round to using it, all the links you put in should give a reasonble boost to the ATK/DEF stats depending on what links you put in

![image](https://user-images.githubusercontent.com/98812930/201786280-83fcef80-9e75-4a20-b054-26b446ac34a7.png)

## Optional

If you would like to create this passive with NO ACTIVE links, you have to type in 'null' in the Links parameter like this

![image](https://user-images.githubusercontent.com/98812930/201786445-5cc09d8b-2b43-43d4-ba8f-e740ad22b2b1.png)

## What Links are avalible

TO view the list of avalible links, navigate to the Links.txt file in the InfoFiles folder

OR go to this link -> https://github.com/samjesus8/CoolerDiscordBot/blob/master/InfoFiles/Links.txt

*CoolerDiscordBot/InfoFiles/Links.txt*

### Unavailable Links

The following links are not implemented into the bot because they may not possess ATK/DEF related buffs:

**Name - Reason**

- Golden Warrior - Debuff Link
- Evil Autocrats - Debuff Link
- Flee - Evasion Link
- Telekinesis - Debuff Link
- Telepathy - Crit Link
- Coward - Crit Link
- Attack of the Clones - Evasion Link
- Mechanical Menaces - DMG Reduction Link
- Champion's Strength - DMG Reduction Link
- Battlefield Diva - Evasion Link
- Dismal Future - Crit Link
- Loyalty - DMG Reduction Link
- Energy Absorption - HP Link
- Strength In Unity - HP Link
- Strongest Clan In Space - Debuff Link
- Cooler's Underling - Crit Link
- Team Turles - Crit Link
- Auto Regeneration - HP Link
- Fusion Failure - HP Link
- Fear & Faith - Debuff Link
- Blazing Battle - Rampage Debuff Link
- Golden Z-Fighter - Crit Link

