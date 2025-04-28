using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public abstract class Item : ICollectible
    {
        public string Name { get; set; }
        public bool IsEquipped { get; set; }

        public Item(string name)
        {
            Name = name;
            IsEquipped = false; // Items are not equipped by default
        }

        // Implement Collect method from ICollectible
        public virtual void Collect(Player player)
        {
            Console.WriteLine($"You collect {Name}. But it has no immediate effect.");
        }

        public virtual void Use(Player player)
        {
            Console.WriteLine($"You use {Name}, but it has no effect.");
        }
    }

    // ItemDatabase with some predefined items
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

    // Weapon class now implements ICollectible
    public class Weapon : Item
    {
        public int AttackPower { get; set; }

        public Weapon(string name, int attackPower) : base(name)
        {
            AttackPower = attackPower;
        }

        // Override Collect method from ICollectible
        public override void Collect(Player player)
        {
            Console.WriteLine($"You collect the {Name}. Your attack power will increase by {AttackPower}.");
            player.Stats.WeaponValue += AttackPower;
        }

        public override void Use(Player player)
        {
            if (!IsEquipped)
            {
                Console.WriteLine($"You equip the {Name}. Your attack power increases by {AttackPower}.");
                player.Stats.WeaponValue += AttackPower;
                IsEquipped = true;
            }
            else
            {
                Console.WriteLine($"{Name} is already equipped.");
            }
        }
    }

    // Armor class now implements ICollectible
    public class Armor : Item
    {
        public int ArmorValue { get; set; }

        public Armor(string name, int armorValue) : base(name)
        {
            ArmorValue = armorValue;
        }

        // Override Collect method from ICollectible
        public override void Collect(Player player)
        {
            Console.WriteLine($"You collect the {Name}. Your armor value will increase by {ArmorValue}.");
            player.Stats.ArmorValue += ArmorValue;
        }

        public override void Use(Player player)
        {
            if (!IsEquipped)
            {
                Console.WriteLine($"You equip the {Name}. Your armor value increases by {ArmorValue}.");
                player.Stats.ArmorValue += ArmorValue;
                IsEquipped = true;
            }
            else
            {
                Console.WriteLine($"{Name} is already equipped.");
            }
        }
    }

    // Potion class now implements ICollectible
    public class Potion : Item
    {
        public Potion() : base("Potion")
        {
        }

        // Override Collect method from ICollectible
        public override void Collect(Player player)
        {
            player.AddPotion(1);  // Adds a potion to the player's count
        }
    }

    // Key class now implements ICollectible
    public class Key : Item
    {
        public Key() : base("Key")
        {
        }

        // Override Collect method from ICollectible
        public override void Collect(Player player)
        {
            player.AddKey(1);  // Adds a key to the player's key count
        }
    }
}