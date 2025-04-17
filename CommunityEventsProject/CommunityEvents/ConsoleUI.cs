namespace CommunityEvents;

public class ConsoleUI {
    public void Show() {
        string command = "none";
        do {
            Console.WriteLine("Events: ");
            string eventNames = File.ReadAllText("event-names.txt");
            Console.WriteLine(eventNames);
            command = AskForInput("Enter {new OR exit OR [the event name]}: ");
            if (command=="new") {
                string newName = AskForInput("Enter event name: ");
                File.AppendAllText("event-names.txt",newName+Environment.NewLine);
            }
            string[] eventList = File.ReadAllLines("event-names.txt");
            foreach (string Event in eventList) {
                if (command==Event) {
                    EventSelect(Event);
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
                } else if (elements[1]=="Item") {
                    items.Add(elements[2]);
                }
            }
        }
        Console.WriteLine(eventName + " Information:");
        Console.WriteLine("Volunteers: ");
        foreach(string volunteer in volunteers) {
            Console.WriteLine(volunteer);
        }
        Console.WriteLine("Items:");
        foreach(string item in items) {
            Console.WriteLine(item);
        }
    }
    public static void EventSelect(string eventName) {
        string eventCommand = "none";
        string volunteerName = "none";
        string volunteerData = "none";
        string itemName = "none";
        string itemData = "none";
        do {
            EventView(eventName);
            eventCommand = AskForInput("Enter {add volunteer OR add item OR exit}: ");
            if (eventCommand=="add volunteer") {
                volunteerName = AskForInput("Enter volunteer name: ");
                volunteerData = eventName + ',' + "Volunteer" + ',' + volunteerName + ',' + "None";
                File.AppendAllText("event-data.csv",volunteerData+Environment.NewLine);
            } else if (eventCommand=="add item") {
                itemName = AskForInput("Enter item name: ");
                itemData = eventName + ',' + "Item" + ',' + itemName + ',' + "No";
                File.AppendAllText("event-data.csv",itemData+Environment.NewLine);
            }
        } while (eventCommand != "exit");
    }
    public static string AskForInput(string message) {
        Console.Write(message);
        return Console.ReadLine();
    }
}