using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotTest.Builders
{
    internal class FortuneStorage
    {
        public List<string> Fortunes = new List<string>();
        public string[] badUnits = { "STR Beerus", "TEQ Corrupted Zamasu", "PHY Buu", "STR Janemba", "INT Janemba", "STR Broly (Not the LR)",
                                    "PHY Broly", "TEQ Perfect Form Cell", "INT Boujack", "TEQ Raditz", "STR Kefla", "INT Android 16",
                                        "AGL Super Vegito", "PHY Vegito Blue", "AGL Pan", "AGL SSJ3 Goku" };

        public FortuneStorage() 
        {
            var random = new Random();
            int badUnitsIndex = random.Next(badUnits.Length);

            //The Infinite Universe

            Fortunes.Add("You will have zero bitches");
            Fortunes.Add("You will get SSBE on your next multi in Dokkan");
            Fortunes.Add("Joku is gonna come to your house tommorow at 5AM");
            Fortunes.Add("You will get shafted when your favorite unit comes out in either Dokkan/Legends");
            Fortunes.Add("There's an autistic baboon at your window");
            Fortunes.Add("The next Dokkan-Fest Banner you summon on, you will get unfeatured SSRs no matter what");
            Fortunes.Add("You will get bitches");
            Fortunes.Add("You will pull the new LR that's currently out");
            Fortunes.Add("Raditz will come to your house and rad all over your struggles");
            Fortunes.Add("You will do a radillion damage next turn on Red Zone");
            Fortunes.Add("You don't deserve any fortunes");
            Fortunes.Add("Your predictions for the next LR/Dokkan-Fest will be true");
            Fortunes.Add("Joku will end up next to you in bed");
            Fortunes.Add("Tommorrow its gonna be morbin time, get ready to have the best day of your life");
            Fortunes.Add("The person who sent the latest message in edcord. You're fucking gay, leave the server u bozo");
            Fortunes.Add("If you get this fortune, Shallot ain't going SSB");
            Fortunes.Add("Delet will get bitches once he graduates");

            Fortunes.Add("Because of you, Main will get shafted in the next multi he does on Dokkan");
            Fortunes.Add("Because of you, Vein will get shafted in the next multi he does on Dokkan");
            Fortunes.Add("Because of you, Coola will get shafted in the next multi he does on Dokkan");
            Fortunes.Add("Because of you, Cloud will get shafted in the next multi he does on Dokkan");
            Fortunes.Add("Because of you, Delet will get shafted in the next multi he does on Dokkan");
            Fortunes.Add("Because of you, Ducky will get shafted in the next multi he does on Dokkan");
            Fortunes.Add("Because of you, Sam will get shafted in the next multi he does on Dokkan");

            Fortunes.Add("If you managed to get this fortune, Loved has a skill issue");
            Fortunes.Add("The next multi you do in dokkan, you will get " + badUnits[badUnitsIndex]);
            Fortunes.Add("The Queen's revive skill will now activate");
            Fortunes.Add("If you get this fortune, you are able to create your own fortune and add it to this list. Please ping @𝕤𝕒𝕞.𝕛𝕖𝕤𝕦𝕤𝟠#6825 with your fortune");
            Fortunes.Add("If you have Legends installed, you are a moron");
            Fortunes.Add("One day, Cooler will return to Discord because of something really dumb");
            Fortunes.Add("The next anime you watch, will have a trap in it");
            Fortunes.Add("You will inevitably get banned from this server because of something stupid");
            Fortunes.Add("ATK & DEF +150%; plus an additional ATK & DEF +50% when performing a Super Attack if facing only 1 enemy and if that enemy's HP is 50% or less when the character performs a Super Attack, " +
                                "plus an additional ATK +100% and high chance of performing a critical hit; performs a critical hit within the same turn after receiving an attack plus an additional DEF +150% when facing 2 or more enemies; Ki +2 for the rest of battle after delivering a final blow");

            //Requested Fortunes

            Fortunes.Add("Joku will go beyond Super Nigger Blue");
            Fortunes.Add("If you get this fortune, you will become as dumb as Cooler");
            Fortunes.Add("Cooler will come to your house thinking there is pussy over there but instead it was just dicks");
            Fortunes.Add("If you get this fortune, you will never enjoy a Dragon Ball Game again");
            Fortunes.Add("Joku will now get attacked by a wild bear");
            Fortunes.Add("Ash will never get shafted in any gacha game");
            Fortunes.Add("Next fortune you do will have the opposite effect");
            Fortunes.Add("If you get this fortune, you will forever suffer from Copium");
            Fortunes.Add("If you get this then Rak should watch his tone");
            Fortunes.Add("Yes");
            Fortunes.Add("No");
            Fortunes.Add("If you get this fortune, Watch Your Tone");
            Fortunes.Add("Upon starting the day on a Sunday, you will gain the ability to ATK & DEF +140%; Ki +1 at start of each turn (up to +3); guards all attacks; " +
                                "plus an additional ATK +40% within the same turn when guard is activated");
            Fortunes.Add("If u get this fortune, tell Coola#5784 to watch his damn tone for maining LoE");
            Fortunes.Add("If you get this fortune then Coola#5784 should watch his tone");
            Fortunes.Add("Vein will get VT and INT Zamasu");
            Fortunes.Add("If u get this fortune, tell Joku that Coola wanted to do VC with him");

            //Sigma Quotes

            Fortunes.Add("Instead of trying to blend in, Stand out and never blend in");
            Fortunes.Add("Confuse them with your silence. Shock them with your actions");
            Fortunes.Add("80% of boys have Girlfriends, the rest have a brain. Separate yourself from society and you will be above them");
            Fortunes.Add("A true man denies society's standards and expectations");
            Fortunes.Add("People say you can’t live without love… I think oxygen is more important!");
            Fortunes.Add("Don’t avoid them when they’re angry. They’ll run to you when the temperature rises");
            Fortunes.Add("You are already a king. It’s simply a matter of finding out the nature of your kingdom");
            Fortunes.Add("Put more of your energy into listening than talking");
            Fortunes.Add("Always be true to who you are, and ignore what other people have to say about you");
            Fortunes.Add("Until his dream comes true, a man cannot relax");
            Fortunes.Add("Never Fear Anyone, Never Trust Anyone, Never Depend On anyone");
            Fortunes.Add("The future depends on what we do in the present");
            Fortunes.Add("Don't Quit. Suffer now and live the rest of your life above those who made you suffer");
            Fortunes.Add("Education wil make you a living. Self Education makes you a fortune");
            Fortunes.Add("The worst thing for a rich person to have: \n\n A wife");
            Fortunes.Add("The less we deserve good fortune, the more we hope for it");
            Fortunes.Add("Never do things for the earth's pleasure. Do it for yourself");
            Fortunes.Add("The day you disband yourself from today's society, you separate yourself from confinement into the truth");
            Fortunes.Add("The best way out is always through");
            Fortunes.Add("In a gentle way, you can shake the world. You don't have to be famous, or have a world changing idea");
            Fortunes.Add("Today is the only day. Yesterday is gone");
            Fortunes.Add("Give light and people will find the way");
            Fortunes.Add("Believe in living today. Not in yesterday, nor in tomorrow");
            Fortunes.Add("When deeds speak, words are nothing");
            Fortunes.Add("Silence is the last thing the world will ever hear from you");
            Fortunes.Add("We would accomplish many more things if we did not think of them as impossible");
            Fortunes.Add("'I can't do it' never yet accomplished anything, 'I will try' has performed wonders");
            Fortunes.Add("Start by doing what's necessary, then what's possible, and suddenly you are doing the impossible");
            Fortunes.Add("If not us, who? If not now, when?");
            Fortunes.Add("Knowing too much of your future is never a good thing");
            Fortunes.Add("Once you make a decision, the universe conspires to make it happen");
            Fortunes.Add("No one saves us but ourselves. No one can and no one may. We ourselves must walk the path");
            Fortunes.Add("Learn as if you will live forever, live like you will die tomorrow");
            Fortunes.Add("When you give joy to other people, you get more joy in return. You should give a good thought to happiness that you can give out");
            Fortunes.Add("When you change your thoughts, remember to also change your world");
            Fortunes.Add("It is only when we take chances, when our lives improve. The initial and the most difficult risk that we need to take is to become honest");
            Fortunes.Add("The road to success and the road to failure are almost exactly the same");
            Fortunes.Add("Develop success from failures. Discouragement and failure are two of the surest stepping stones to success");
            Fortunes.Add("Experience is a hard teacher because she gives the test first, the lesson afterwards.");
        }
    }
}
