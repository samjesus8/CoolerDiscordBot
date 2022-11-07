using System.Collections.Generic;

namespace DiscordBotTest.InternalBuilders
{
    internal class LinksStorage
    {
        public Dictionary<string, Tuple> Links = new Dictionary<string, Tuple>();
    }

    public class Tuple
    {
        public double ATK { get; set; }
        public double DEF { get; set; }
        public Tuple(double aTK, double dEF)
        {
            ATK = aTK;
            DEF = dEF;
        }
    }
}
