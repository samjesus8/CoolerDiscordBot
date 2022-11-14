using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace DiscordBotTest.InternalBuilders
{
    internal class DokkanLinks : LinksStorage
    {
        public string LinkName { get; set; }
        public double ATK { get; set; }
        public double DEF { get; set; }

        public string Error { get; set; }

        public DokkanLinks() 
        {

        }

        public DokkanLinks(string linkName, double atk, double def)
        {
            LinkName = linkName;
            ATK = atk;
            DEF = def;
        }

        public bool LoadLinks() 
        {
            try 
            {
                using (StreamReader sr = new StreamReader("Links.json"))
                {
                    string json = sr.ReadToEnd();
                    JSONObjectLinks obj = JsonConvert.DeserializeObject<JSONObjectLinks>(json);

                    for (int i = 0; i < obj.members.Length; i++)
                    {
                        Links.Add(obj.members[i].LinkName, new Tuple(obj.members[i].ATK, obj.members[i].DEF));
                    }
                    return true;
                }
            }
            catch(Exception ex) 
            {
                Error = ex.ToString();
                return false;
            }

        }

        public bool StoreLinks(DokkanLinks LinkObj) 
        {
            Links.Clear(); //This should only be written to when its loading from the JSON File not writing to
            Error = null;
            try 
            {
                //var path = @"C:\Users\samue\Documents\Bot\bin\Debug\Links.json";
                var path = @"D:\Visual Studio Projects\DiscordBotTest\bin\Debug\Links.json";
                var json = File.ReadAllText(path);

                var jsonObj = JObject.Parse(json);
                var members = jsonObj["members"].ToObject<List<DokkanLinks>>();

                members.Add(LinkObj);

                jsonObj["members"] = JArray.FromObject(members);

                File.WriteAllText(path, jsonObj.ToString());
                return true;
            }
            catch (Exception ex)
            {
                Error = ex.ToString();
                Console.WriteLine(ex);
                return false;
            }

        }
    }

    public class JSONObjectLinks 
    {
        public string LinksCategory { get; set; }
        public Members[] members { get; set; }
    }

    public class Members 
    {
        public string LinkName { get; set; }
        public double ATK { get; set; }
        public double DEF { get; set; }
    }
}
