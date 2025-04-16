namespace CommunityEvents;
 
using System;
using System.IO;
using System.Collections.Generic;

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
                EventView(event);
                break;
                
            }
        }
    }
    public EventView(string eventName) {
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
        Console.WriteLine(eventName + "Information:");
        Console.WriteLine("Volunteers:");
        foreach(string volunteer in volunteers) {
            Console.WriteLine(volunteer);
        }
        Console.WriteLine("Items:");
        foreach(string item in items) {
            Console.WriteLine(item);
    }
}
