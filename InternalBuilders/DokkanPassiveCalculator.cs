using DiscordBotTest.InternalBuilders;

namespace DiscordBotTest.Builders
{
    internal class DokkanPassiveCalculator
    {
        private DokkanLinks _LinkEngine = new DokkanLinks();

        //User Input
        public string Rarity { get; set; }
        public long HP { get; set; }
        public long ATK { get; set; }
        public long DEF { get; set; }
        public long LeaderSkill { get; set; }
        public long PassiveATK { get; set; }
        public long PassiveDEF { get; set; }
        public long Support { get; set; }
        public string[] Links { get; set; }

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

        public DokkanPassiveCalculator(string rarity, int hP, int aTK, int dEF, double leaderSkill, double passiveATK, long passiveDEF, long supportBuff, double DMGReduction, string Links)
        {
            Rarity = rarity.ToUpper();

            if (Rarity == "TUR")
            {
                this.ResultATK = GetATKTUR(aTK, leaderSkill, passiveATK, supportBuff, Links);
                this.ResultDEF = GetDEFTUR(dEF, leaderSkill, passiveDEF, supportBuff, DMGReduction, Links);
            }
            else if (Rarity == "LR")
            {
                this.ResultATK = GetATKLR(aTK, leaderSkill, passiveATK, supportBuff, Links);
                this.ResultDEF = GetDEFLR(dEF, leaderSkill, passiveDEF, supportBuff, DMGReduction, Links);
            }
        }

        public string GetATKTUR(double ATK, double Lead, double passiveATK, double support, string Links) 
        {
            double BaseATKPostSuper = 0; //ATK without any buffs
            double ATKWithSupport = 0; //ATK with ONLY Support
            double ATKWithLinks = 0; //ATK with ONLY Links
            double OverallATK = 0; //ATK with everything combined

            double linksATKMultiplier = 0; //Multiplier for all Link Buffs Combined

            Lead = Lead / 100;
            passiveATK = passiveATK / 100;

            //NO BUFFS

            double step1 = ATK * (1 + Lead + Lead); //Multiplying ATK by Leader Skill X2
            double step2 = step1 * (1 + passiveATK); //Multiplying new ATK by Passive buffs (ALL IN TOTAL)

            //SUPPORT BUFFS

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

            //LINKS

            string[] linkSet = Links.Split(' ');
            _LinkEngine.LoadLinks();

            for (int i = 0; i < linkSet.Length; i++) 
            {
                if (_LinkEngine.Links.ContainsKey(linkSet[i]))
                {
                    _LinkEngine.Links.TryGetValue(linkSet[i], out var TempLink); //Get Link info from Dictionary if it exists

                    linksATKMultiplier = linksATKMultiplier + TempLink.ATK; //Add the buffs to the multiplier variables
                }
            }
            linksATKMultiplier = linksATKMultiplier / 100;

            double step3 = (long)(step2 * KiMultiplier12); //Multiplying the new ATK by 12 Ki Multiplier
            BaseATKPostSuper = (long)(step3 * TURSAMultiplier); //Multiplying the new ATK by SA Multiplier 10
            ATKWithLinks = BaseATKPostSuper * (1 + linksATKMultiplier);

            OverallATK = BaseATKPostSuper * (1 + support + linksATKMultiplier);

            string FinalMSG = "ATK Stat When Supering: ***" + BaseATKPostSuper.ToString("N0") + "*** \n" +
                              "ATK Stat with Support: ***" + ATKWithSupport.ToString("N0") + "*** " + " (" + (support * 100) + "% Support) \n" +
                              "ATK Stat with Links: ***" + ATKWithLinks.ToString("N0") + "*** \n\n" + "Links: **" + string.Join(", ", linkSet) + "** \n\n" +
                              "TOTAL ATK STAT (Passive + Links + Support) -> ***" + OverallATK.ToString("N0") + "***";

            return FinalMSG;
        }

