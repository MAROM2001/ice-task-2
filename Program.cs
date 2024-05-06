using System;
using System.Collections.Generic;

public class InventoryItem
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Category { get; set; }
}

public class Inventory
{
    private Dictionary<string, List<InventoryItem>> inventory;

    public Inventory()
    {
        inventory = new Dictionary<string, List<InventoryItem>>();
    }

    public void AddItem(InventoryItem item)
    {
        if (!inventory.ContainsKey(item.Category))
        {
            inventory[item.Category] = new List<InventoryItem>();
        }

        inventory[item.Category].Add(item);
    }

    public void RemoveItem(InventoryItem item)
    {
        if (inventory.ContainsKey(item.Category))
        {
            inventory[item.Category].Remove(item);
        }
    }

    public void DisplayInventory()
    {
        foreach (var category in inventory)
        {
            Console.WriteLine($"Category: {category.Key}");
            foreach (var item in category.Value)
            {
                Console.WriteLine($"Name: {item.Name}, Price: {item.Price}, Quantity: {item.Quantity}");
            }
            Console.WriteLine();
        }
    }
}

public class GroceryStore
{
    public Inventory Inventory { get; }

    public GroceryStore()
    {
        Inventory = new Inventory();
    }
}

public static class InputValidator
{
    public static bool ValidateItem(InventoryItem item)
    {
        if (string.IsNullOrWhiteSpace(item.Name) || item.Price <= 0 || item.Quantity <= 0 || string.IsNullOrWhiteSpace(item.Category))
        {
            return false;
        }
        return true;
    }
}

public static class ErrorHandler
{
    public static void HandleError(Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {
            GroceryStore groceryStore = new GroceryStore();

            InventoryItem item1 = new InventoryItem { Name = "Apple", Price = 1.99m, Quantity = 100, Category = "Fruits" };
            InventoryItem item2 = new InventoryItem { Name = "Broccoli", Price = 0.99m, Quantity = 50, Category = "Vegetables" };

            if (InputValidator.ValidateItem(item1))
            {
                groceryStore.Inventory.AddItem(item1);
            }
            else
            {
                Console.WriteLine("Invalid item.");
            }

            if (InputValidator.ValidateItem(item2))
            {
                groceryStore.Inventory.AddItem(item2);
            }
            else
            {
                Console.WriteLine("Invalid item.");
            }

            groceryStore.Inventory.DisplayInventory();
        }
        catch (Exception ex)
        {
            ErrorHandler.HandleError(ex);
        }
    }
}
