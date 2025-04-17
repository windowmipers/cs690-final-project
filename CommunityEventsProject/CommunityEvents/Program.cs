using System;
using System.IO;
using System.Collections.Generic;

namespace CommunityEvents;
 

class Program
{
    static void Main(string[] args)
    {
        string command = "none";
        do {
            Console.WriteLine("Events: ");
            string eventNames = File.ReadAllText("event-names.txt");
            Console.WriteLine(eventNames);
            Console.WriteLine("Enter {new OR exit OR [the event name]}");
            command = Console.ReadLine();
            if (command=="new") {
                Console.WriteLine("Enter event name: ");
                string newName = Console.ReadLine();
                File.AppendAllText("event-names.txt",newName+Environment.NewLine);
            }
            string[] eventList = File.ReadAllLines("event-names.txt");
            foreach (string Event in eventList) {
                if (command==Event) {
                    EventView(Event);
                    break;
                
                }
            }
        } while (command != "exit");
    }
    public static void EventView(string eventName) {
        string[] lines = File.ReadAllLines("event-data.csv");
        List<string> volunteers = new List<string>();
        List<string> items = new List<string>();
        foreach (string line in lines) {
            string[] elements = line.Split(',');
            if (elements[0]==eventName) {
                if (elements[1]=="Volunteer") {
                    volunteers.Add(elements[2]);
                } else if (elements[1]=="Type") {
                    items.Add(elements[2]);
                }
            }
        }
        Console.WriteLine(eventName + " Information:");
        Console.WriteLine("Volunteers:");
        foreach(string volunteer in volunteers) {
            Console.WriteLine(volunteer);
        }
        Console.WriteLine("Items:");
        foreach(string item in items) {
            Console.WriteLine(item);
        }
    }
}