        public string GetDEFTUR(double DEF, double Lead, double passiveDEF, double support, double DMGReduction, string Links) 
        {
            double resultPreSuper = 0;
            double DEFWReduction = 0;
            double resultSupport = 0;
            double resultLinks = 0;
            double resultTotal = 0;
            double linksDEFMultiplier = 0;

            string finalMSG;

            Lead = Lead / 100;
            passiveDEF = passiveDEF / 100;
            support = support / 100;

            var step1 = DEF * (1 + Lead + Lead); //Multiplying DEF by Leader Skill x2
            resultPreSuper = step1 * (1 + PassiveDEF); //Final DEF value with Leader & Passive

            //Optional Support Buffs go here
            //LINKS

            string[] linkSet = Links.Split(' ');
            _LinkEngine.LoadLinks();

            for (int i = 0; i < linkSet.Length; i++)
            {
                if (_LinkEngine.Links.ContainsKey(linkSet[i]))
                {
                    _LinkEngine.Links.TryGetValue(linkSet[i], out var TempLink); //Get Link info from Dictionary if it exists

                    linksDEFMultiplier = linksDEFMultiplier + TempLink.DEF; //Add the buffs to the multiplier variables
                }
            }
            linksDEFMultiplier = linksDEFMultiplier / 100;
            resultLinks = resultPreSuper * (1 + linksDEFMultiplier);

            resultSupport = resultPreSuper * (1 + Support);
            resultTotal = resultPreSuper * (1 + Support + linksDEFMultiplier);


            double DEFNormal = resultTotal * DEFRaise;
            double DEFGreat = resultTotal * GreatDEFRaise;
            double DEFMassive = resultTotal * MassiveDEFRaise;

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
                              "DEF with DMG Reduction - ***" + DEFWReduction.ToString("N0") + "*** (" + (DMGReduction * 100) + "% Reduction) \n" +
                              "DEF with Links - ***" + resultLinks.ToString("N0") + "*** \n\n" +
                              "Links: **" + string.Join(", ", linkSet) + "** \n\n" +
                              "**DEF - Super Attack Raises (WITH SUPPORT + LINKS)** \n\n" +
                              "Raises DEF for 1 Turn - ***" + DEFNormal.ToString("N0") + "*** \n" +
                              "Greatly Raises DEF for 1 Turn - ***" + DEFGreat.ToString("N0") + "*** \n" +
                              "Massively Raises DEF for 1 turn - ***" + DEFMassive.ToString("N0") + "*** \n";

            return finalMSG;
        }

        public string GetATKLR(double ATK, double Lead, double passiveATK, double support, string Links) 
        {
            double linksATKMultiplier = 0;

            double BaseATKPostSuperC = 0; //ATK without any buffs
            double BaseATKPostSuperMC = 0;

            double ATKWithSupportC = 0; //ATK with ONLY Support
            double ATKWithSupportMC = 0;

            double ATKWithLinksC = 0; //ATK with ONLY Links
            double ATKWithLinksMC = 0;

            double OverallATKC = 0; //ATK with everything combined
            double OverallATKMC = 0;

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

            //LINKS

            string[] linkSet = Links.Split(' ');
            _LinkEngine.LoadLinks();

            for (int i = 0; i < linkSet.Length; i++)
            {
                if (_LinkEngine.Links.ContainsKey(linkSet[i]))
                {
                    _LinkEngine.Links.TryGetValue(linkSet[i], out var TempLink); //Get Link info from Dictionary if it exists

                    linksATKMultiplier = linksATKMultiplier + TempLink.ATK; //Add the buffs to the multiplier variables
                }
            }
            linksATKMultiplier = linksATKMultiplier / 100;

            double step3Colossal = (long)(step2 * KiMultiplier12); //Multiplying the new ATK by 24 Ki Multiplier
            double step3MegaColossal = (long)(step2 * KiMultiplier24);
            BaseATKPostSuperC = (long)(step3Colossal * LRSAMultiplierC); //Multiplying the new ATK by SA Multiplier 20
            BaseATKPostSuperMC = (long)(step3MegaColossal * LRSAMultiplierMC);

            ATKWithLinksC = BaseATKPostSuperC * (1 + linksATKMultiplier); //Multiplying by Links
            ATKWithLinksMC = BaseATKPostSuperMC * (1 + linksATKMultiplier);

            OverallATKC = BaseATKPostSuperC * (1 + support + linksATKMultiplier);
            OverallATKMC = BaseATKPostSuperMC * (1 + support + linksATKMultiplier);

            string FinalMSG = "ATK Stat When Supering (Colossal - AT 12 KI): ***" + BaseATKPostSuperC.ToString("N0") + "*** \n" +
                              "ATK Stat When Supering (Mega-Colossal - 24 KI): ***" + BaseATKPostSuperMC.ToString("N0") + "*** \n\n" +

                              "ATK Stat with Support (Colossal - AT 12 KI): ***" + ATKWithSupportC.ToString("N0") + "*** " + " (" + (support * 100) + "% Support)" + "\n" +
                              "ATK Stat with Support (Mega-Colossal - 24 KI): ***" + ATKWithSupportMC.ToString("N0") + "*** " + " (" + (support * 100) + "% Support) \n\n" +
                              
                              "ATK Stat with Links (Colossal - AT 12 KI): ***" + ATKWithLinksC.ToString("N0") + "*** \n" +
                              "ATK Stat with Links (Mega-Colossal - 24 KI): ***" + ATKWithLinksMC.ToString("N0") + "*** \n\n" +
                              "Links: **" + string.Join(", ", linkSet) + "** \n\n" +
                              
                              "**TOTAL ATK STATS (Passive + Support + Links)** \n\n" +
                              
                              "TOTAL ATK STAT (Colossal) -> ***" + OverallATKC.ToString("N0") + "*** \n" +
                              "TOTAL ATK STAT (Mega-Colossal) -> ***" + OverallATKMC.ToString("N0") + "***";

            return FinalMSG;
        }

