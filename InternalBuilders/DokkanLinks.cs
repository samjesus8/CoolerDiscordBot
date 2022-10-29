using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotTest.InternalBuilders
{
    internal class DokkanLinks
    {
        public Dictionary<string, Tuple> Links = new Dictionary<string, Tuple>();

        public DokkanLinks() 
        {
            Links.Add("Link1", new Tuple(1, 2));
        }

        public void StoreLinks() 
        {

        }
    }

    public class Tuple 
    {
        public int ATK { get; set; }
        public int DEF { get; set; }
        public Tuple(int aTK, int dEF)
        {
            ATK = aTK;
            DEF = dEF;
        }
    }
}
