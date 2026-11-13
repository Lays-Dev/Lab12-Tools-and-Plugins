using UnityEngine;

// Represents one item in the inventory.
[System.Serializable]
public class InventoryItem
{
    // Unique ID assigned to the item.
    public int ID;

    // Name of the item.
    public string Name;

    // Value or price of the item.
    public int Value;

    // Constructor used to create new inventory items.
    public InventoryItem(int id, string name, int value)
    {
        ID = id;
        Name = name;
        Value = value;
    }
}
