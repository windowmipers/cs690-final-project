namespace CommunityEvents;

public class ConsoleUI {
    public void Show() {
        string command = "none";
        do {
            Console.WriteLine("Events: ");
            string eventNames = File.ReadAllText("event-names.txt");
            Console.WriteLine(eventNames);
            command = AskForInput("Enter {new OR delete OR exit OR [the event name]}: ");
            if (command=="new") {
                string newName = AskForInput("Enter event name: ");
                File.AppendAllText("event-names.txt",newName+Environment.NewLine);
            } else if (command=="delete") {
                string eventToDelete = AskForInput("Enter event name: ");
                string[] lines = File.ReadAllLines("event-names.txt");
                File.Create("event-names.txt").Close();
                foreach (string line in lines) {
                    string[] elements = line.Split(',');
                    if (elements[0]!=eventToDelete) {
                        File.AppendAllText("event-data.csv",line+Environment.NewLine);
                    }
                }
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
        List<(string,string)> volunteers = new List<(string,string)>();
        List<(string,string)> items = new List<(string,string)>();
        foreach (string line in lines) {
            string[] elements = line.Split(',');
            if (elements[0]==eventName) {
                if (elements[1]=="Volunteer") {
                    volunteers.Add((elements[2],elements[3]));
                } else if (elements[1]=="Item") {
                    items.Add((elements[2],elements[3]));
                }
            }
        }
        Console.WriteLine(eventName + " Information:");
        Console.WriteLine("Volunteers: ");
        foreach((string,string) volunteer in volunteers) {
            Console.WriteLine(volunteer);
        }
        Console.WriteLine("Items:");
        foreach((string,string) item in items) {
            Console.WriteLine(item);
        }
    }
    public static void EventSelect(string eventName) {
        string eventCommand = "none";
        string volunteerName = "none";
        string itemName = "none";
        string newStatus = "none";
        do {
            EventView(eventName);
            eventCommand = AskForInput("Enter {add volunteer OR add item OR delete volunteer OR delete item OR update volunteer role OR change item status OR exit}: ");
            if (eventCommand=="add volunteer") {
                volunteerName = AskForInput("Enter volunteer name: ");
                AddVolunteer(eventName,volunteerName);
            } else if (eventCommand=="add item") {
                itemName = AskForInput("Enter item name: ");
                AddItem(eventName,itemName);
            } else if (eventCommand=="delete volunteer") {
                volunteerName = AskForInput("Enter volunteer name: ");
                DeleteEntity(eventName,volunteerName);
            } else if (eventCommand=="delete item") {
                itemName = AskForInput("Enter item name: ");
                DeleteEntity(eventName,itemName);
            } else if (eventCommand=="update volunteer role") {
                volunteerName = AskForInput("Enter volunteer name: ");
                newStatus = AskForInput("Enter assigned role: ");
                Update(eventName,"Volunteer",volunteerName,newStatus);
            } else if (eventCommand=="change item status") {
                itemName = AskForInput("Enter item name: ");
                newStatus = AskForInput("Has the item been acquired? {Y OR N} ");
                if (newStatus=="Y") {
                    newStatus = "Acquired";
                } else if (newStatus=="N")
                    newStatus = "Not Acquired";
                Update(eventName,"Item",itemName,newStatus);
            }
                
        } while (eventCommand != "exit");
    }
    public static string AskForInput(string message) {
        Console.Write(message);
        return Console.ReadLine();
    }
    public static void AddVolunteer(string eventName,string volunteerName) {
        string volunteerData = eventName + ',' + "Volunteer" + ',' + volunteerName + ',' + "No Role";
        File.AppendAllText("event-data.csv",volunteerData+Environment.NewLine);
    }
    public static void AddItem(string eventName,string itemName) {
        string itemData = eventName + ',' + "Item" + ',' + itemName + ',' + "Not Acquired";
        File.AppendAllText("event-data.csv",itemData+Environment.NewLine);
    }
    public static void DeleteEntity(string eventName,string entityName) {
        string[] lines = File.ReadAllLines("event-data.csv");
        File.Create("event-data.csv").Close();
        foreach (string line in lines) {
            string[] elements = line.Split(',');
            if (elements[0]!=eventName || elements[2]!=entityName) {
                File.AppendAllText("event-data.csv",line+Environment.NewLine);
            }
        }
    }
    public static void Update(string eventName,string entityType,string entityName,string status) {
        DeleteEntity(eventName,entityName);
        string newData = eventName + ',' + entityType + ',' + entityName + ',' + status;
        File.AppendAllText("event-data.csv",newData+Environment.NewLine);
    }
}