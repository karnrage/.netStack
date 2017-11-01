using System;

namespace FundamentalsI
{
    class Program
    {
        static void Main(string[] args)
        {
            int x;  
            for(x = 1; x < 256; x++)
            {
                Console.WriteLine(x);
            }

              
            for(int y = 1; y < 101; y++)
            {
                if ((y % 3 == 0) && (y % 5 ==0)
                Console.WriteLine(y);
            }
        }
        
    }
}
      
            for (int i = 1; i &lt; 101; i++)
            {
                if ((i % 3 == 0) &amp;&amp; (i % 5 == 0))
                    fbString += "FizzBuzz" + Environment.NewLine;
                else if (i % 3 == 0)
                    fbString += "Fizz" + Environment.NewLine;
                else if (i % 5 == 0)
                    fbString += "Buzz" + Environment.NewLine;
                else
                    fbString += i.ToString() + Environment.NewLine;
            }
            return fbString;
