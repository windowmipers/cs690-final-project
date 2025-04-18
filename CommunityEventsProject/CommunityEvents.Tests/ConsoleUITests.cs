namespace CommunityEvents.Tests;

using CommunityEvents;

public class ConsoleUITests
{
    ConsoleUI consoleUI = new ConsoleUI();
    [Fact]
    public void TestAddVolunteer()
    {
        File.Create("event-data.csv").Close();
        consoleUI.AddVolunteer("Trash Cleanup", "John Smith");
        string volunteerLine = eventName + ',' + "Volunteer" + ',' + volunteerName + ',' + "None";
        var contentFromFile = File.ReadAllText("event-data.csv");
        Assert.Equal(volunteerLine,contentFromFile);
    }
    [Fact]
    public void TestAddItem()
    {
        File.Create("event-data.csv").Close();
        consoleUI.AddItem("Trash Cleanup", "Garbage Bags");
        string itemLine = eventName + ',' + "Item" + ',' + itemName + ',' + "No";
        var contentFromFile = File.ReadAllText("event-data.csv");
        Assert.Equal(itemLine,contentFromFile);
    }
}