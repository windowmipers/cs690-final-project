namespace CommunityEvents;

using Spectre.Console;

public class ConsoleUI {
    public void Show() {
        string command = "none";
        string selectedEvent = "none";
        do {
            Console.WriteLine("Events: ");
            string eventNames = File.ReadAllText("event-names.txt");
            string[] eventList = File.ReadAllLines("event-names.txt");
            Console.WriteLine(eventNames);
            command = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select an option: ")
                    .AddChoices(new[] {
                        "New Event", "Delete Event", "Select Event", "Exit", 
                    }));
            if (command=="New Event") {
                string newName = AskForInput("Enter event name: ");
                File.AppendAllText("event-names.txt",newName+Environment.NewLine);
            } else if (command=="Delete Event") {
                string eventToDelete = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Select an option: ")
                        .AddChoices(
                            eventList
                        ));
                string[] lines = File.ReadAllLines("event-names.txt");
                File.Create("event-names.txt").Close();
                foreach (string line in lines) {
                    string[] elements = line.Split(',');
                    if (elements[0]!=eventToDelete) {
                        File.AppendAllText("event-names.txt",line+Environment.NewLine);
                    }
                }
            } else if (command=="Select Event") {
                selectedEvent = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Select an option: ")
                        .AddChoices(
                            eventList
                        ));
            
                foreach (string Event in eventList) {
                    if (selectedEvent==Event) {
                        EventSelect(Event);
                        break;
                    }
                }
            }
        } while (command != "Exit");
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
            string[] lines = File.ReadAllLines("event-data.csv");
            List<string> volunteerNames = new List<string>();
            List<string> itemNames = new List<string>();
            foreach (string line in lines) {
                string[] elements = line.Split(',');
                if (elements[0]==eventName) {
                    if (elements[1]=="Volunteer") {
                        volunteerNames.Add(elements[2]);
                    } else if (elements[1]=="Item") {
                        itemNames.Add(elements[2]);
                    }
                }
            }
            eventCommand = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select an option: ")
                    .AddChoices(new[] {
                        "Add Volunteer", "Add Item", "Delete Volunteer", "Delete Item", "Update Volunteer Role", "Change Item Status", "Exit",
                    }));
            if (eventCommand=="Add Volunteer") {
                volunteerName = AskForInput("Enter volunteer name: ");
                AddVolunteer(eventName,volunteerName);
            } else if (eventCommand=="Add Item") {
                itemName = AskForInput("Enter item name: ");
                AddItem(eventName,itemName);
            } else if (eventCommand=="Delete Volunteer") {
                if (volunteerNames.Count > 0) {
                    volunteerName = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Select a volunteer: ")
                            .AddChoices(
                                volunteerNames
                            ));
                    DeleteEntity(eventName,volunteerName);
                } else {
                    Console.WriteLine("No volunteers to select from");
                }
            } else if (eventCommand=="Delete Item") {
                if (itemNames.Count > 0) {
                    itemName = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Select an item: ")
                        .AddChoices(
                            itemNames
                        ));
                } else {
                    Console.WriteLine("No items to select from");
                }
                DeleteEntity(eventName,itemName);
            } else if (eventCommand=="Update Volunteer Role") {
                if (volunteerNames.Count > 0) {
                    volunteerName = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Select a volunteer: ")
                            .AddChoices(
                                volunteerNames
                            ));
                    newStatus = AskForInput("Enter assigned role: ");
                    Update(eventName,"Volunteer",volunteerName,newStatus);
                } else {
                    Console.WriteLine("No volunteers to select from");
                }
            } else if (eventCommand=="Change Item Status") {
                if (itemNames.Count > 0) {
                    itemName = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Select an option: ")
                            .AddChoices(
                                itemNames
                            ));
                    newStatus = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Has the item been acquired?")
                            .AddChoices(new[] {
                                "Yes", "No",
                            }));
                    if (newStatus=="Yes") {
                        newStatus = "Acquired";
                    } else if (newStatus=="No")
                        newStatus = "Not Acquired";
                    Update(eventName,"Item",itemName,newStatus);
                } else {
                    Console.WriteLine("No items to select from");
                }
            }
        } while (eventCommand != "Exit");
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