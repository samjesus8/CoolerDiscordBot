using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotTest
{
    internal class CardGenerator
    {
        public int[] genNo = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
        public string[] genSuit = { "Clubs", "Diamonds", "Spades", "Hearts" }; //Arrays for the cards
        public string cardHit { get; internal set; }
        public int GenNoRes { get; internal set; }
        public CardGenerator()
        {
            var rand = new Random(); //Random Generator
            int hitIndex = rand.Next(0, this.genNo.Length - 1);
            int hitSuitIndex = rand.Next(0, this.genSuit.Length - 1); //Index is 1 less than array length
            this.GenNoRes = this.genNo.ElementAt(hitIndex);
            this.cardHit = this.genNo.ElementAt(hitIndex) + " of " + this.genSuit.ElementAt(hitSuitIndex); //The actual card     
        }
    }
}
