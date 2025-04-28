using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    // Represents an item that can be collected and used
    public abstract class Item : ICollectible
    {
        public string Name { get; set; } // Item's name
        public bool IsEquipped { get; set; } // Whether the item is equipped

        public Item(string name)
        {
            Name = name;
            IsEquipped = false; // Default to not equipped
        }

        // Default collect action
        public virtual void Collect(Player player)
        {
            Console.WriteLine($"You collect {Name}. It has no immediate effect.");
        }

        // Default use action
        public virtual void Use(Player player)
        {
            Console.WriteLine($"You use {Name}, but nothing happens.");
        }
    }

    // Stores predefined items in the game
    public static class ItemDatabase
    {
        public static Dictionary<string, Item> Items = new Dictionary<string, Item>()
        {
            // Weapons
            { "Rusty Sword", new Weapon("Rusty Sword", 5) },
            { "Battle Axe", new Weapon("Battle Axe", 13) },
            { "Enchanted Dagger", new Weapon("Enchanted Dagger", 10) },
            { "Dagger", new Weapon("Dagger", 3) },

            // Armors
            { "Leather Armor", new Armor("Leather Armor", 3) },
            { "Chainmail Armor", new Armor("Chainmail Armor", 6) },
            { "Dragon Scale Armor", new Armor("Dragon Scale Armor", 10) },

            // Potions and Keys
            { "Potion", new Potion() },
            { "Key", new Key() }
        };
    }

    // Represents a weapon item
    public class Weapon : Item
    {
        public int AttackPower { get; set; }

        public Weapon(string name, int attackPower) : base(name)
        {
            AttackPower = attackPower;
        }

        // Collect weapon and increase attack power
        public override void Collect(Player player)
        {
            Console.WriteLine($"You collect the {Name}. Attack power increased by {AttackPower}.");
            player.Stats.WeaponValue += AttackPower;
        }

        // Equip weapon to increase attack power
        public override void Use(Player player)
        {
            if (!IsEquipped)
            {
                Console.WriteLine($"You equip {Name}. Attack power increased.");
                player.Stats.WeaponValue += AttackPower;
                IsEquipped = true;
            }
            else
            {
                Console.WriteLine($"{Name} is already equipped.");
            }
        }
    }

    // Represents an armor item
    public class Armor : Item
    {
        public int ArmorValue { get; set; }

        public Armor(string name, int armorValue) : base(name)
        {
            ArmorValue = armorValue;
        }

        // Collect armor and increase defense
        public override void Collect(Player player)
        {
            Console.WriteLine($"You collect the {Name}. Armor value increased by {ArmorValue}.");
            player.Stats.ArmorValue += ArmorValue;
        }

        // Equip armor to increase defense
        public override void Use(Player player)
        {
            if (!IsEquipped)
            {
                Console.WriteLine($"You equip {Name}. Armor value increased.");
                player.Stats.ArmorValue += ArmorValue;
                IsEquipped = true;
            }
            else
            {
                Console.WriteLine($"{Name} is already equipped.");
            }
        }
    }

    // Represents a potion item
    public class Potion : Item
    {
        public Potion() : base("Potion")
        {
        }

        // Collect potion and add it to inventory
        public override void Collect(Player player)
        {
            player.AddPotion(1);
        }
    }

    // Represents a key item
    public class Key : Item
    {
        public Key() : base("Key")
        {
        }

        // Collect key and add it to inventory
        public override void Collect(Player player)
        {
            player.AddKey(1);
        }
    }
}
