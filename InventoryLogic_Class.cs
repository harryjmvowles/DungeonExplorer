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
        public string Name { get; set; }
        public bool IsEquipped { get; set; }

        public Item(string name)
        {
            Name = name;
            IsEquipped = false;
        }

        public virtual void Collect(Player player)
        {
            Console.WriteLine($"You collect {Name}. It has no immediate effect.");
        }

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
            { "Rusty Sword", new Weapon("Rusty Sword", 5) },
            { "Battle Axe", new Weapon("Battle Axe", 13) },
            { "Enchanted Dagger", new Weapon("Enchanted Dagger", 10) },
            { "Dagger", new Weapon("Dagger", 3) },
            { "Greatsword", new Weapon("Greatsword", 15) },

            { "Leather Armor", new Armor("Leather Armor", 3) },
            { "Chainmail Armor", new Armor("Chainmail Armor", 6) },
            { "Dragon Scale Armor", new Armor("Dragon Scale Armor", 10) },
            { "Steel Armor", new Armor("Steel Armor", 7) },

            { "Potion", new Potion() },
            { "Key", new Key() },
            { "Golden Key", new GoldenKey() },
            { "Torch", new Torch() }
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

        public override void Collect(Player player)
        {
            Console.WriteLine($"You collect the {Name}. Attack power increased by {AttackPower}.");
            player.Stats.WeaponValue += AttackPower;
        }

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

        public override void Collect(Player player)
        {
            Console.WriteLine($"You collect the {Name}. Armor value increased by {ArmorValue}.");
            player.Stats.ArmorValue += ArmorValue;
        }

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
        public Potion() : base("Potion") { }

        public override void Collect(Player player)
        {
            player.AddPotion(1);
        }
    }

    // Represents a key item
    public class Key : Item
    {
        public Key() : base("Key") { }

        public override void Collect(Player player)
        {
            player.AddKey(1);
        }
    }

    // Represents a golden key
    public class GoldenKey : Item
    {
        public GoldenKey() : base("Golden Key") { }

        public override void Collect(Player player)
        {
            Console.WriteLine("You collect the Golden Key. It looks important...");
            player.AddGoldenKey();
        }

        public override void Use(Player player)
        {
            Console.WriteLine("This key is different to the rest, it sings to you... guiding you through the rooms.");
            Console.WriteLine("You feel a strange connection to the Golden Key. It might unlock something special.");
            Console.WriteLine("press any key to continue...");
            Console.ReadKey();
        }
    }

    // Represents a torch
    public class Torch : Item
    {
        public Torch() : base("Torch") { }

        public override void Collect(Player player)
        {
            Console.WriteLine("You pick up the Torch. It lights your way in dark places.");
        }

        public override void Use(Player player)
        {
            Console.WriteLine("You hold up the Torch. It illuminates the area.");
            Console.WriteLine("press any key to continue...");
            Console.ReadKey();
        }
    }
}
