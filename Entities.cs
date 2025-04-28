using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DungeonExplorer.Weapon;



// Creature Class
namespace DungeonExplorer
{
    public abstract class Creature
    {
        public string Name { get; protected set; }
        public Statistics Stats { get; protected set; } // Using Statistics for Creature's stats

        // Constructor for Creature Class (Player and Enemy)
        public Creature(string name, int health = 100, int armorValue = 5, int weaponValue = 5)
        {
            Name = name;
            Stats = new Statistics
            {
                Health = health,
                ArmorValue = armorValue,
                WeaponValue = weaponValue
            };
        }
    }

    // Enemy Class
    public class Enemy : Creature, IDamageable
    {
        public string Weapon { get; private set; }

        // Enemy-specific constructor with weapon, armor, stats, etc.
        public Enemy(string name, int health, string weapon, int armorValue, int weaponValue)
            : base(name, health, armorValue, weaponValue)
        {
            Weapon = weapon;
        }

        // Method to take damage
        public void TakeDamage(int amount)
        {
            Stats.Health -= amount;
            if (Stats.Health < 0)
                Stats.Health = 0;
        }
    }
    public class Player : Creature, IDamageable
    {
        private int _potions = 0;
        private int _keys = 0;
        private List<Item> _inventory;  // Inventory for items like weapons, armor
        public Room CurrentRoom { get; set; }

        // Player-specific properties and getter/setter
        public int Potions { get => _potions; private set => _potions = value; }
        public int Keys { get => _keys; private set => _keys = value; }
        public List<Item> Inventory { get => _inventory; private set => _inventory = value; }

        // Constructor for Player
        public Player(string name, int health = 100, int armorValue = 5, int weaponValue = 5)
            : base(name, health, armorValue, weaponValue)
        {
            _inventory = new List<Item>();
        }

        // Better AddToInventory method
        public void AddToInventory(Item item)
        {
            if (item is Weapon || item is Armor)
            {
                Inventory.Add(item);
                Console.WriteLine($"You picked up {item.Name}!");
            }
            else if (item is Potion)
            {
                AddPotion(1);  
            }
            else if (item is Key)
            {
                AddKey(1);
            }
            else
            {
                Console.WriteLine("Unknown item.");
            }
        }


        // Method to view player inventory (only weapons, armor)
        public void ViewInventory()
        {
            Console.WriteLine($"Name: {Name}\nHealth: {Stats.Health}\nDamage: {Stats.WeaponValue}\nDefense: {Stats.ArmorValue}\nPotions: {_potions}\nKeys: {_keys}");
            Console.WriteLine("Inventory:");
            foreach (Item item in _inventory)
            {
                string equippedStatus = item.IsEquipped ? " (Equipped)" : "";
                Console.WriteLine($"- {item.Name}{equippedStatus}");
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        // Method to use an item (e.g., Equip a weapon, armor, or consume potions)
        public void UseItem(string itemName)
        {
            var item = _inventory.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
            if (item != null)
            {
                item.Use(this); // Call the specific Use method for the item
            }
            else
            {
                Console.WriteLine($"Item {itemName} not found in inventory.");
            }
        }

        // Add potion method (store potions separately)
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

        // Method to add a key (store keys separately)
        public void AddKey(int amount = 1)
        {
            if (amount > 0)
            {
                _keys += amount;
                Console.WriteLine($"You have added {amount} key(s) to your bag. You now have {_keys} key(s).");
            }
            else
            {
                Console.WriteLine("Invalid key amount.");
            }
        }

        // Method to use a key (stored separately)
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

        // Method to use a potion
        public void UsePotion()
        {
            if (Potions > 0)
            {
                Potions--;
                Stats.Health += 25;
                Console.WriteLine("You have used a potion. Your health has increased by 25.");
            }
            else
            {
                Console.WriteLine("You have no potions left.");
            }
        }

        // Method to take damage
        public void TakeDamage(int amount)
        {
            Stats.Health -= amount;
            if (Stats.Health < 0)
                Stats.Health = 0;
        }
    }
}