using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Testing
    {
        public static void RunTests()
        {
            TestPlayer();
            TestDoor();
            Console.WriteLine("All tests passed!");
        }

        private static void TestPlayer()
        {
            Player player = new Player("TestPlayer");
            Debug.Assert(player.Name == "TestPlayer", "Player name should be TestPlayer");
            Debug.Assert(player.Health == 100, "Player health should start at 100");
            Debug.Assert(player.Potions == 0, "Player should start with 0 potions");

            player.AddToInventory("Sword");
            Debug.Assert(player.Inventory.Contains("Sword"), "Inventory should contain 'Sword'");
        }

        private static void TestDoor()
        {
            Door door = new Door("Test Door", true);
            Debug.Assert(door.IsLocked, "Door should be locked initially");

            door.Unlock();
            Debug.Assert(!door.IsLocked, "Door should be unlocked after Unlock() is called");

            door.Lock();
            Debug.Assert(door.IsLocked, "Door should be locked after Lock() is called");
        }
    }
}

