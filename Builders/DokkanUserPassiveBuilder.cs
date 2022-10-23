using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace DiscordBotTest.Builders
{
    internal class DokkanUserPassiveBuilder
    {
        public string UserName { get; set; }
        public string PassiveName { get; set; }

        public int UnitHP { get; set; }
        public int UnitATK { get; set; }
        public int UnitDEF { get; set; }

        public string UnitLeaderName { get; set; }
        public int UnitLeaderSkill { get; set; }

        public int UnitPassiveATK { get; set; }
        public int UnitPassiveDEF { get; set; }

        public int Support { get; set; }
        public string Links { get; set; }

        public DokkanUserPassiveBuilder(string user, string Name, int unitHP, int unitATK, int unitDEF, string leaderName, int unitLeaderSkill, int unitPassiveATK, int unitPassiveDEF, int support, string links)
        {
            UserName = user;
            PassiveName = Name;
            UnitHP = unitHP;
            UnitATK = unitATK;
            UnitDEF = unitDEF;
            UnitLeaderName = leaderName;
            UnitLeaderSkill = unitLeaderSkill;
            UnitPassiveATK = unitPassiveATK;
            UnitPassiveDEF = unitPassiveDEF;
            Support = support;
            Links = links;
        }

        public DokkanUserPassiveBuilder(string name) 
        {
            ReadJSONFile(name);
        }

        private void ReadJSONFile(string nameSearch) 
        {
            List<object> Passives = new List<object>();
            try 
            {
                using (StreamReader sr = new StreamReader("UserPassivesStorage.json"))
                {
                    string jsonFile = sr.ReadToEnd();
                    JSONObject100 jsonObject = JsonConvert.DeserializeObject<JSONObject100>(jsonFile);

                    Passives.Add(jsonObject);

                    var random = new Random();
                    var data = jsonObject.members[random.Next(0, jsonObject.members.Length)];

                    this.UserName = data.userName;
                    this.PassiveName = data.passiveName;
                    this.UnitHP = data.unitHP;
                    this.UnitATK = data.unitATK;
                    this.UnitDEF = data.unitDEF;
                    this.UnitLeaderName = data.leaderName;
                    this.UnitLeaderSkill = data.leaderValue;
                    this.UnitPassiveATK = data.passiveATK;
                    this.UnitPassiveDEF = data.passiveDEF;
                    this.Support = data.supportBuff;
                    this.Links = data.links;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void StoreUserPassives(DokkanUserPassiveBuilder classObj) 
        {
            using (StreamWriter sw = new StreamWriter("UserPassivesStorage.json", true))
            {
                var members = new List<DokkanUserPassiveBuilder> { classObj };
                var data = new Members
                {
                    passiveName = classObj.PassiveName
                };

                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                sw.WriteLine(json);
                sw.Close();
            }
        }
    }

    class JSONObject100 
    {
        public string userPassiveStorage { get; set; }
        public Members[] members { get; set; }
    }

    class Members 
    {
        public string userName { get; set; }
        public string passiveName { get; set; }
        public string leaderName { get; set; }
        public int unitHP { get; set; }
        public int unitATK { get; set; }
        public int unitDEF { get; set; }
        public int leaderValue { get; set; }
        public int passiveATK { get; set; }
        public int passiveDEF { get; set; }
        public int supportBuff { get; set; }
        public string links { get; set; }
    }
}
