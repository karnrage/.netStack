using System;
using System.Collections.Generic; // needed to use lists


namespace deck
{
    public class Program
    {       
       public string stringVal;
       public string suit;
       public int val;

       public card(string name, string division, int amount)

       {
           stringVal = name;
           suit = division;
           val = amount;

       }
       
       
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
