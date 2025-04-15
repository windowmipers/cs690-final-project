namespace CommunityEvents;
 
using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string command = "none";
        Console.WriteLine("Events: ");
        string eventNames = File.ReadAllText("event-names.txt");
        Console.WriteLine(eventNames);
        Console.WriteLine("Enter {new OR exit or [the event name]}");
        command = Console.ReadLine();
        if (command=="new") {
            Console.WriteLine("Enter event name: ");
            string newName = Console.ReadLine();
                File.AppendAllText("event-names.txt",newName+Environment.NewLine);
        }
        string[] eventList = File.ReadAllLines("event-names.txt");
        foreach (string event in eventList) {
            if (command==event) {
                
            }
        }
    }
    public EventView() {
        string[] lines = File.ReadAllLines("event-data.csv");
        foreach (string line in lines) {
            string[] elements = line.Split(',');
        }
    }
}
