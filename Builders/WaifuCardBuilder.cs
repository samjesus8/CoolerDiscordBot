using Newtonsoft.Json;
using System;
using System.IO;

namespace DiscordBotTest
{
    internal class WaifuCardBuilder
    {
        public string Name { get; set; }
        public string AnimeName { get; set; }
        public string ImageURL { get; set; }

        public WaifuCardBuilder() 
        {
            try
            {
                using (StreamReader sr = new StreamReader("WaifuCards.json"))
                {
                    string json = sr.ReadToEnd();
                    JSONObject1 obj = JsonConvert.DeserializeObject<JSONObject1>(json);

                    Random rand = new Random();
                    var data = obj.members[rand.Next(0, obj.members.Length)];

                    Name = data.name;
                    AnimeName = data.animename;
                    ImageURL = data.imageURL;
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
            }
        }
    }
    class JSONObject1
    {
        public string categoryName { get; set; }
        public AnimeMember[] members { get; set; }
    }

    class AnimeMember
    {
        public string name { get; set; }
        public string animename { get; set; }
        public string imageURL { get; set; }
    }
}
