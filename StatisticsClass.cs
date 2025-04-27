using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Statistics
    {
        public int Health { get; set; }
        public int ArmorValue { get; set; }
        public int WeaponValue { get; set; }

        // Formula to calculate damage dealt based on player's attack
        public int CalculateDamage(int enemyArmorValue, int attackPower)
        {
            int damage = attackPower - enemyArmorValue;
            if (damage < 0) damage = 0;
            return damage;
        }

        // Formula to calculate damage taken by player
        public int CalculateDamageTaken(int weaponValue, int playerArmorValue)
        {
            int damage = weaponValue - playerArmorValue;
            if (damage < 0) damage = 0;
            return damage;
        }
    }


}
