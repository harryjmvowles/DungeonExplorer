using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DungeonExplorer.Creature;

namespace DungeonExplorer
{

    internal class Encounter
    {
        static Random rand = new Random();

        // Randomly get an enemy name
        public static string GetName()
        {
            switch (rand.Next(0, 4))
            {
                case 0: return "forgotten Troop";
                case 1: return "Goblin";
                case 2: return "Troll";
                case 3: return "Demon";
                default: return "Skeleton";
            }
        }

        // Randomly get a weapon name
        public static string GetWeapon()
        {
            switch (rand.Next(0, 4))
            {
                case 0: return "Dagger";
                case 1: return "Wand";
                case 2: return "Battle Axe";
                case 3: return "Sword";
                default: return "Fist";
            }
        }

        // Randomly get an armor value for enemy using switch
        public static int GetArmorValue()
        {
            switch (rand.Next(1, 6)) // Random 1-5
            {
                case 1: return 1;
                case 2: return 2;
                case 3: return 3;
                case 4: return 4;
                case 5: return 5;
                default: return 1;
            }
        }

        // First scripted encounter
        public static void FirstEncounter()
        {
            Console.WriteLine("As you enter you realise your gut was right! A Creature stood in the corner of the dark lit room.");
            Console.WriteLine("Thank the lord that mysterious man gave you a Dagger.");
            Console.WriteLine("You have no choice but to fight... unless you can run!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            Combat(false, "Skeleton", "Dagger", 40);
        }

        // Random basic encounter with a 50% chance
        public static void BasicEncounter()
        {
            if (rand.Next(0, 2) == 0)
            {
                Console.WriteLine("You sense danger... but nothing appears.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("You have been detected, a creature approaches you!");
                Combat(true, "", "", 0);
            }
        }

        // Handles combat
        public static void Combat(bool random, string name, string weapon, int health)
        {
            IDamageable enemy;

            if (random)
            {
                string randomName = GetName();
                string randomWeapon = GetWeapon();
                int randomHealth = rand.Next(25, 75);
                int randomArmorValue = GetArmorValue();
                int attackPower = GetWeaponPower(randomWeapon);

                enemy = new Enemy(randomName, randomHealth, randomWeapon, randomArmorValue, attackPower);
            }
            else
            {
                int attackPower = GetWeaponPower(weapon);
                int enemyArmorValue = GetArmorValue();

                enemy = new Enemy(name, health, weapon, enemyArmorValue, attackPower);
            }

            Enemy enemyObj = enemy as Enemy;

            Console.WriteLine($"You are now fighting a {enemyObj.Name}.");
            Console.WriteLine($"They wield a {enemyObj.Weapon}: {enemyObj.Stats.WeaponValue} Damage, with an armor value of {enemyObj.Stats.ArmorValue}.");
            Console.ReadKey();

            while (enemyObj.Stats.Health > 0 && GameManager.Instance.CurrentPlayer.Stats.Health > 0)
            {
                Console.Clear();
                Console.WriteLine("Potions: " + GameManager.Instance.CurrentPlayer.Potions);
                Console.WriteLine("Health: " + GameManager.Instance.CurrentPlayer.Stats.Health);
                Console.WriteLine($"ATK: {GameManager.Instance.CurrentPlayer.Stats.WeaponValue}, DEF: {GameManager.Instance.CurrentPlayer.Stats.ArmorValue}");
                Console.WriteLine($"{enemyObj.Name} Health: {enemyObj.Stats.Health}");
                Console.WriteLine("-------------------------");
                Console.WriteLine("|  (A)ttack    (D)efend  |");
                Console.WriteLine("|  (R)un       (H)eal    |");
                Console.WriteLine("-------------------------");
                Console.WriteLine("Make your move! Enter A, D, R, or H:");
                string move = Console.ReadLine().ToLower();

                var player = GameManager.Instance.CurrentPlayer;

                if (move == "a")
                {
                    Console.WriteLine("You attack your enemy!");
                    int attack = rand.Next(0, player.Stats.WeaponValue + 1) + rand.Next(1, 4);
                    int damage = player.Stats.CalculateDamage(enemyObj.Stats.ArmorValue, attack);

                    if (damage >= 15)
                        Console.WriteLine("Critical Hit! You hit with great force!");
                    else if (damage <= 5)
                        Console.WriteLine("Weak hit... barely a scratch.");

                    Console.WriteLine($"You deal {damage} damage to the {enemyObj.Name}.");
                    enemyObj.TakeDamage(damage);

                    int eDamage = enemyObj.Stats.CalculateDamage(player.Stats.ArmorValue, enemyObj.Stats.WeaponValue);

                    Console.WriteLine($"The {enemyObj.Name} strikes back at you for {eDamage} damage!");
                    player.TakeDamage(eDamage);
                }
                else if (move == "d")
                {
                    Console.WriteLine("You defend yourself against the enemy!");
                    int attack = rand.Next(0, player.Stats.WeaponValue + 1);
                    int damage = (enemyObj.Stats.WeaponValue / 2) - player.Stats.ArmorValue;
                    if (damage < 0) damage = 0;

                    Console.WriteLine($"You take {damage} reduced damage and counterattack for {attack} damage!");
                    player.TakeDamage(damage);
                    enemyObj.TakeDamage(attack);
                }
                else if (move == "h")
                {
                    if (player.Potions == 0)
                    {
                        int damage = enemyObj.Stats.CalculateDamageTaken(player.Stats.WeaponValue, player.Stats.ArmorValue);
                        Console.WriteLine("You go to grab a potion but there is none left! You can't heal...");
                        player.TakeDamage(damage);
                        Console.WriteLine($"While you were distracted, the {enemyObj.Name} attacks! You lose {damage} health.");
                    }
                    else
                    {
                        Console.WriteLine("You grab a potion, drinking it as fast as you can during battle.");
                        player.UsePotion();
                        Console.WriteLine($"You have {player.Potions} potions left.");
                        Console.WriteLine($"You have {player.Stats.Health} Health.");
                    }
                }
                else if (move == "r")
                {
                    int chance = rand.Next(0, 2);
                    if (chance == 0)
                    {
                        Console.WriteLine("You successfully escaped!");
                        Console.ReadKey();
                        return;
                    }
                    else
                    {
                        int damage = enemyObj.Stats.CalculateDamageTaken(player.Stats.WeaponValue, player.Stats.ArmorValue);
                        Console.WriteLine($"You failed to escape! The {enemyObj.Name} strikes you for {damage} damage!");
                        player.TakeDamage(damage);
                    }
                }
                else
                {
                    Console.WriteLine($"That is not a valid move. The {enemyObj.Name} looks at you confused.");
                    Console.WriteLine("Please try again.");
                }

                Console.ReadKey();
            }

            Console.Clear();
            if (enemyObj.Stats.Health <= 0)
            {
                Console.WriteLine($"You have defeated the {enemyObj.Name}!");
                Console.WriteLine($"You have {GameManager.Instance.CurrentPlayer.Stats.Health} health remaining.");
                Console.WriteLine($"You have {GameManager.Instance.CurrentPlayer.Potions} potions left.");
                Console.WriteLine("Press any key to continue...");
               
            }
            else if (GameManager.Instance.CurrentPlayer.Stats.Health <= 0)
            {
                Console.WriteLine("You have been slain...");
            }
            Console.ReadKey();
        }





        // Return weapon power based on weapon type
        public static int GetWeaponPower(string weapon)
        {
            switch (weapon)
            {
                case "Dagger": return 10;
                case "Wand": return 15;
                case "Battle Axe": return 20;
                case "Sword": return 13;
                default: return 5;
            }
        }
    }




}

        
    