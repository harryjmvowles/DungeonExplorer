using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml.Linq;


namespace DungeonExplorer
{
    // Creates Player Class
    public class Player : Creature
    {
        private int _potions = 0;
        private int _keys = 0;
        private int _level = 1;
        private int _experience = 0;
        private int _experienceToNextLevel = 100;
        private List<string> _inventory;
        public Room CurrentRoom { get; set; }

        // Player-specific properties and getter/setter
        public int Potions { get => _potions; private set => _potions = value; }
        public int Keys { get => _keys; private set => _keys = value; }
        public int Level { get => _level; private set => _level = value; }
        public int Experience { get => _experience; private set => _experience = value; }
        public int ExperienceToNextLevel { get => _experienceToNextLevel; private set => _experienceToNextLevel = value; }
        public List<string> Inventory { get => _inventory; private set => _inventory = value; }

        // Constructor for Player
        public Player(string name, int health = 100, int defense = 5, int armorValue = 5, int weaponValue = 5)
            : base(name, health, defense, armorValue, weaponValue)
        {
            _inventory = new List<string>();
        }
    
    


        //Method to view player inventory
        public void ViewInventory()
        {
            Console.WriteLine($"Name: {Name}\nHealth: {Health}\nPotions: {_potions}\nLevel: {_level}\nKeys: {_keys}");
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
                Console.WriteLine($"You have added {amount} potion(s) to your bag. You now have {_potions} potion(s).");
            }
            else
            {
                Console.WriteLine("Invalid potion amount.");
            }
        }

        //Method to use a key
        public void UseKey()
        {
            if (Keys > 0)
            {
                Keys--;
                Console.WriteLine("You have used a key.");
            }
            else
            {
                Console.WriteLine("You have no keys left.");
            }
        }

        //Method to use a potion
        public void UsePotion()
        {
            if (Potions > 0)
            {
                Potions--;
                Health += 25;
                Console.WriteLine("You have used a potion. Your health has increased by 25.");
            }
            else
            {
                Console.WriteLine("You have no potions left.");
            }
        }
    }
}

