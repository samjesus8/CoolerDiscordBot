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
        public long HP { get; set; }
        public long ATK { get; set; }
        public long DEF { get; set; }
        public long LeaderSkill { get; set; }
        public long PassiveATK { get; set; }
        public long PassiveDEF { get; set; }

        //Constants
        private double KiMultiplier12 = 1.5;
        private double KiMultiplier24 = 2;
        private double SAMultiplierWithRainbow = 5.8;

        private double DEFRaise = 1.3;
        private double GreatDEFRaise = 1.5;
        private double MassiveDEFRaise = 2;

        //Result Variables
        public long ResultATK { get; set; }
        public (double, string) ResultDEF { get; set; }

        public DokkanPassiveCalculator(int hP, int aTK, int dEF, double leaderSkill, double passiveATK, long passiveDEF)
        {
            this.ResultATK = (long)GetATK(aTK, leaderSkill, passiveATK);
            this.ResultDEF = GetDEF(dEF, leaderSkill, passiveATK);
        }

        public double GetATK(double ATK, double Lead, double passiveATK) 
        {
            double result = 0;
            Lead = Lead / 100;
            passiveATK = passiveATK / 100;

            double step1 = ATK * (1 + Lead + Lead); //Multiplying ATK by Leader Skill X2
            double step2 = step1 * (1 + passiveATK); //Multiplying new ATK by Passive buffs (ALL IN TOTAL)

            //Optional Support Buffs go here

            double step3 = (long)(step2 * KiMultiplier12); //Multiplying the new ATK by 12 Ki Multiplier
            result = (long)(step3 * SAMultiplierWithRainbow); //Multiplying the new ATK by SA Multiplier 10

            return result;
        }

        public (double, string) GetDEF(double DEF, double Lead, double passiveDEF) 
        {
            double resultPreSuper = 0;
            string resultPostSuper;
            Lead = Lead / 100;
            passiveDEF = passiveDEF / 100;

            resultPreSuper = DEF * (1 + Lead + Lead); //Multiplying DEF by Leader Skill x2

            //Optional Support Buffs go here

            double DEFNormal = resultPreSuper * DEFRaise;
            double DEFGreat = resultPreSuper * GreatDEFRaise;
            double DEFMassive = resultPreSuper * MassiveDEFRaise;

            resultPostSuper = "**DEF Values in Set Conditions** \n\n" +
                              "Raises DEF for 1 Turn - ***" + DEFNormal + "*** \n" +
                              "Greatly Raises DEF for 1 Turn - ***" + DEFGreat + "*** \n" +
                              "Massively Raises DEF for 1 turn - ***" + DEFMassive + "*** \n";

            return (resultPreSuper, resultPostSuper);
        }
    }
}
