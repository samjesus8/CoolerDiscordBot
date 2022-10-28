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
        public string Error { get; set; }

        public string UserName { get; set; }
        public string PassiveName { get; set; }
        public string Rarity { get; set; }

        public int UnitHP { get; set; }
        public int UnitATK { get; set; }
        public int UnitDEF { get; set; }

        public string UnitLeaderName { get; set; }
        public int UnitLeaderSkill { get; set; }

        public int UnitPassiveATK { get; set; }
        public int UnitPassiveDEF { get; set; }

        public int DmgReduction { get; set; }
        public int Support { get; set; }
        public string Links { get; set; }

        private Members Json { get; set; }
        public List<Members> membersJSONList = new List<Members>();

        public DokkanUserPassiveBuilder(string user, string Name, string rarity, int unitHP, int unitATK, int unitDEF, string leaderName, int unitLeaderSkill, int unitPassiveATK, int unitPassiveDEF, int dmgReduction, int support, string links)
        {
            UserName = user;
            PassiveName = Name;
            Rarity = rarity;
            UnitHP = unitHP;
            UnitATK = unitATK;
            UnitDEF = unitDEF;
            UnitLeaderName = leaderName;
            UnitLeaderSkill = unitLeaderSkill;
            UnitPassiveATK = unitPassiveATK;
            DmgReduction = dmgReduction;
            UnitPassiveDEF = unitPassiveDEF;
            Support = support;
            Links = links;
        }

        public DokkanUserPassiveBuilder(string user, string passiveName) 
        {
            GetUserPassivesList(user, passiveName);
        }

        public DokkanUserPassiveBuilder(string name) 
        {
            try 
            {
                ReadJSONFile(name);
                UserName = Json.UserName;
                PassiveName = Json.PassiveName;
                Rarity = Json.Rarity;
                UnitHP = Json.UnitHP;
                UnitATK = Json.UnitATK;
                UnitDEF = Json.UnitDEF;
                UnitLeaderName = Json.UnitLeaderName;
                UnitLeaderSkill = Json.UnitLeaderSkill;
                UnitPassiveATK = Json.UnitPassiveATK;
                UnitPassiveDEF = Json.UnitPassiveDEF;
                DmgReduction = Json.DMGReduction;
                Support = Json.Support;
                Links = Json.Links;
            }
            catch (Exception ex)
            {
                Error = ex.Message.ToString();
            }
        }

        public DokkanUserPassiveBuilder() 
        {

        }

        private void ReadJSONFile(string nameSearch) 
        {
            try 
            {
                using (StreamReader sr = new StreamReader("UserPassivesStorage.json"))
                {
                    string jsonFile = sr.ReadToEnd();
                    JSONObject100 jsonObject = JsonConvert.DeserializeObject<JSONObject100>(jsonFile);

                    for (int i = 0; i < jsonObject.members.Length; i++)
                    {
                        var member = jsonObject.members[i];
                        if (member.PassiveName == nameSearch || member.PassiveName.ToLower() == nameSearch)
                        {
                            this.Json = member;
                        }
                        else 
                        {
                            //No Match
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void GetUserPassivesList(string nameSearch, string passiveName) 
        {
            try
            {
                using (StreamReader sr = new StreamReader("UserPassivesStorage.json")) 
                {
                    string jsonFile = sr.ReadToEnd();
                    JSONObject100 jsonObject = JsonConvert.DeserializeObject<JSONObject100>(jsonFile);

                    for (int i = 0; i < jsonObject.members.Length; i++) 
                    {
                        var member = jsonObject.members[i];
                        if (member.UserName == nameSearch)
                        {
                            //Add to list
                            membersJSONList.Add(member);
                        }
                        else 
                        {
                            //Continue Search
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                Error = ex.Message;
            }
        }

        public void GetSpecificPassive(string passiveName) 
        {
            try
            {
                using (StreamReader sr = new StreamReader("UserPassivesStorage.json"))
                {
                    string jsonFile = sr.ReadToEnd();
                    JSONObject100 jsonObject = JsonConvert.DeserializeObject<JSONObject100>(jsonFile);

                    for (int i = 0; i < jsonObject.members.Length; i++)
                    {
                        var member = jsonObject.members[i];
                        if (member.PassiveName == passiveName || member.PassiveName.ToLower() == passiveName)
                        {
                            UserName = member.UserName;
                            PassiveName = member.PassiveName;
                            Rarity = member.Rarity;
                            UnitHP = member.UnitHP;
                            UnitATK = member.UnitATK;
                            UnitDEF = member.UnitDEF;
                            UnitLeaderName = member.UnitLeaderName;
                            UnitLeaderSkill = member.UnitLeaderSkill;
                            UnitPassiveATK = member.UnitPassiveATK;
                            UnitPassiveDEF = member.UnitPassiveDEF;
                            DmgReduction = member.DMGReduction;
                            Support = member.Support;
                            Links = member.Links;
                        }
                        else
                        {
                            //No Match
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public bool DeleteSpecificPassive(DokkanUserPassiveBuilder classObj)
        {
            try 
            {
                //var path = @"C:\Users\samue\Documents\Bot\bin\Debug\UserPassivesStorage.json";
                var path = @"D:\Visual Studio Projects\DiscordBotTest\bin\Debug\UserPassivesStorage.json";
                var json = File.ReadAllText(path);

                var jsonObj = JObject.Parse(json);

                var members = jsonObj["members"].ToObject<List<DokkanUserPassiveBuilder>>();

                var found = members.FirstOrDefault(m => m.PassiveName == classObj.PassiveName);
                if (found != null)
                {
                    members.Remove(found);
                    jsonObj["members"] = JArray.FromObject(members);

                    File.WriteAllText(path, jsonObj.ToString());
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
                Error = ex.ToString();
                return false;
            }
        }

        public void StoreUserPassives(DokkanUserPassiveBuilder classObj)
        {
            try
            {
                //var path = @"C:\Users\samue\Documents\Bot\bin\Debug\UserPassivesStorage.json";
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
        public string Rarity { get; set; }
        public string UnitLeaderName { get; set; }
        public int UnitHP { get; set; }
        public int UnitATK { get; set; }
        public int UnitDEF { get; set; }
        public int UnitLeaderSkill { get; set; }
        public int UnitPassiveATK { get; set; }
        public int UnitPassiveDEF { get; set; }
        public int DMGReduction { get; set; }
        public int Support { get; set; }
        public string Links { get; set; }
    }
}
