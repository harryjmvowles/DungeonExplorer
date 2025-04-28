using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DungeonExplorer.Weapon;


namespace DungeonExplorer
{
    // Abstract Creature class, serves as base for Player and Enemy
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

    // Player class
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

        // Collect an item and add it to inventory
        public void CollectItem(Item item)
        {
            if (item is ICollectible collectible)
            {
                collectible.Collect(this);  // Call Collect method from ICollectible
                AddToInventory(item);  // Add the item to the inventory
            }
            else
            {
                Console.WriteLine("This item cannot be collected.");
            }
        }

        // Add item to inventory
        public void AddToInventory(Item item)
        {
            Inventory.Add(item);
            Console.WriteLine($"You picked up a {item.Name}!");
        }

        // Method to view player inventory with options
        public void ViewInventory()
        {
            while (true)  // Keep showing the inventory menu until the user chooses to continue
            {
                Console.Clear();
                Console.WriteLine($"Name: {Name}\nHealth: {Stats.Health}\nDamage: {Stats.WeaponValue}\nDefense: {Stats.ArmorValue}\nPotions: {_potions}\nKeys: {_keys}");
                Console.WriteLine("\nInventory:");
                foreach (Item item in _inventory)
                {
                    string equippedStatus = item.IsEquipped ? " (Equipped)" : "";
                    Console.WriteLine($"- {item.Name}{equippedStatus}");
                }

                // Display inventory management options
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("1. Equip item");
                Console.WriteLine("2. Unequip item");
                Console.WriteLine("3. Sort inventory");
                Console.WriteLine("4. Continue");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        EquipItem();
                        break;
                    case "2":
                        UnequipItem();
                        break;
                    case "3":
                        SortInventory();
                        break;
                    case "4":
                        return;  // Exit the loop and return to the game
                    default:
                        Console.WriteLine("Invalid option. Please choose a valid option.");
                        break;
                }
            }
        }

        // Method to equip an item from the inventory
        public void EquipItem()
        {
            Console.WriteLine("Enter the name of the item to equip:");
            string itemName = Console.ReadLine();
            var item = _inventory.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

            if (item != null && (item is Weapon || item is Armor))
            {
                // Check if the item is a weapon or armor and if something is already equipped
                if (item is Weapon)
                {
                    // If a weapon is already equipped, unequip it first
                    var currentWeapon = _inventory.FirstOrDefault(i => i is Weapon && i.IsEquipped);
                    if (currentWeapon != null)
                    {
                        Console.WriteLine($"Unequipping {currentWeapon.Name}...");
                        currentWeapon.IsEquipped = false;
                        GameManager.Instance.CurrentPlayer.Stats.WeaponValue -= ((Weapon)currentWeapon).AttackPower;
                    }

                    item.Use(this);  // Equip the new weapon
                }
                else if (item is Armor)
                {
                    // If armor is already equipped, unequip it first
                    var currentArmor = _inventory.FirstOrDefault(i => i is Armor && i.IsEquipped);
                    if (currentArmor != null)
                    {
                        Console.WriteLine($"Unequipping {currentArmor.Name}...");
                        currentArmor.IsEquipped = false;
                        GameManager.Instance.CurrentPlayer.Stats.ArmorValue -= ((Armor)currentArmor).ArmorValue;
                    }

                    item.Use(this);  // Equip the new armor
                }
            }
            else
            {
                Console.WriteLine($"Item '{itemName}' is not a valid weapon or armor, or it is not in your inventory.");
            }
        }

        // Method to unequip an item from the inventory
        public void UnequipItem()
        {
            Console.WriteLine("Enter the name of the item to unequip:");
            string itemName = Console.ReadLine();
            var item = _inventory.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

            if (item != null && (item is Weapon || item is Armor) && item.IsEquipped)
            {
                item.IsEquipped = false;  // Mark the item as unequipped
                if (item is Weapon weapon)
                {
                    GameManager.Instance.CurrentPlayer.Stats.WeaponValue -= weapon.AttackPower;
                }
                else if (item is Armor armor)
                {
                    GameManager.Instance.CurrentPlayer.Stats.ArmorValue -= armor.ArmorValue;
                }
                Console.WriteLine($"You have unequipped the {item.Name}.");
            }
            else
            {
                Console.WriteLine($"Item '{itemName}' is not equipped or it is not a valid weapon or armor.");
            }
        }

        public void SortInventory()
        {
            Console.Clear();
            Console.WriteLine("Choose an option to sort your inventory:");

            // List options for sorting
            Console.WriteLine("1. Sort Alphabetically");
            Console.WriteLine("2. Sort by Weapon Strength");
            Console.WriteLine("3. Sort by Armor Strength");
            Console.WriteLine("4. Filter by Weapons");
            Console.WriteLine("5. Filter by Armor");
            Console.WriteLine("6. Go Back");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    // Alphabetically sort the entire inventory by item name
                    _inventory.Sort((item1, item2) => item1.Name.CompareTo(item2.Name));
                    Console.WriteLine("Inventory sorted alphabetically.");
                    DisplayItems(); // Display the sorted items
                    break;

                case "2":
                    // Sort weapons by attack power (strongest first)
                    var weapons = _inventory.OfType<Weapon>()
                                             .OrderByDescending(w => w.AttackPower)
                                             .ToList();
                    Console.WriteLine("Weapons sorted by strength (strongest first).");
                    DisplayItemsWithWeaponValue(weapons.Cast<Item>().ToList());  // Convert to List<Item> before displaying
                    break;

                case "3":
                    // Sort armor by armor value (strongest first)
                    var armor = _inventory.OfType<Armor>()
                                          .OrderByDescending(a => a.ArmorValue)
                                          .ToList();
                    Console.WriteLine("Armor sorted by strength (strongest first).");
                    DisplayItemsWithArmorValue(armor.Cast<Item>().ToList());  // Convert to List<Item> before displaying
                    break;

                case "4":
                    // Filter and display only weapons
                    var filteredWeapons = _inventory.OfType<Weapon>().ToList();
                    Console.WriteLine("Filtered weapons:");
                    DisplayItemsWithWeaponValue(filteredWeapons.Cast<Item>().ToList());  // Convert to List<Item> before displaying
                    break;

                case "5":
                    // Filter and display only armor
                    var filteredArmor = _inventory.OfType<Armor>().ToList();
                    Console.WriteLine("Filtered armor:");
                    DisplayItemsWithArmorValue(filteredArmor.Cast<Item>().ToList());  // Convert to List<Item> before displaying
                    break;

                case "6":
                    return;  // Exit the inventory sorting menu

                default:
                    Console.WriteLine("Invalid option. Please choose a valid option.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        // Method to display items with WeaponValue for weapons
        public void DisplayItemsWithWeaponValue(List<Item> items)
        {
            foreach (var item in items)
            {
                if (item is Weapon weapon)
                {
                    Console.WriteLine($"- {weapon.Name} (Weapon Power: {weapon.AttackPower})");
                }
                else
                {
                    Console.WriteLine($"- {item.Name}");
                }
            }
        }

        // Method to display items with ArmorValue for armor
        public void DisplayItemsWithArmorValue(List<Item> items)
        {
            foreach (var item in items)
            {
                if (item is Armor armor)
                {
                    Console.WriteLine($"- {armor.Name} (Armor Value: {armor.ArmorValue})");
                }
                else
                {
                    Console.WriteLine($"- {item.Name}");
                }
            }
        }

        // Method to display all items
        public void DisplayItems(IEnumerable<Item> items = null)
        {
            if (items == null)
            {
                items = _inventory;  // If no list is passed, show the entire inventory
            }

            if (items.Count() == 0)
            {
                Console.WriteLine("No items to display.");
            }
            else
            {
                foreach (Item item in items)
                {
                    string equippedStatus = item.IsEquipped ? " (Equipped)" : "";
                    Console.WriteLine($"- {item.Name}{equippedStatus}");
                }
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

        // Method to take damage
        public void TakeDamage(int amount)
        {
            Stats.Health -= amount;
            if (Stats.Health < 0)
                Stats.Health = 0;
        }
    }
}

