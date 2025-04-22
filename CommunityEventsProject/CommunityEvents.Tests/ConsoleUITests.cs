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
        string volunteerLine = eventName + ',' + "Volunteer" + ',' + volunteerName + ',' + "No Role\n";
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
        string itemLine = eventName + ',' + "Item" + ',' + itemName + ',' + "Not Acquired\n";
        var contentFromFile = File.ReadAllText("event-data.csv");
        Assert.Equal(itemLine,contentFromFile);
    }
    [Fact]
    public void TestDeleteEntity()
    {
        File.Create("event-data.csv").Close();
        string eventName = "Trash Cleanup";
        string itemName = "Garbage Bags";
        ConsoleUI.AddItem(eventName,itemName);
        ConsoleUI.DeleteEntity(eventName,itemName);
        var contentFromFile = File.ReadAllText("event-data.csv");
        Assert.Equal("",contentFromFile);
    }
    [Fact]
    public void TestUpdate()
    {
        File.Create("event-data.csv").Close();
        string eventName = "Trash Cleanup";
        string itemName = "Garbage Bags";
        ConsoleUI.AddItem(eventName,itemName);
        ConsoleUI.Update(eventName,"Item",itemName,"Acquired");
        string itemLine = eventName + ',' + "Item" + ',' + itemName + ',' + "Acquired\n";
        var contentFromFile = File.ReadAllText("event-data.csv");
        Assert.Equal(itemLine,contentFromFile);
    }
}