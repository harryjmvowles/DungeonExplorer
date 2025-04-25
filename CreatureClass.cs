using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    // The CreatureClass is used to inherit for player and enemy classes.
    public abstract class Creature
    {
        public string Name { get; protected set; }
        public int Health { get; protected internal set; }
        public int Defense { get; protected set; }
        public int ArmorValue { get; protected set; }
        public int WeaponValue { get; protected set; }

        // Constructor for Creature Class (Player and Enemy)
        public Creature(string name, int health = 100, int defense = 5, int armorValue = 5, int weaponValue = 5)
        {
            Name = name;
            Health = health;
            Defense = defense;
            ArmorValue = armorValue;
            WeaponValue = weaponValue;
        }

        public class Enemy : Creature
        {
            public string Weapon { get; private set; }

            // Enemy-specific constructor with weapon, armor, stats, etc.
            public Enemy(string name, int health, int defense, string weapon, int armorValue, int weaponValue)
                : base(name, health, defense, armorValue, weaponValue)
            {
                Weapon = weapon;
            }
        }
    }
}