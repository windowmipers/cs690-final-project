namespace CommunityEvents;
 
using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Select view {events OR volunteers}: ");
        string mode = Console.ReadLine();
        string command = "none";
        if (mode=="volunteers") {
            do {
                Console.WriteLine("Volunteer List: ");
                string volunteerList = File.ReadAllText("volunteer-data.txt");
                Console.WriteLine(volunteerList);
                Console.WriteLine("Select {new OR main menu}");
                command = Console.ReadLine();
                if (command=="new") {
                    Console.WriteLine("Enter volunteer name: ");
                    string newName = Console.ReadLine();
                    File.AppendAllText("volunteer-data.txt",newName+Environment.NewLine);
                }
            } while (command != "main menu");
        }
    }
}
