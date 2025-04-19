namespace CommunityEvents.Tests;

using CommunityEvents;

public class ConsoleUITests
{
    ConsoleUI consoleUI = new ConsoleUI();
    [Fact]
    public void TestAddVolunteer()
    {
        File.Create("event-data.csv").Close();
        string eventName = "Trash Cleanup";
        string volunteerName = "John Smith";
        ConsoleUI.AddVolunteer(eventName, volunteerName);
        string volunteerLine = eventName + ',' + "Volunteer" + ',' + volunteerName + ',' + "None\n";
        var contentFromFile = File.ReadAllText("event-data.csv");
        Assert.Equal(volunteerLine,contentFromFile);
    }
    [Fact]
    public void TestAddItem()
    {
        File.Create("event-data.csv").Close();
        string eventName = "Trash Cleanup";
        string itemName = "Garbage Bags";
        ConsoleUI.AddItem(eventName, itemName);
        string itemLine = eventName + ',' + "Item" + ',' + itemName + ',' + "No\n";
        var contentFromFile = File.ReadAllText("event-data.csv");
        Assert.Equal(itemLine,contentFromFile);
    }
}