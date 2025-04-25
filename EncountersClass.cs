using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DungeonExplorer.Creature;

namespace DungeonExplorer
{
    class Encounters
    {
        internal class Encounter
        {
            public static string GetName()
            {
                //Randomly generated switch case to deside what monster
                switch (rand.Next(0, 4))
                {
                    case 0:
                        return "Ashen Soldier";
                    case 1:
                        return "Goblin";
                    case 2:
                        return "Troll";
                    case 3:
                        return "Demon";
                    default:
                        return "Skeleton";

                }
            }

            public static string GetWeapon()
            {
                //Randomly generated switch case to deside what weapon
                switch (rand.Next(0, 4))
                {
                    case 0:
                        return "Dagger";
                    case 1:
                        return "Wand";
                    case 2:
                        return "Battle Axe";
                    case 3:
                        return "Sword";
                    default:
                        return "Fist";

                }
            }

            static Random rand = new Random();
            //ENCOUNTERS
            public static void FirstEncounter()
            {
                Console.WriteLine("You enter the Dungeon, a creature stood in the corner of the dark lit room.");
                Console.WriteLine("unprepared for your journey, you only have a small dagger...");
                Console.ReadKey();
                Console.Clear();
                Combat(false, "Skeleton", "Dagger", 40);
            }
            public static void BasicEncounter()
            {
                Console.WriteLine("You have been detected, a creature approaches you!");
                Combat(true, "", "", 0);
            }



            //This Handles Combat of random and pre-determined monsters.
            public static void Combat(bool random, string name, string weapon, int health)
            {
                Enemy enemy; // new enemy object

                if (random)
                {
                    string randomName = GetName();
                    string randomWeapon = GetWeapon();
                    int randomHealth = rand.Next(25, 100);
                    int randomArmorValue = rand.Next(1, 5);  // Random armor value
                    int attackPower = GetWeaponPower(randomWeapon); // Get the weapon power for the enemy

                    // Ensure all parameters are passed, including weaponValue
                    enemy = new Enemy(randomName, randomHealth, attackPower, randomWeapon, randomArmorValue, attackPower);
                }
                else
                {
                    int attackPower = GetWeaponPower(weapon); // Get the weapon power for the player
                    int enemyArmorValue = 5;  // Default armor value for the enemy

                    // Ensure all parameters are passed, including weaponValue
                    enemy = new Enemy(name, health, attackPower, weapon, enemyArmorValue, attackPower);
                }

                Console.WriteLine($"You are now fighting a {enemy.Name}.");
                Console.WriteLine($"They wield a {enemy.Weapon} with an armor value of {enemy.ArmorValue}.");
                Console.ReadKey();

                while (enemy.Health > 0 && GameManager.Instance.CurrentPlayer.Health > 0)
                {
                    Console.Clear();
                    Console.WriteLine("Potions: " + GameManager.Instance.CurrentPlayer.Potions);
                    Console.WriteLine("Health: " + GameManager.Instance.CurrentPlayer.Health);
                    Console.WriteLine($"{enemy.Name}: {enemy.Health}");
                    Console.WriteLine("-------------------------");
                    Console.WriteLine("|  (A)ttack    (D)efend  |");
                    Console.WriteLine("|  (R)un       (H)eal    |");
                    Console.WriteLine("-------------------------");
                    Console.WriteLine("Make your move! Enter A, D, R, or H:");
                    string move = Console.ReadLine().ToLower();

                    string n = enemy.Name;
                    int h = enemy.Health;
                    var player = GameManager.Instance.CurrentPlayer;

                    // Attack
                    if (move == "a")
                    {
                        Console.WriteLine("You attack your enemy!");
                        int attack = rand.Next(0, player.WeaponValue + 1) + rand.Next(1, 4); // Player's weapon value for attack
                        int damage = enemy.ArmorValue - player.WeaponValue; // Calculate the damage to the enemy
                        if (damage < 0)
                            damage = 0;
                        Console.WriteLine($"You deal {attack} damage to the {n}.");
                        enemy.Health -= attack;
                    }

                    // Defend
                    else if (move == "d")
                    {
                        Console.WriteLine("You defend yourself against your enemy!");
                        int attack = rand.Next(0, player.WeaponValue + 1); // Player's defense attack
                        int damage = (enemy.WeaponValue / 4) - player.ArmorValue; // Calculate the damage to the player when defending
                        if (damage < 0)
                            damage = 0;
                        Console.WriteLine($"You only lose {damage} health, and manage to serve {damage} damage to the {n}.");
                        player.Health -= damage;
                        enemy.Health -= attack;
                    }

                    // Heal
                    else if (move == "h")
                    {
                        if (player.Potions == 0)
                        {
                            int damage = enemy.WeaponValue - player.ArmorValue;
                            if (damage < 0)
                                damage = 0;
                            Console.WriteLine("You reach for your potions, but find you do not have any left! You are unable to heal yourself.");
                            player.Health -= damage;
                            Console.WriteLine($"While you were distracted, the {n} attacks! You lose {damage} health.");
                        }
                        else
                        {
                            Console.WriteLine("You grab a potion from your bag, drinking it as fast as you can.");
                            player.UsePotion();
                            Console.WriteLine($"You have {player.Potions} potions left.");
                            Console.WriteLine($"You have {player.Health} Health.");
                        }
                    }

                    // Run
                    else if (move == "r")
                    {
                        int chance = rand.Next(0, 2);
                        if (chance == 0)
                        {
                            Console.WriteLine("You successfully escaped!");
                            break;
                        }
                        else
                        {
                            int damage = enemy.WeaponValue - player.ArmorValue;
                            if (damage < 0)
                                damage = 0;
                            Console.WriteLine($"You failed to escape! The {n} strikes you for {damage} damage!");
                            player.Health -= damage;
                        }
                    }

                    // Invalid input
                    else
                    {
                        Console.WriteLine($"That is not a valid move. The {n} looks at you confused.");
                        Console.WriteLine("Please try again.");
                    }

                    Console.ReadKey();
                }
            }


            public static int GetWeaponPower(string weapon)
            {
                switch (weapon)
                {
                    case "Dagger":
                        return 10;
                    case "Wand":
                        return 15;
                    case "Battle Axe":
                        return 20;
                    case "Sword":
                        return 13;
                    default:
                        return 5;
                }
            }



        }
        
    }
}