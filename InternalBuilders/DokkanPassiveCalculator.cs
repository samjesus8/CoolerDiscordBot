using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotTest.Builders
{
    internal class DokkanPassiveCalculator
    {
        //User Input
        public string Rarity { get; set; }
        public long HP { get; set; }
        public long ATK { get; set; }
        public long DEF { get; set; }
        public long LeaderSkill { get; set; }
        public long PassiveATK { get; set; }
        public long PassiveDEF { get; set; }
        public long Support { get; set; }

        //Constants
        private double KiMultiplier12 = 1.5;
        private double KiMultiplier24 = 2;

        private double TURSAMultiplier = 5.8;
        private double LRSAMultiplierC = 4.95;
        private double LRSAMultiplierMC = 6.4;

        private double DEFRaise = 1.3;
        private double GreatDEFRaise = 1.5;
        private double MassiveDEFRaise = 2;

        //Result Variables
        public string ResultATK { get; set; }
        public string ResultDEF { get; set; }

        public DokkanPassiveCalculator(string rarity, int hP, int aTK, int dEF, double leaderSkill, double passiveATK, long passiveDEF, long supportBuff, double DMGReduction)
        {
            Rarity = rarity.ToUpper();

            if (Rarity == "TUR")
            {
                this.ResultATK = GetATKTUR(aTK, leaderSkill, passiveATK, supportBuff);
                this.ResultDEF = GetDEFTUR(dEF, leaderSkill, passiveDEF, supportBuff, DMGReduction);
            }
            else if (Rarity == "LR")
            {
                this.ResultATK = GetATKLR(aTK, leaderSkill, passiveATK, supportBuff);
                this.ResultDEF = GetDEFLR(dEF, leaderSkill, passiveDEF, supportBuff, DMGReduction);
            }
        }

        public string GetATKTUR(double ATK, double Lead, double passiveATK, double support) 
        {
            double BaseATKPostSuper = 0; //ATK without any buffs
            double ATKWithSupport = 0; //ATK with ONLY Support
            double ATKWithLinks = 0; //ATK with ONLY Links
            double OverallATK = 0; //ATK with everything combined

            Lead = Lead / 100;
            passiveATK = passiveATK / 100;


            double step1 = ATK * (1 + Lead + Lead); //Multiplying ATK by Leader Skill X2
            double step2 = step1 * (1 + passiveATK); //Multiplying new ATK by Passive buffs (ALL IN TOTAL)

            if (support == 0)
            {
                support = 0; //This means there is no support
                ATKWithSupport = 0;
            }
            else
            {
                support = support / 100; //Calculate multiplier
                double supportStep = step2 * (1 + support); //Multiplying by any Support from allies
                double step4 = supportStep * KiMultiplier12;

                ATKWithSupport = step4 * TURSAMultiplier;
                //Continue
            }

            double step3 = (long)(step2 * KiMultiplier12); //Multiplying the new ATK by 12 Ki Multiplier
            BaseATKPostSuper = (long)(step3 * TURSAMultiplier); //Multiplying the new ATK by SA Multiplier 10

            string FinalMSG = "ATK Stat When Supering: ***" + BaseATKPostSuper.ToString("N0") + "*** \n" +
                              "ATK Stat with Support: ***" + ATKWithSupport.ToString("N0") + "*** " + " (" + (support * 100) + "% Support)";

            return FinalMSG;
        }

        public string GetDEFTUR(double DEF, double Lead, double passiveDEF, double support, double DMGReduction) 
        {
            double resultPreSuper = 0;
            double DEFWReduction = 0;
            double resultSupport = 0;

            string finalMSG;

            Lead = Lead / 100;
            passiveDEF = passiveDEF / 100;
            support = support / 100;

            var step1 = DEF * (1 + Lead + Lead); //Multiplying DEF by Leader Skill x2
            resultPreSuper = step1 * (1 + PassiveDEF); //Final DEF value with Leader & Passive

            //Optional Support Buffs go here

            resultSupport = resultPreSuper * (1 + Support);

            double DEFNormal = resultPreSuper * DEFRaise;
            double DEFGreat = resultPreSuper * GreatDEFRaise;
            double DEFMassive = resultPreSuper * MassiveDEFRaise;

            if (DMGReduction == 0)
            {
                DMGReduction = 0;
                DEFWReduction = resultPreSuper;
            }
            else
            {
                DMGReduction = DMGReduction / 100;
                DEFWReduction = resultPreSuper * (1 + DMGReduction);
            }

            finalMSG = "**DEF Values in Set Conditions** \n\n" +
                              "DEF Pre Super - ***" + resultPreSuper.ToString("N0") + "*** \n" +
                              "DEF With Support - ***" + resultSupport.ToString("N0") + "*** ( " + (support * 100) + "% Support) \n" +
                              "DEF with DMG Reduction - ***" + DEFWReduction.ToString("N0") + "*** (" + (DMGReduction * 100) + "% Reduction) \n\n" +
                              "**DEF - Super Attack Raises** \n\n" +
                              "Raises DEF for 1 Turn - ***" + DEFNormal.ToString("N0") + "*** \n" +
                              "Greatly Raises DEF for 1 Turn - ***" + DEFGreat.ToString("N0") + "*** \n" +
                              "Massively Raises DEF for 1 turn - ***" + DEFMassive.ToString("N0") + "*** \n";

            return finalMSG;
        }

        public string GetATKLR(double ATK, double Lead, double passiveATK, double support) 
        {
            double BaseATKPostSuperC = 0; //ATK without any buffs
            double BaseATKPostSuperMC = 0;

            double ATKWithSupportC = 0; //ATK with ONLY Support
            double ATKWithSupportMC = 0;

            double ATKWithLinks = 0; //ATK with ONLY Links
            double OverallATK = 0; //ATK with everything combined

            Lead = Lead / 100;
            passiveATK = passiveATK / 100;


            double step1 = ATK * (1 + Lead + Lead); //Multiplying ATK by Leader Skill X2
            double step2 = step1 * (1 + passiveATK); //Multiplying new ATK by Passive buffs (ALL IN TOTAL)

            if (support == 0)
            {
                support = 0; //This means there is no support
                ATKWithSupportC = 0;
                ATKWithSupportMC = 0;
            }
            else
            {
                support = support / 100; //Calculate multiplier
                double supportStep = step2 * (1 + support); //Multiplying by any Support from allies
                double step4Colossal = supportStep * KiMultiplier12;
                double step4MegaColossal = supportStep * KiMultiplier24;

                ATKWithSupportC = step4Colossal * LRSAMultiplierC;
                ATKWithSupportMC = step4MegaColossal * LRSAMultiplierMC;

                //Continue
            }

            double step3Colossal = (long)(step2 * KiMultiplier12); //Multiplying the new ATK by 24 Ki Multiplier
            double step3MegaColossal = (long)(step2 * KiMultiplier24);
            BaseATKPostSuperC = (long)(step3Colossal * LRSAMultiplierC); //Multiplying the new ATK by SA Multiplier 20
            BaseATKPostSuperMC = (long)(step3MegaColossal * LRSAMultiplierMC);

            string FinalMSG = "ATK Stat When Supering (Colossal - AT 12 KI): ***" + BaseATKPostSuperC.ToString("N0") + "*** \n" +
                              "ATK Stat When Supering (Mega-Colossal - 24 KI): ***" + BaseATKPostSuperMC.ToString("N0") + "*** \n\n" +

                              "ATK Stat with Support (Colossal - AT 12 KI): ***" + ATKWithSupportC.ToString("N0") + "*** " + " (" + (support * 100) + "% Support)" + "\n" +
                              "ATK Stat with Support (Mega-Colossal - 24 KI): ***" + ATKWithSupportMC.ToString("N0") + "*** " + " (" + (support * 100) + "% Support)";

            return FinalMSG;
        }

        public string GetDEFLR(double DEF, double Lead, double passiveDEF, double support, double DMGReduction) 
        {
            double resultPreSuper = 0;
            double resultSupport = 0;
            string finalMSG;

            Lead = Lead / 100;
            passiveDEF = passiveDEF / 100;
            support = support / 100;

            var step1 = DEF * (1 + Lead + Lead); //Multiplying DEF by Leader Skill x2
            resultPreSuper = step1 * (1 + passiveDEF); //Final DEF Value with Both Lead & Passive Buffs

            //Optional Support Buffs go here

            resultSupport = resultPreSuper * (1 + support);

            double DEFNormal = resultPreSuper * DEFRaise;
            double DEFWReduction = DEFNormal * (1 + DMGReduction);
            double DEFGreat = resultPreSuper * GreatDEFRaise;
            double DEFMassive = resultPreSuper * MassiveDEFRaise;

            if (DMGReduction == 0)
            {
                DMGReduction = 0;
                DEFWReduction = resultPreSuper;
            }
            else
            {
                DMGReduction = DMGReduction / 100;
                DEFWReduction = resultPreSuper * (1 + DMGReduction);
            }

            finalMSG = "**DEF Values in Set Conditions** \n\n" +
                              "DEF Pre Super - ***" + resultPreSuper.ToString("N0") + "*** \n" +
                              "DEF With Support - ***" + resultSupport.ToString("N0") + "*** ( " + (support * 100) + "% Support) \n" +
                              "DEF with DMG Reduction - ***" + DEFWReduction.ToString("N0") + "*** (" + (DMGReduction * 100) + "% Reduction) \n\n" +
                              "**DEF - Super Attack Raises** \n\n" +
                              "Raises DEF for 1 Turn - ***" + DEFNormal.ToString("N0") + "*** \n" +
                              "Greatly Raises DEF for 1 Turn - ***" + DEFGreat.ToString("N0") + "*** \n" +
                              "Massively Raises DEF for 1 turn - ***" + DEFMassive.ToString("N0") + "*** \n";

            return finalMSG;
        }
    }
}
