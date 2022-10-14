using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotTest.Builders
{
    internal class DokkanPassiveCalculator
    {
        public string Name { get; set; }
        public long HP { get; set; }
        public long ATK { get; set; }
        public long DEF { get; set; }
        public long LeaderSkill { get; set; }
        public long PassiveATK { get; set; }
        public long PassiveDEF { get; set; }

        public DokkanPassiveCalculator(string name, long hP, long aTK, long dEF, long leaderSkill, long passiveATK, long passiveDEF)
        {

        }
    }
}
