# Dokkan Passive Slash Commands

Welcome to the Passive Creator application released in V1.5 of the discord bot. This is a Dokkan style simulator where users can enter their own passive information and the bot will generate some stats based on different conditions such as the ATK with support or DEF on a massive raise. This is not a 100% Dokkan replica and is only meant for Fun & Experimental purposes

## Step 1 - "/passivecreate"
This is Step 1 into using the command. As you start writing the command, Discord automatically shows you each parameter of the command that you need to fill in such as the Base Attack value or the Leader Skill name, please take your time and fill out all the details it asks you to

### Parameters
![image](https://user-images.githubusercontent.com/98812930/204794571-8c8e0da0-5736-4604-84d0-eae0608fffd7.png)


### Command Parameters

- PassiveName - Give your Passive a name, it can be anything you want
- Rarity - Is your card a "TUR", "LR", "TUR(EZA)" or "LR(EZA)". When typing this in it has to be the exact same name as listed. You cannot put this in lower case or else the bot won't recognise it
- HP - The base HP value of your card
- ATK - The base ATK of your card
- DEF - The base DEF of your card
- Leader Skill Name - Give your leader skill a name, this can be anything you want. It dosent have to be one from Dokkan like Pure Saiyans
- Leader Skill Value - This has to be a % value, most popular ones are 150, 170, 200 (DO NOT INCLUDE THE %, JUST TYPE IN THE NUMBER)
- PassiveATK - This is the TOTAL ATK% buff of your passive so if you have multiple buffs please add them up and put the final value here (DO NOT INCLUDE THE %, JUST THE NUMBER)
- PassiveDEF - This is the TOTAL DEF% buff of your passive so if you have multiple buffs please add them up and put the final value here (DO NOT INCLUDE THE %, JUST THE NUMBER)
- DMGReduction - This is the % buff of damage reduction your passive includes
- Support - This is the total % buff of support from allies
- Links - There has to be a Maximum of 7 links separated with ONE SPACE, you do not have to provide 7 links as you only need to provide ones that you know will be active

E.g: Link1 Link2 Link3 Link4 Link5 Link6 Link7

If you dont want to add any links please type in "null"

## Confirmation

Once you have pressed ENTER after filling out your details you will recieve a confirmation message with all your details, you must react with a :thumbsup: if you want the passive to be created, :thumbsdown:  if you want to cancel the command

![image](https://user-images.githubusercontent.com/98812930/204795023-2a8dd770-90c0-401d-97d1-064b2cb05cdd.png)


## Step 2 - "/usepassive"

This is the command where you get to use your passive that you created. It has one parameter and thats the Name of your passive.
YOU MUST SPECIFY THE EXACT SAME NAME YOU USED IN /passivecreate WHEN FILLING OUT THE 'passivename' PARAMETER

The bot will then take your inputs and run it through the maths. It will then generate a Embed with your stats such as ATK stat when supering, DEF when it was Massively raised

Example of INT LR Vegeta & Trunks being used:

![image](https://user-images.githubusercontent.com/98812930/204795375-83569631-3fcf-4a61-a29e-643e72db5c20.png)

![image](https://user-images.githubusercontent.com/98812930/204795216-b5936182-891c-475b-a6b6-0347b54b6035.png)

## /passivelist Command

This command allows users to view the list of passives they have created and its details. You can also use this command to view the passives of another user and its details

**The parameters of this command are**
User - This is the username you are trying to search for

PassiveName - If you just want a list of passives type in 'null'. Else you have to type the name of their passive to view its details

This following input where PassiveName = "null", shows the list of Passives you created

![image](https://user-images.githubusercontent.com/98812930/197633259-fb346456-7186-4d70-94e2-9eb7df5437b4.png)


The following input where PassiveName = "YourPassiveName" shows details about the name of the passive you entered

Step 1: 

![image](https://user-images.githubusercontent.com/98812930/197633306-69eddf51-a810-4fb7-8f70-81bcf1b9f57c.png)


Step 2: 

![image](https://user-images.githubusercontent.com/98812930/197633614-2aec638a-6fab-4c65-ab3c-e80f29771166.png)

## /deletepassive Command

This command allows users to delete any passives that ONLY they have created. You cannot delete another user's passive

Simply provide the name of the passive you want to delete and it will remove it from the system

### Step 1 - Look at the passive you want to delete

In this example we will be deleting the "PHY Bardock" passive from the list of stored passives for this user:

![image](https://user-images.githubusercontent.com/98812930/201942410-cff1b3cd-8903-4ba6-923c-dab5d31a8d7c.png)

### Step 2 - Use /deletepassive

So to delete the PHY Bardock passive all we do is /deletepassive and provide the exact same name:

![image](https://user-images.githubusercontent.com/98812930/201942833-33bf8ff7-a91c-4f62-89ec-8487374b47a9.png)

Executing the command:

![image](https://user-images.githubusercontent.com/98812930/201942933-a60f3642-a993-4e8d-8a62-19ba53a36d3d.png)

### Step 3 - Check if it was Deleted

To check if the passive was deleted, run /passivelist again and check your list of passives:

![image](https://user-images.githubusercontent.com/98812930/201943130-9382af38-d6a6-45d1-b9cc-f6e088464623.png)

And as you can see, PHY Bardock is no longer in the list for this user

# Additional Support - Links to help pages

If any of these commands return an error, please ping 𝕤𝕒𝕞.𝕛𝕖𝕤𝕦𝕤𝟠#6825 in Discord, ask for help in the support server or submit an issue here specifying what went wrong with screenshots of the error, what you did to get that error and we will try and help

- For people struggling to input Links when creating a passive please visit this link -> https://github.com/samjesus8/CoolerDiscordBot/blob/master/InfoFiles/PassiveGenerator-UsingLinks.md

- For a list of Links please visit this link -> https://github.com/samjesus8/CoolerDiscordBot/blob/master/InfoFiles/Links.txt
(Each link shows its ATK/DEF Buffs respectively)



