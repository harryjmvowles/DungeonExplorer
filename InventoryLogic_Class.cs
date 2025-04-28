using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public abstract class Item
    {
        public string Name { get; set; }
        public bool IsEquipped { get; set; }

        public Item(string name)
        {
            Name = name;
            IsEquipped = false; // Items are not equipped by default
        }

        public virtual void Use(Player player)
        {
            Console.WriteLine($"You use {Name}, but it has no effect.");
        }
    }
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

        };
    }

    public class Weapon : Item
    {
        public int AttackPower { get; set; }

        public Weapon(string name, int attackPower) : base(name)
        {
            AttackPower = attackPower;
        }

        public override void Use(Player player)
        {
            if (!IsEquipped)
            {
                Console.WriteLine($"You equip the {Name}. Your attack power increases by {AttackPower}.");
                GameManager.Instance.CurrentPlayer.Stats.WeaponValue += AttackPower;
                IsEquipped = true;
            }
            else
            {
                Console.WriteLine($"{Name} is already equipped.");
            }
        }
    }

    public class Armor : Item
    {
        public int ArmorValue { get; set; }

        public Armor(string name, int armorValue) : base(name)
        {
            ArmorValue = armorValue;
        }

        public override void Use(Player player)
        {
            if (!IsEquipped)
            {
                Console.WriteLine($"You equip the {Name}. Your armor value increases by {ArmorValue}.");
                GameManager.Instance.CurrentPlayer.Stats.ArmorValue += ArmorValue;
                IsEquipped = true;
            }
            else
            {
                Console.WriteLine($"{Name} is already equipped.");
            }
        }
    }

    // Potion class
    public class Potion : Item
    {
        public Potion() : base("Potion")
        {
        }

        public override void Use(Player player)
        {
            player.AddPotion(1); // Adds a potion to the player's count
            
        }
    }

    // Key class
    public class Key : Item
    {
        public Key() : base("Key")
        {
        }

        public override void Use(Player player)
        {
            player.AddKey(1); // Adds a key to the player's key count
        
        }
    }





}
