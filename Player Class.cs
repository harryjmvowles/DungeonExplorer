using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DungeonExplorer
{
    //Creates Player Class
    public class Player
    {
        //Player Stats with default values
        private string _name;
        private int _health = 100;
        private int _attack = 10;
        private int _defense = 5;
        private int _potions = 0;
        private int _keys = 0;
        private int _level = 1;
        private int _experience = 0;
        private int _experienceToNextLevel = 100;
        private List<string> _inventory;

        //Getters and Setters
        public string Name { get => _name; private set => _name = value; }
        public int Health { get => _health; private set => _health = value; }
        public int Potions { get => _potions; private set => _potions = value; }
        public List<string> Inventory { get => _inventory; private set => _inventory = value; }

        //Constructor to create a player with a name and inventory
        public Player(string name)
        {
            _name = name;
            _inventory = new List<string>();
        }

        //Method to view player inventory
        public void ViewInventory()
        {
            Console.WriteLine($"Name: {Name}\nHealth: {_health}\nPotions: {_potions}\nLevel: {_level}\nKeys: {_keys}");
            Console.WriteLine("Inventory:");
            foreach (string item in _inventory)
            {
                Console.WriteLine("- " + item);
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        //Method to add an item to the player's inventory
        public void AddToInventory(string item)
        {
            if (item == "Potion")
            {
                _potions++;
            }
            else if (item == "Key")
            {
                _keys++;
            }
            else
            {
                _inventory.Add(item);
            }
        }

        //Add potion method
        public void AddPotion(int amount = 1)
        {
            if (amount > 0)
            {
                _potions += amount;
                Console.WriteLine($"You have added {amount} potion(s). You now have {_potions} potion(s).");
            }
            else
            {
                Console.WriteLine("Invalid potion amount.");
            }
        }
    }
}

