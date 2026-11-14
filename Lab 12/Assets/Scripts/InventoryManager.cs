
using UnityEngine;
using System.Collections.Generic;

// using System Collections for acess to lists.

public class InventoryManager : MonoBehaviour
{
    // Stores all possible inventory items in an inventroy list.
    public List<InventoryItem> inventory = new List<InventoryItem>();

    // Name passed to the linear search. Change this in the Inspector.
    public string searchItemName = "Sword";

    // Array of items used to generate inventory.
    private string[] itemNames =
    {
        "Sword",
        "Sheild",
        "Axe",
        "Shovel",
        "Bow",
        "Magic Staff",
        "Health Potion",
        "Speed Potion",
        "Bread",
        "Honeycombs",
        "Herbs",
        "Armor"

    };

    void Start()
    {
        // Create the inventory before searching through it.
        InitializeInventory();

        // Linear Search.
        // InventoryItem is the type of object that will be stored. 
        // foundItem is the variable name.
        // LinearSearchByName calls the search method which is further down. 
        InventoryItem foundItem = LinearSearchByName(searchItemName);

        // Null check
        if (foundItem != null)
        {
            Debug.Log("Linear Search Found: " + foundItem.Name + " with ID: " + foundItem.ID + " and Value: " + foundItem.Value);
        }
        else
        {
            Debug.Log("Item not found: " + searchItemName);
        }

        // Sort inventory by ID before Binary Search.
        SortInventoryByID();

        // Binary Search.
        InventoryItem foundByID = BinarySearchByID(inventory[0].ID);

        if (foundByID != null)
        {
            Debug.Log("Binary Search Found: " + foundByID.Name + " with ID: " + foundByID.ID + " and Value: " + foundByID.Value);
        }
        else
        {
            Debug.Log("Item ID not found!");
        }

        // Quick sort by value after binary search, which needs ID order.
        QuickSortByValue();

        foreach (InventoryItem item in inventory)
        {
            Debug.Log("Quick Sort By Value: " + item.Name + " with ID: " + item.ID + " and Value: " + item.Value);
        }
    }

    // Creates inventory items with random IDs and values.
    void InitializeInventory()
    {
        // Start empty
        inventory.Clear();

        // A HashSet is a collection that stores unique integer values.
        // Every item has a unique ID
        // HashSet checks if an ID is in use.
        HashSet<int> usedIDs = new HashSet<int>();

        foreach (string itemName in itemNames)
        {
            int randomID;

            // Make sure every item has a unique ID.
            do
            {
                randomID = Random.Range(100, 1000);
            }
            while (usedIDs.Contains(randomID));

            usedIDs.Add(randomID);

            // Generate a random item value.
            int randomValue = Random.Range(10, 500);

            // Create and add the item.
            InventoryItem newItem =
                new InventoryItem(randomID, itemName, randomValue);

            inventory.Add(newItem);
        }

        Debug.Log("Inventory Initialized! Total Items: " + inventory.Count);
    }

    // Linear search by name.

    public InventoryItem LinearSearchByName(string itemName)
    {
        // Check every item in the inventory.
        foreach (InventoryItem item in inventory)
        {
            // Compare the items name with the requested name.
            if (item.Name == itemName)
            {
                // Return the item if it matches.
                return item;
            }
        }

        // Return null if no matching item exists.
        return null;
    }

    // Sort inventory by ID
    public void SortInventoryByID()
    {
        inventory.Sort((a, b) => a.ID.CompareTo(b.ID));

        Debug.Log("Inventory Sorted By ID!");
    }

    // Binary Search by ID

    public InventoryItem BinarySearchByID(int itemID)
    {
        int left = 0;
        int right = inventory.Count - 1;

        // Continue searching while there are items to check.
        while (left <= right)
        {
            // Find the middle position.
            int middle = (left + right) / 2;

            InventoryItem middleItem = inventory[middle];

            // Check whether the middle item is the one we want.
            if (middleItem.ID == itemID)
            {
                return middleItem;
            }

            // If the ID is too small, search the right half.
            else if (middleItem.ID < itemID)
            {
                left = middle + 1;
            }

            // If the ID is too large, search the left half.
            else
            {
                right = middle - 1;
            }
        }

        // Return null if the ID does not exist.
        return null;
    }

    // Quick sort by item value.
    // Entry point used by Start after the searches finish.
    public void QuickSortByValue()
    {
        QuickSortByValue(0, inventory.Count - 1);

        Debug.Log("Inventory Sorted By Value!");
    }

    // Recursively sort the range from first through last.
    void QuickSortByValue(int first, int last)
    {
        if (first < last)
        {
            // Place the pivot and sort each side of it.
            int pivot = PartitionByValue(first, last);

            QuickSortByValue(first, pivot - 1);
            QuickSortByValue(pivot + 1, last);
        }
    }

    // Partition the range so values smaller than the pivot come first.
    int PartitionByValue(int first, int last)
    {
        // The last item in the range is the pivot.
        int pivot = inventory[last].Value;
        int smaller = first - 1;

        for (int element = first; element < last; element++)
        {
            // Move items smaller than the pivot toward the front.
            if (inventory[element].Value < pivot)
            {
                smaller++;

                InventoryItem temporary = inventory[smaller];
                inventory[smaller] = inventory[element];
                inventory[element] = temporary;
            }
        }

        // Place the pivot just after the smaller items.
        InventoryItem temporaryNext = inventory[smaller + 1];
        inventory[smaller + 1] = inventory[last];
        inventory[last] = temporaryNext;

        return smaller + 1;
    }
}
