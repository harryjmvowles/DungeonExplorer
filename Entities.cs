using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
        public string Weapon { get; protected set; }

        public Enemy(string name, int health, string weapon, int armorValue, int weaponValue)
            : base(name, health, armorValue, weaponValue)
        {
            Weapon = weapon;
        }

        public virtual void TakeDamage(int amount)
        {
            Stats.Health -= amount;
            if (Stats.Health < 0)
                Stats.Health = 0;
        }

        public virtual void PerformSpecialAbility()
        {
            // Base enemies have no special abilities
        }
    }

    // BaseMonster (for custom behaviors)
    public abstract class BaseMonster : Enemy
    {
        protected Random rand = new Random();

        public BaseMonster(string name, int health, string weapon, int armorValue, int weaponValue)
            : base(name, health, weapon, armorValue, weaponValue)
        {
        }

        public abstract override void PerformSpecialAbility();
    }

    // Goblin: Weak but debuf dmg
    public class GoblinMonster : BaseMonster
    {
        public GoblinMonster()
            : base("Goblin", 35, "Dagger", 2, 10) { }

        public override void PerformSpecialAbility()
        {
            Console.WriteLine("The Goblin throws dirt in your eyes! Your weapon damage drops slightly!");
            GameManager.Instance.CurrentPlayer.Stats.WeaponValue -= 2;
            if (GameManager.Instance.CurrentPlayer.Stats.WeaponValue < 5)
                GameManager.Instance.CurrentPlayer.Stats.WeaponValue = 5;
            Console.WriteLine($"Your weapon damage is now {GameManager.Instance.CurrentPlayer.Stats.WeaponValue}.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }

    // Troll: Regenerates health
    public class TrollMonster : BaseMonster
    {
        public TrollMonster()
            : base("Troll", 60, "Club", 3, 14) { }

        public override void PerformSpecialAbility()
        {
            Console.WriteLine("The Troll regenerates some health!");
            Stats.Health += 10;
            if (Stats.Health > 60) Stats.Health = 60;
            Console.WriteLine($"The Troll's health is now {Stats.Health}.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }

    // Demon: Reduces your armor temporarily
    public class DemonMonster : BaseMonster
    {
        public DemonMonster()
            : base("Demon", 50, "Fireball", 4, 16) { }

        public override void PerformSpecialAbility()
        {
            Console.WriteLine("The Demon curses you! Your armor weakens!");
            GameManager.Instance.CurrentPlayer.Stats.ArmorValue -= 2;
            if (GameManager.Instance.CurrentPlayer.Stats.ArmorValue < 0)
                GameManager.Instance.CurrentPlayer.Stats.ArmorValue = 0;
            Console.WriteLine($"Your armor value is now {GameManager.Instance.CurrentPlayer.Stats.ArmorValue}.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }

    // Skeleton: No special powers, just basic
    public class SkeletonMonster : BaseMonster
    {
        public SkeletonMonster()
            : base("Skeleton", 40, "Rusty Sword", 3, 12) { }

        public override void PerformSpecialAbility()
        {
            // Skeleton has no special ability
        }
    }


    // Player class
    public class Player : Creature, IDamageable
    {
        private int _potions = 0;
        private int _keys = 0;
        private List<Item> _inventory;  // Inventory for items like weapons, armor
        public Room CurrentRoom { get; set; }

        // Golden Key
        public bool HasGoldenKey { get; private set; } = false;

        // Backup base stats to restore after combat
        public int DefaultWeaponValue { get; private set; }
        public int DefaultArmorValue { get; private set; }

        // Player-specific properties and getter/setter
        public int Potions { get => _potions; private set => _potions = value; }
        public int Keys { get => _keys; private set => _keys = value; }
        public List<Item> Inventory { get => _inventory; private set => _inventory = value; }

        // Constructor for Player
        public Player(string name, int health = 100, int armorValue = 5, int weaponValue = 5)
            : base(name, health, armorValue, weaponValue)
        {
            _inventory = new List<Item>();
            DefaultWeaponValue = weaponValue;
            DefaultArmorValue = armorValue;
        }

        public void AddGoldenKey()
        {
            HasGoldenKey = true;
            Console.WriteLine("You now possess the Golden Key.");
            Inventory.Add(new GoldenKey());
        }

        public void CollectItem(Item item)
        {
            if (item is ICollectible collectible)
            {
                collectible.Collect(this);
                AddToInventory(item);
            }
            else
            {
                Console.WriteLine("This item cannot be collected.");
            }
        }

        public void AddToInventory(Item item)
        {
            Inventory.Add(item);
            Console.WriteLine($"You picked up a {item.Name}!");
        }

        public void ViewInventory()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Name: {Name}\nHealth: {Stats.Health}\nDamage: {Stats.WeaponValue}\nDefense: {Stats.ArmorValue}\nPotions: {_potions}\nKeys: {_keys}");
                Console.WriteLine("\nInventory:");
                foreach (Item item in _inventory)
                {
                    string equippedStatus = item.IsEquipped ? " (Equipped)" : "";
                    Console.WriteLine($"- {item.Name}{equippedStatus}");
                }

                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("1. Equip/Use item");
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
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please choose a valid option.");
                        break;
                }
            }
        }

        public void EquipItem()
        {
            Console.WriteLine("Enter the name of the item to equip:");
            string itemName = Console.ReadLine();
            var item = _inventory.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

            if (item != null && (item is Weapon || item is Armor || item is GoldenKey || item is Torch))
            {
                if (item is Weapon)
                {
                    var currentWeapon = _inventory.FirstOrDefault(i => i is Weapon && i.IsEquipped);
                    if (currentWeapon != null)
                    {
                        Console.WriteLine($"Unequipping {currentWeapon.Name}...");
                        currentWeapon.IsEquipped = false;
                        GameManager.Instance.CurrentPlayer.Stats.WeaponValue -= ((Weapon)currentWeapon).AttackPower;
                    }

                    item.Use(this);
                }
                else if (item is Armor)
                {
                    var currentArmor = _inventory.FirstOrDefault(i => i is Armor && i.IsEquipped);
                    if (currentArmor != null)
                    {
                        Console.WriteLine($"Unequipping {currentArmor.Name}...");
                        currentArmor.IsEquipped = false;
                        GameManager.Instance.CurrentPlayer.Stats.ArmorValue -= ((Armor)currentArmor).ArmorValue;
                    }

                    item.Use(this);
                }
                else if (item is GoldenKey)
                {
                    { item.Use(this); }
                    ;
                }

                else if (item is Torch)
                {
                    { item.Use(this); }
                    ;
                }

            }
            else
            {
                Console.WriteLine($"Item '{itemName}' is not a valid weapon or armor, or it is not in your inventory.");
            }
        }
        public void UnequipItem()
        {
            Console.WriteLine("Enter the name of the item to unequip:");
            string itemName = Console.ReadLine();
            var item = _inventory.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

            if (item != null && (item is Weapon || item is Armor) && item.IsEquipped)
            {
                item.IsEquipped = false;
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
                    _inventory.Sort((item1, item2) => item1.Name.CompareTo(item2.Name));
                    Console.WriteLine("Inventory sorted alphabetically.");
                    DisplayItems();
                    break;

                case "2":
                    var weapons = _inventory.OfType<Weapon>()
                                             .OrderByDescending(w => w.AttackPower)
                                             .ToList();
                    Console.WriteLine("Weapons sorted by strength (strongest first).");
                    DisplayItemsWithWeaponValue(weapons.Cast<Item>().ToList());
                    break;

                case "3":
                    var armor = _inventory.OfType<Armor>()
                                          .OrderByDescending(a => a.ArmorValue)
                                          .ToList();
                    Console.WriteLine("Armor sorted by strength (strongest first).");
                    DisplayItemsWithArmorValue(armor.Cast<Item>().ToList());
                    break;

                case "4":
                    var filteredWeapons = _inventory.OfType<Weapon>().ToList();
                    Console.WriteLine("Filtered weapons:");
                    DisplayItemsWithWeaponValue(filteredWeapons.Cast<Item>().ToList());
                    break;

                case "5":
                    var filteredArmor = _inventory.OfType<Armor>().ToList();
                    Console.WriteLine("Filtered armor:");
                    DisplayItemsWithArmorValue(filteredArmor.Cast<Item>().ToList());
                    break;

                case "6":
                    return;

                default:
                    Console.WriteLine("Invalid option. Please choose a valid option.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

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

        public void DisplayItems(IEnumerable<Item> items = null)
        {
            if (items == null)
            {
                items = _inventory;
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

        public void AddPotion(int amount = 1)
        {
            if (amount > 0)
            {
                if (_potions < 6)
                {
                    _potions += amount;
                    Console.WriteLine($"You have added {amount} potion(s) to your bag. You now have {_potions} potion(s).");
                }
                else
                {
                    Console.WriteLine("Can only hold 5 Potions in your bag.");
                }
            }
            else
            {
                Console.WriteLine("Invalid potion amount.");
            }
        }

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

        public void UseItem(string itemName)
        {
            var item = _inventory.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
            if (item != null)
            {
                item.Use(this);
            }
            else
            {
                Console.WriteLine($"Item {itemName} not found in inventory.");
            }
        }

        public void TakeDamage(int amount)
        {
            Stats.Health -= amount;
            if (Stats.Health < 0)
                Stats.Health = 0;
        }
    }

}

