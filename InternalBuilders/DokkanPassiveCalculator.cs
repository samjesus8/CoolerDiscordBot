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

        private double TURSAMultiplier = 5.05; //Supreme DMG
        private double LRSAMultiplierC = 5.0;
        private double LRSAMultiplierMC = 6.45;

        private double EZATURSAMultiplier = 6.05; //Supreme DMG (EZA)
        private double EZALRSAMultiplierC = 5.25;
        private double EZALRSAMultiplierMC = 6.95;

        private double NormalRaise = 1.3;
        private double GreatRaise = 1.5;
        private double MassiveRaise = 2;

        //Result Variables
        public string ResultATK { get; set; }
        public string ResultDEF { get; set; }

        public DokkanPassiveCalculator(string rarity, int hP, int aTK, int dEF, double leaderSkill, double passiveATK, long passiveDEF, long supportBuff, double DMGReduction, string Links)
        {
            Rarity = rarity.ToUpper();

            if (Rarity == "TUR")
            {
                this.ResultATK = GetATKTUR(aTK, leaderSkill, passiveATK, supportBuff, Links);
                this.ResultDEF = GetDEF(dEF, leaderSkill, passiveDEF, supportBuff, DMGReduction, Links);
            }
            else if (Rarity == "LR")
            {
                this.ResultATK = GetATKLR(aTK, leaderSkill, passiveATK, supportBuff, Links);
                this.ResultDEF = GetDEF(dEF, leaderSkill, passiveDEF, supportBuff, DMGReduction, Links);
            }
            else if (Rarity.ToLower() == "tur(eza)") 
            {
                this.ResultATK = GetATKTUREZA(aTK, leaderSkill, passiveATK, supportBuff, Links);
                this.ResultDEF = GetDEF(dEF, leaderSkill, passiveDEF, supportBuff, DMGReduction, Links);
            }
            else if (Rarity.ToLower() == "lr(eza)")
            {
                this.ResultATK = GetATKLREZA(aTK, leaderSkill, passiveATK, supportBuff, Links);
                this.ResultDEF = GetDEF(dEF, leaderSkill, passiveDEF, supportBuff, DMGReduction, Links);
            }
        }

        //TUR
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
                double supportStep1 = supportStep * KiMultiplier12;

                double step5 = supportStep1 * TURSAMultiplier;
                ATKWithSupport = step5 * NormalRaise; //Raises ATK for 1 Turn
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
            double step4 = (long)(step3 * TURSAMultiplier); //Multiplying the new ATK by SA Multiplier 10

            BaseATKPostSuper = step4 * NormalRaise;

            double linkStep = BaseATKPostSuper * (1 + linksATKMultiplier);
            ATKWithLinks = linkStep;

            OverallATK = BaseATKPostSuper * (1 + support + linksATKMultiplier);
            double OverallATKGreat = step4 * (1 + support + linksATKMultiplier) * GreatRaise;
            double OverallATKMassive = step4 * (1 + support + linksATKMultiplier) * MassiveRaise;

            string FinalMSG = "**Base ATK Stats (Calculated at Normal Raises (30%)** \n" +
                              "ATK Stat after Super (Normal Raise): ***" + BaseATKPostSuper.ToString("N0") + "*** \n" +
                              "ATK Stat With Support: ***" + ATKWithSupport.ToString("N0") + "*** " + " (" + (support * 100) + "% Support) \n" +
                              "ATK Stat With Links: ***" + ATKWithLinks.ToString("N0") + "*** \n\n" + "Links: **" + string.Join(", ", linkSet) + "** \n\n" +
                              "**TOTAL ATK STAT (Passive + Links + Support):** \n" +
                              "Normal Raise (30%) - ***" + OverallATK.ToString("N0") + "*** \n" +
                              "Great Raise (50%) - ***" + OverallATKGreat.ToString("N0") + "*** \n" +
                              "Massive Raise (100%) - ***" + OverallATKMassive.ToString("N0") + "*** \n";

            return FinalMSG;
        }

        public string GetDEF(double DEF, double Lead, double passiveDEF, double support, double DMGReduction, string Links) 
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
            resultPreSuper = step1 * (1 + passiveDEF); //Final DEF value with Leader & Passive

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


            double DEFNormal = resultTotal * NormalRaise;
            double DEFGreat = resultTotal * GreatRaise;
            double DEFMassive = resultTotal * MassiveRaise;

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
                              "DEF Before Super - ***" + resultPreSuper.ToString("N0") + "*** \n" +
                              "DEF With Support - ***" + resultSupport.ToString("N0") + "*** (" + (support * 100) + "% Support) \n" +
                              "DEF with DMG Reduction - ***" + DEFWReduction.ToString("N0") + "*** (" + (DMGReduction * 100) + "% Reduction) \n" +
                              "DEF with Links - ***" + resultLinks.ToString("N0") + "*** \n\n" +
                              "Links: **" + string.Join(", ", linkSet) + "** \n\n" +
                       "**DEF - After Super Attack (WITH SUPPORT + LINKS)** \n\n" +
                              "Raises DEF for 1 Turn - ***" + DEFNormal.ToString("N0") + "*** \n" +
                              "Greatly Raises DEF for 1 Turn - ***" + DEFGreat.ToString("N0") + "*** \n" +
                              "Massively Raises DEF for 1 turn - ***" + DEFMassive.ToString("N0") + "*** \n";

            return finalMSG;
        }

        //LR
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

                double step5Colossal = step4Colossal * LRSAMultiplierC;
                ATKWithSupportC = step5Colossal * NormalRaise; //Raises ATK for 1 Turn, causes colossal DMG

                double step5MegaColossal = step4MegaColossal * LRSAMultiplierMC;
                ATKWithSupportMC = step5MegaColossal * NormalRaise; //Raises ATK for 1 Turn, causes mega-colossal DMG

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

            double step4Colossal1 = (long)(step3Colossal * LRSAMultiplierC); //Multiplying the new ATK by SA Multiplier 20
            BaseATKPostSuperC = (long)(step4Colossal1 * NormalRaise);

            double step4MegaColossal1 = (long)(step3MegaColossal * LRSAMultiplierMC);
            BaseATKPostSuperMC = (long)(step4MegaColossal1 * NormalRaise);

            ATKWithLinksC = BaseATKPostSuperC * (1 + linksATKMultiplier); //Multiplying by Links
            ATKWithLinksMC = BaseATKPostSuperMC * (1 + linksATKMultiplier);

            OverallATKC = BaseATKPostSuperC * (1 + support + linksATKMultiplier);
            OverallATKMC = BaseATKPostSuperMC * (1 + support + linksATKMultiplier);

            double OverallGreatATKC = step4Colossal1 * (1 + support + linksATKMultiplier) * GreatRaise;
            double OverallGreatATKMC = step4MegaColossal1 * (1 + support + linksATKMultiplier) * GreatRaise;

            double OverallMassiveATKC = step4Colossal1 * (1 + support + linksATKMultiplier) * MassiveRaise;
            double OverallMassiveATKMC = step4MegaColossal1 * (1 + support + linksATKMultiplier) * MassiveRaise;

            string FinalMSG = "**Base ATK Stats (Calculated at Normal Raises (30%)** \n" +
                              "ATK Stat When Supering (12 KI): ***" + BaseATKPostSuperC.ToString("N0") + "*** \n" +
                              "ATK Stat When Supering (24 KI): ***" + BaseATKPostSuperMC.ToString("N0") + "*** \n\n" +

                              "ATK Stat with Support (12 KI): ***" + ATKWithSupportC.ToString("N0") + "*** " + " (" + (support * 100) + "% Support)" + "\n" +
                              "ATK Stat with Support (24 KI): ***" + ATKWithSupportMC.ToString("N0") + "*** " + " (" + (support * 100) + "% Support) \n\n" +

                              "ATK Stat with Links (12 KI): ***" + ATKWithLinksC.ToString("N0") + "*** \n" +
                              "ATK Stat with Links (24 KI): ***" + ATKWithLinksMC.ToString("N0") + "*** \n\n" +
                              "Links: **" + string.Join(", ", linkSet) + "** \n\n" +

                              "**TOTAL ATK STATS (Passive + Support + Links)** \n" +
                              "Normal Raise (Colossal) -> ***" + OverallATKC.ToString("N0") + "*** \n" +
                              "Normal Raise (Mega-Colossal) -> ***" + OverallATKMC.ToString("N0") + "*** \n\n" +

                              "Great Raise (Colossal) -> ***" + OverallGreatATKC.ToString("N0") + "*** \n" +
                              "Great Raise (Mega-Colossal) -> ***" + OverallGreatATKMC.ToString("N0") + "*** \n\n" +

                              "Massive Raise (Colossal) -> ***" + OverallMassiveATKC.ToString("N0") + "*** \n" +
                              "Massive Raise (Mega-Colossal) -> ***" + OverallMassiveATKMC.ToString("N0") + "*** \n";

            return FinalMSG;
        }

        //TUR (EZA)
        public string GetATKTUREZA(double ATK, double Lead, double passiveATK, double support, string Links) 
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
                double supportStep1 = supportStep * KiMultiplier12;

                ATKWithSupport = supportStep1 * EZATURSAMultiplier;
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
            double step4 = (long)(step3 * EZATURSAMultiplier); //Multiplying the new ATK by SA Multiplier 10

            BaseATKPostSuper = (long)(step4 * NormalRaise);
            ATKWithLinks = BaseATKPostSuper * (1 + linksATKMultiplier);

            OverallATK = BaseATKPostSuper * (1 + support + linksATKMultiplier);
            double OverallATKGreat = step4 * (1 + support + linksATKMultiplier) * GreatRaise;
            double OverallATKMassive = step4 * (1 + support + linksATKMultiplier) * MassiveRaise;

            string FinalMSG = "**Base ATK Stats (Calculated at Normal Raises (30%)** \n" +
                              "ATK Stat When Supering: ***" + BaseATKPostSuper.ToString("N0") + "*** \n" +
                              "ATK Stat with Support: ***" + ATKWithSupport.ToString("N0") + "*** " + " (" + (support * 100) + "% Support) \n" +
                              "ATK Stat with Links: ***" + ATKWithLinks.ToString("N0") + "*** \n\n" + 
                              "Links: **" + string.Join(", ", linkSet) + "** \n\n" +

                              "**TOTAL ATK STAT (Passive + Links + Support):** \n" +
                              "Normal Raise (30%) - ***" + OverallATK.ToString("N0") + "*** \n" +
                              "Great Raise (50%) - ***" + OverallATKGreat.ToString("N0") + "*** \n" +
                              "Massive Raise (100%) - ***" + OverallATKMassive.ToString("N0") + "*** \n"; ;

            return FinalMSG;
        }

        //LR (EZA)
        public string GetATKLREZA(double ATK, double Lead, double passiveATK, double support, string Links)
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

                double step5Colossal = step4Colossal * EZALRSAMultiplierC;
                ATKWithSupportC = step5Colossal * NormalRaise; //Raises ATK for 1 Turn, causes colossal DMG

                double step5MegaColossal = step4MegaColossal * EZALRSAMultiplierMC;
                ATKWithSupportMC = step5MegaColossal * NormalRaise; //Raises ATK for 1 Turn, causes mega-colossal DMG

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

            double step4Colossal1 = (long)(step3Colossal * EZALRSAMultiplierC); //Multiplying the new ATK by SA Multiplier 20
            BaseATKPostSuperC = (long)(step4Colossal1 * NormalRaise);

            double step4MegaColossal1 = (long)(step3MegaColossal * EZALRSAMultiplierMC);
            BaseATKPostSuperMC = (long)(step4MegaColossal1 * NormalRaise);

            ATKWithLinksC = BaseATKPostSuperC * (1 + linksATKMultiplier); //Multiplying by Links
            ATKWithLinksMC = BaseATKPostSuperMC * (1 + linksATKMultiplier);

            OverallATKC = BaseATKPostSuperC * (1 + support + linksATKMultiplier);
            OverallATKMC = BaseATKPostSuperMC * (1 + support + linksATKMultiplier);

            double OverallGreatATKC = step4Colossal1 * (1 + support + linksATKMultiplier) * GreatRaise;
            double OverallGreatATKMC = step4MegaColossal1 * (1 + support + linksATKMultiplier) * GreatRaise;

            double OverallMassiveATKC = step4Colossal1 * (1 + support + linksATKMultiplier) * MassiveRaise;
            double OverallMassiveATKMC = step4MegaColossal1 * (1 + support + linksATKMultiplier) * MassiveRaise;

            string FinalMSG = "**Base ATK Stats (Calculated at Normal Raises (30%)** \n" +
                              "ATK Stat When Supering (12 KI): ***" + BaseATKPostSuperC.ToString("N0") + "*** \n" +
                              "ATK Stat When Supering (24 KI): ***" + BaseATKPostSuperMC.ToString("N0") + "*** \n\n" +

                              "ATK Stat with Support (12 KI): ***" + ATKWithSupportC.ToString("N0") + "*** " + " (" + (support * 100) + "% Support)" + "\n" +
                              "ATK Stat with Support (24 KI): ***" + ATKWithSupportMC.ToString("N0") + "*** " + " (" + (support * 100) + "% Support) \n\n" +

                              "ATK Stat with Links (12 KI): ***" + ATKWithLinksC.ToString("N0") + "*** \n" +
                              "ATK Stat with Links (24 KI): ***" + ATKWithLinksMC.ToString("N0") + "*** \n\n" +
                              "Links: **" + string.Join(", ", linkSet) + "** \n\n" +

                              "**TOTAL ATK STATS (Passive + Support + Links)** \n" +
                              "Normal Raise (Colossal) -> ***" + OverallATKC.ToString("N0") + "*** \n" +
                              "Normal Raise (Mega-Colossal) -> ***" + OverallATKMC.ToString("N0") + "*** \n\n" +

                              "Great Raise (Colossal) -> ***" + OverallGreatATKC.ToString("N0") + "*** \n" +
                              "Great Raise (Mega-Colossal) -> ***" + OverallGreatATKMC.ToString("N0") + "*** \n\n" +

                              "Massive Raise (Colossal) -> ***" + OverallMassiveATKC.ToString("N0") + "*** \n" +
                              "Massive Raise (Mega-Colossal) -> ***" + OverallMassiveATKMC.ToString("N0") + "*** \n";

            return FinalMSG;
        }
    }
}
