using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DiscordBotTest
{
    internal class DokkanCardBuilder
    {
        public string Name { get; set; }
        public string Passive { get; set; }
        public string ImageURL { get; set; }

        public DokkanCardBuilder()
        {
            try
            {
                using (StreamReader r = new StreamReader("LRPassives.json"))
                {
                    string json = r.ReadToEnd();
                    JSONObject obj = JsonConvert.DeserializeObject<JSONObject>(json);

                    Random rand = new Random();
                    var data = obj.members[rand.Next(0, obj.members.Length)];

                    Name = data.name;
                    Passive = data.passive;
                    ImageURL = data.imageURL;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
    class JSONObject
    {
        public string categoryName { get; set; }
        public Member[] members { get; set; }
    }

    class Member
    {
        public string name { get; set; }
        public string passive { get; set; }
        public string imageURL { get; set; }
    }
}
