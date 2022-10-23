# Dokkan Passive Slash Commands

Welcome to the Passive Creator application released in V1.5 of the discord bot. This is a Dokkan style simulator where users can enter their own passive information and the bot will generate some stats based on different conditions such as the ATK with support or DEF on a massive raise. This is not a 100% Dokkan replica and is only meant for Fun & Experimental purposes

## Step 1 - "/passivecreate"
This is Step 1 into using the command. As you start writing the command, Discord automatically shows you each parameter of the command that you need to fill in such as the Base Attack value or the Leader Skill name, please take your time and fill out all the details it asks you to

### Parameters
![image](https://user-images.githubusercontent.com/98812930/197410885-bf175a6b-5810-4e5f-ab9b-7703312764a1.png)
![image](https://user-images.githubusercontent.com/98812930/197410990-74fae665-ac46-4eda-9a75-1dce15f9f5d7.png)


> **Command Parameters**

PassiveName - Give your Passive a name, it can be anything you want
HP - The base HP value of your card
ATK - The base ATK of your card
DEF - The base DEF of your card

Leader Skill Name - Give your leader skill a name, this can be anything you want. It dosent have to be one from Dokkan like Pure Saiyans
Leader Skill Value - This has to be a % value, most popular ones are 150, 170, 200 (DO NOT INCLUDE THE %, JUST TYPE IN THE NUMBER)

PassiveATK - This is the TOTAL ATK% buff of your passive so if you have multiple buffs please add them up and put the final value here (DO NOT INCLUDE THE %, JUST THE NUMBER)
PassiveDEF - This is the TOTAL DEF% buff of your passive so if you have multiple buffs please add them up and put the final value here (DO NOT INCLUDE THE %, JUST THE NUMBER)

DMGReduction - This is the % buff of damage reduction your passive includes
Support - This is the total % buff of support from allies

Links - There has to be 7 links separated with ONE SPACE

E.g: Link1 Link2 Link3 Link4 Link5 Link6 Link7
If you dont want to add any links please type in "null"

> **Confirmation**

Once you have pressed ENTER after filling out your details you will recieve a confirmation message with all your details, you must react with a :thumbsup: if you want the passive to be created, :thumbsdown:  if you want to cancel the command

![image](https://user-images.githubusercontent.com/98812930/197412658-71dd8b35-f00d-4339-9ff3-310999e91035.png)


## Step 2 - "/usepassive"

This is the command where you get to use your passive that you created. It has one parameter and thats the Name of your passive.
YOU MUST SPECIFY THE EXACT SAME NAME YOU USED IN /passivecreate WHEN FILLING OUT THE 'passivename' PARAMETER

The bot will then take your inputs and run it through the maths. It will then generate a Embed with your stats such as ATK stat when supering, DEF when it was Massively raised

![image](https://user-images.githubusercontent.com/98812930/197412774-6d8db776-b2c5-4bbc-b64a-70083a6cce8c.png)

