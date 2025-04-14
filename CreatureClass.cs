using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    //The CreatureClass is to be used to inherit for player and enemy classes.
    public abstract class Creature
    {
        public string Name { get; protected set; }
        public int Health { get; protected set; }
        public int Attack { get; protected set; }
        public int Defense { get; protected set; }

        public Creature(string name, int health = 100, int attack = 10, int defense = 5)
        {
            Name = name;
            Health = health;
            Attack = attack;
            Defense = defense;
        }

    }
}
