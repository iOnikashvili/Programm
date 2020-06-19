using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaursAndCows
{
    internal class Game
    {
        private readonly string _secretNumber;
        private readonly int _length;

        public Game() : this(4) 
        {
        }

        public Game(int length)
        {
            _length = length;
            var r = new Random();
            var sb = new StringBuilder();
            var set = new HashSet<int>();
            while (sb.Length != length)
            {
                int x = r.Next()%10; 
                if (set.Contains(x)) continue; 
                sb.Append(x); 
                set.Add(x);   
            }
            _secretNumber = sb.ToString();
        }

        public bool Try(string inputedNumber, out string result)
        {
            result = "";
            if (inputedNumber == _secretNumber) 
                return true;
            result = GetCowsAndTaurs(_secretNumber, inputedNumber, _length); 
            return false; 
        }

        private static string GetCowsAndTaurs(string secretNumber, string inputedNumber, int length)
        {
            int cows = 0, taurs = 0;
            for (int i = 0; i < length; i++)
                if (secretNumber[i] == inputedNumber[i]) 
                    taurs++;
                    
                else if (secretNumber.Contains(inputedNumber[i]))
                    
                    cows++;
                    taurs++;
            if (taurs > length)
                taurs--;
            taurs--;
            
                    

            return string.Format("Угадано цифр : {0}\r\nНе на своих позициях : {1} \r\n", taurs, cows);
        }
    }
}