using System;
using System.Collections.Generic;
using System.Linq;
using JsonData;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Collections to work with
            List<Artist> Artists = JsonToFile<Artist>.ReadJson();
            List<Group> Groups = JsonToFile<Group>.ReadJson();

            //========================================================
            //Solve all of the prompts below using various LINQ queries
            //========================================================

            //There is only one artist in this collection from Mount Vernon, what is their name and age?
            var oneArtist = from Artist in Artists
                            where Artist.Hometown == "Mount Vernon"
                            // select Artist
                            select new {Artist.ArtistName};

                            

            //         // Below attempted to write to console after succeeding in debugger. Not working
            //         // var theZoneArtist = zoneArtist;
                    System.Console.WriteLine($"{oneArtist}"); 
                            
            // //Who is the youngest artist in our collection of artists?
            var youngArtist =   from Artist in Artists
                                orderby Artist.Age
                                select Artist;

            // var theYoungArtist = youngArtist.First();
            // System.Console.WriteLine($"{theYoungArtist.ArtistName}");                           
                            
            // //Display all artists with 'William' somewhere in their real name
            var allWilliam = from Artist in Artists
                            where Artist.RealName.Contains("William")
                            select new {Artist.RealName};

                    // var theallWilliam = allWilliam.Take(2);       attempted this after seeing result had two entries, attempted to print the entries. better result below
            foreach(var will in allWilliam){
                System.Console.WriteLine($"{will.RealName}");
            }                     
            
            // //Display the 3 oldest artist from Atlanta
            var oldAtlanta = from Artist in Artists
                            orderby Artist.Age descending
                            where Artist.Hometown == "Atlanta"
                            select new {Artist.ArtistName};

                    // System.Console.WriteLine(oldAtlanta); 

            var theOldAtlanta =  oldAtlanta.Take(3);
            foreach(var artist in theOldAtlanta){
                System.Console.WriteLine($"{artist.ArtistName}");
            }

            // Display all groups with names less than 8 characters in length

            var groupnameless = from Group in Groups
                            where (Group.GroupName.Length < 8)
                            select new {Group.GroupName};

            System.Console.WriteLine("All group names shorter than 8 digits:");
            foreach(var group in groupnameless){
                System.Console.WriteLine($"{group.GroupName}");
            }                            

            //(Optional) Display the Group Name of all groups that have members that are not from New York City

            //(Optional) Display the artist names of all members of the group 'Wu-Tang Clan'
        }
    }
}
