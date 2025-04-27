using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



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
}