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

        // First scripted encounter
        public static void FirstEncounter()
        {
            Console.WriteLine("As you enter you realise your gut was right! A Creature stood in the corner of the dark lit room.");
            Console.WriteLine("Thank the lord that mysterious man gave you a Dagger.");
            Console.WriteLine("You have no choice but to fight... unless you can run!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();

            // Hardcoded Dagger stats: 10 ATK, 2 DEF
            var enemy = new Enemy("Skeleton", 40, "Dagger", 2, 10);
            Combat(enemy);
        }

        // Random basic encounter with a 50% chance
        public static void BasicEncounter()
        {
            if (rand.Next(0, 2) == 0)
            {
                Console.WriteLine("You sense danger... but nothing appears.");
                Console.WriteLine("You can continue on your way.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("You have been detected, a creature approaches you!");

                IDamageable enemy;
                int type = rand.Next(0, 4);

                switch (type)
                {
                    case 0:
                        enemy = new GoblinMonster();
                        break;
                    case 1:
                        enemy = new TrollMonster();
                        break;
                    case 2:
                        enemy = new DemonMonster();
                        break;
                    default:
                        enemy = new SkeletonMonster();
                        break;
                }

                Combat(enemy);
            }
        }

        // Handles the combat process
        public static void Combat(IDamageable enemy)
        {
            Enemy enemyObj = enemy as Enemy;

            Console.WriteLine($"You are now fighting a {enemyObj.Name}.");
            Console.WriteLine($"They wield a {enemyObj.Weapon}: {enemyObj.Stats.WeaponValue} Damage, with an armor value of {enemyObj.Stats.ArmorValue}.");
            Console.ReadKey();

            while (enemyObj.Stats.Health > 0 && GameManager.Instance.CurrentPlayer.Stats.Health > 0)
            {
                Console.Clear();

                // Display player and enemy stats
                var player = GameManager.Instance.CurrentPlayer;
                Console.WriteLine("Your Stats:");
                Console.WriteLine("Health: " + player.Stats.Health);
                Console.WriteLine("Potions: " + player.Potions);
                Console.WriteLine($"ATK: {player.Stats.WeaponValue}, DEF: {player.Stats.ArmorValue}");
                Console.WriteLine($"\n{enemyObj.Name} Health: {enemyObj.Stats.Health}");
                Console.WriteLine("-------------------------");
                Console.WriteLine("|  (A)ttack    (D)efend  |");
                Console.WriteLine("|  (R)un       (H)eal    |");
                Console.WriteLine("-------------------------");
                Console.WriteLine("Make your move! Enter A, D, R, or H:");
                string move = Console.ReadLine().ToLower();

                if (move == "a")
                {
                    Console.WriteLine("You attack the enemy!");
                    int attack = rand.Next(0, player.Stats.WeaponValue + 1) + rand.Next(1, 4);
                    int damage = player.Stats.CalculateDamage(enemyObj.Stats.ArmorValue, attack);

                    if (damage >= 15)
                        Console.WriteLine("Critical hit! You strike with great force!");
                    else if (damage <= 5)
                        Console.WriteLine("Weak hit... barely a scratch.");

                    Console.WriteLine($"You deal {damage} damage to {enemyObj.Name}.");
                    enemyObj.TakeDamage(damage);

                    // Enemy counterattack
                    int eDamage = enemyObj.Stats.CalculateDamage(player.Stats.ArmorValue, enemyObj.Stats.WeaponValue);
                    Console.WriteLine($"{enemyObj.Name} strikes back for {eDamage} damage!");
                    player.TakeDamage(eDamage);

                    // Monster special ability
                    if (enemyObj is BaseMonster monster && rand.Next(0, 4) == 0)
                    {
                        monster.PerformSpecialAbility();
                    }
                }
                else if (move == "d")
                {
                    Console.WriteLine("You brace for the incoming attack!");

                    int baseDamage = enemyObj.Stats.WeaponValue;
                    int reducedDamage = (int)(baseDamage * 0.6) - (player.Stats.ArmorValue / 2);
                    if (reducedDamage < 1) reducedDamage = 1;

                    int counterAttack = rand.Next(1, player.Stats.WeaponValue / 2 + 2);

                    Console.WriteLine($"You take {reducedDamage} damage and counterattack for {counterAttack} damage!");
                    player.TakeDamage(reducedDamage);
                    enemyObj.TakeDamage(counterAttack);
                }
                else if (move == "h")
                {
                    if (player.Potions == 0)
                    {
                        int damage = enemyObj.Stats.CalculateDamageTaken(player.Stats.WeaponValue, player.Stats.ArmorValue);
                        Console.WriteLine("You try to heal, but you have no potions left!");
                        player.TakeDamage(damage);
                        Console.WriteLine($"The {enemyObj.Name} attacks while you're distracted! You lose {damage} health.");
                    }
                    else
                    {
                        Console.WriteLine("You drink a potion and regain some health.");
                        player.UsePotion();
                        Console.WriteLine($"You have {player.Potions} potions left.");
                        Console.WriteLine($"You have {player.Stats.Health} health remaining.");
                    }
                }
                else if (move == "r")
                {
                    int chance = rand.Next(0, 2);
                    if (chance == 0)
                    {
                        Console.WriteLine("You successfully escape!");
                        Console.WriteLine($"You have {player.Stats.Health} health left.");
                        Console.WriteLine($"You have {player.Potions} potions remaining.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        return;
                    }
                    else
                    {
                        int damage = enemyObj.Stats.CalculateDamageTaken(player.Stats.WeaponValue, player.Stats.ArmorValue);
                        Console.WriteLine($"You fail to escape! The {enemyObj.Name} attacks for {damage} damage!");
                        player.TakeDamage(damage);
                    }
                }
                else
                {
                    Console.WriteLine("That's not a valid move. Try again.");
                }

                Console.ReadKey();
            }

            // End of combat
            Console.Clear();
            if (enemyObj.Stats.Health <= 0)
            {
                Console.WriteLine($"You defeated the {enemyObj.Name}!");
                Console.WriteLine($"You have {GameManager.Instance.CurrentPlayer.Stats.Health} health left.");
                Console.WriteLine($"You have {GameManager.Instance.CurrentPlayer.Potions} potions remaining.");
                Console.WriteLine("Press any key to continue...");
            }
            else if (GameManager.Instance.CurrentPlayer.Stats.Health <= 0)
            {
                Console.WriteLine("You have been defeated...");
                GameManager.GameOver(false);
            }

            var stats = GameManager.Instance.CurrentPlayer.Stats;

            // Reset to base values
            stats.WeaponValue = GameManager.Instance.CurrentPlayer.DefaultWeaponValue;
            stats.ArmorValue = GameManager.Instance.CurrentPlayer.DefaultArmorValue;

            // Reapply equipped weapon bonus
            foreach (var item in GameManager.Instance.CurrentPlayer.Inventory)
            {
                if (item is Weapon weapon && weapon.IsEquipped)
                {
                    stats.WeaponValue += weapon.AttackPower;
                }

                if (item is Armor armor && armor.IsEquipped)
                {
                    stats.ArmorValue += armor.ArmorValue;
                }
            }


            Console.ReadKey();
        }
    }
}




