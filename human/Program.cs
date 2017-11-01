using System;

namespace human
{
    
    public class Human
    {
        public string name{get; set;}
        public int strength = 3;
        public int intelligence = 3;
        public int dexterity = 3;
        public int health = 100;   

        // #below new instance(occurance) set which takes defaults, yet sets the name
        public Human (string nm)
        {
            name = nm;
        }
        // public string GetName()
        // {
        //     return name;
        // }
        // below new custom class set where all variables can be set
           public Human(string myname, int mystrength, int myintellegence, int mydexterity, int myhealth)
        {
           name = myname;
           strength = mystrength;
           intelligence = myintellegence;
           dexterity = mydexterity;
           health = myhealth;
        }
        public void Attack(Human batman)
        {
            batman.health = batman.health-(strength*5);
        }
        
    }

    
    public class Program    
    {
        public static void Main(string[] args)
        {
        Human myHuman = new Human("jordan");
        Human myHuman2 = new Human("jaren");
        Console.WriteLine(myHuman.name);
        myHuman.Attack(myHuman2);
        // attacked once
        
        }
           
       
    }
    
}
