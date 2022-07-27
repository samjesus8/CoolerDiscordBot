using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotTest
{
    public class NumberGenerator
    {
        public int[] randomNumbers = new int[5];
        public int[] setNumbers = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20
                                    , 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40
                                        , 41, 42, 43, 44, 45, 46, 47, 48, 49, 50};
        public string[] result;

        public NumberGenerator() 
        {
            var rand = new Random();
            for (int i = 0; i < 5; i++)
            {
                randomNumbers[i] = rand.Next(setNumbers.Length);
                if (i == 5) { break; }
            }
            result = randomNumbers.Select(x => x.ToString()).ToArray();
        }
    }
}
