using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DiscordBotTest.Builders
{
    internal class DokkanUserPassiveBuilder
    {
        public int Count = 0;

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

        public DokkanUserPassiveBuilder() { }

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

                    for (int i = 0; i < jsonObject.members.Length; i++)
                    {
                        if (i < jsonObject.members.Length)
                        {
                            var data = jsonObject.members[i];
                            Passives.Add(data);
                        }
                        else
                        {
                            Console.Write("Error");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void StoreUserPassives(DokkanUserPassiveBuilder classObj)
        {
            try
            {
                var path = @"D:\Visual Studio Projects\DiscordBotTest\bin\Debug\UserPassivesStorage.json";
                var json = File.ReadAllText(path);

                var jsonObj = JObject.Parse(json);

                var members = jsonObj["members"].ToObject<List<DokkanUserPassiveBuilder>>();
                members.Add(classObj);

                jsonObj["members"] = JArray.FromObject(members);

                File.WriteAllText(path, jsonObj.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
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
        public string UserName { get; set; }
        public string PassiveName { get; set; }
        public string UnitLeaderName { get; set; }
        public int UnitHP { get; set; }
        public int UnitATK { get; set; }
        public int UnitDEF { get; set; }
        public int UnitLeaderSkill { get; set; }
        public int UnitPassiveATK { get; set; }
        public int UnitPassiveDEF { get; set; }
        public int Support { get; set; }
        public string Links { get; set; }
    }
}