        public string GetDEFLR(double DEF, double Lead, double passiveDEF, double support, double DMGReduction, string Links) 
        {
            double linksDEFMultiplier = 0;
            double resultPreSuper = 0;
            double resultSupport = 0;
            double resultLinks = 0;
            double resultTotal = 0;
            string finalMSG;

            Lead = Lead / 100;
            passiveDEF = passiveDEF / 100;
            support = support / 100;

            var step1 = DEF * (1 + Lead + Lead); //Multiplying DEF by Leader Skill x2
            resultPreSuper = step1 * (1 + passiveDEF); //Final DEF Value with Both Lead & Passive Buffs

            //Optional Support Buffs go here
            //LINKS

            string[] linkSet = Links.Split(' ');
            _LinkEngine.LoadLinks();

            for (int i = 0; i < linkSet.Length; i++)
            {
                if (_LinkEngine.Links.ContainsKey(linkSet[i]))
                {
                    _LinkEngine.Links.TryGetValue(linkSet[i], out var TempLink); //Get Link info from Dictionary if it exists

                    linksDEFMultiplier = linksDEFMultiplier + TempLink.DEF; //Add the buffs to the multiplier variables
                }
            }
            linksDEFMultiplier = linksDEFMultiplier / 100;
            resultLinks = resultPreSuper * (1 + linksDEFMultiplier);

            resultSupport = resultPreSuper * (1 + support);
            resultTotal = resultPreSuper * (1 + support + linksDEFMultiplier);

            double DEFNormal = resultTotal * DEFRaise;
            double DEFWReduction = DEFNormal * (1 + DMGReduction);
            double DEFGreat = resultTotal * GreatDEFRaise;
            double DEFMassive = resultTotal * MassiveDEFRaise;

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
                              "DEF with DMG Reduction - ***" + DEFWReduction.ToString("N0") + "*** (" + (DMGReduction * 100) + "% Reduction) \n" +
                              "DEF with Links - ***" + resultLinks.ToString("N0") + "*** \n\n" +
                              "Links: **" + string.Join(", ", linkSet) + "** \n\n" +
                              "**DEF - Super Attack Raises (WITH LINKS + SUPPORT)** \n\n" +
                              "Raises DEF for 1 Turn - ***" + DEFNormal.ToString("N0") + "*** \n" +
                              "Greatly Raises DEF for 1 Turn - ***" + DEFGreat.ToString("N0") + "*** \n" +
                              "Massively Raises DEF for 1 turn - ***" + DEFMassive.ToString("N0") + "*** \n";

            return finalMSG;
        }
    }
}
