using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DungeonExplorer
{
    public class PointOfInterest
    {
        // Properties for the PointOfInterest
        public string Name { get; set; }  // Name of the point of interest (e.g., Desk, Bookshelf)
        public string Description { get; set; }  // Description of the point of interest
        public List<Item> Items { get; set; }  // List of items associated with this point of interest

        // Constructor to initialize the PointOfInterest
        public PointOfInterest(string name, string description, List<Item> items = null)
        {
            Name = name;
            Description = description;
            Items = items ?? new List<Item>();  // If no items are provided, initialize an empty list
        }

        // Method to add an item to this point of interest
        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        // Method to remove an item from this point of interest
        public void RemoveItem(Item item)
        {
            Items.Remove(item);
        }
    }

}
