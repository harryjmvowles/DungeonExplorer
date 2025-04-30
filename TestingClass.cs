using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DungeonExplorer
{
    public class Tests
    {
        // A simple subclass of Item for testing purposes
        public class TestItem : Item
        {
            public TestItem(string name) : base(name) { }

            public override void Collect(Player player)
            {
                Console.WriteLine($"Test item {Name} collected.");
            }

            public override void Use(Player player)
            {
                Console.WriteLine($"Test item {Name} used.");
            }
        }

        public class TestPotion : Item, ICollectible
        {
            public TestPotion(string name) : base(name) { }

            public override void Use(Player player)
            {
                player.Stats.Health += 25;
            }

            public override void Collect(Player player)
            {
                player.AddPotion();
            }
        }

        public class TestKey : Item, ICollectible
        {
            public TestKey(string name) : base(name) { }

            public override void Use(Player player)
            {
                // Optional logic
            }

            public override void Collect(Player player)
            {
                player.AddKey();
            }
        }

        public static void TestAddItem()
        {
            var room = new Room("Test Room", "A room for testing items", new List<Item>(), new Dictionary<string, PointOfInterest>(), new Dictionary<string, Door>());
            var item = new TestItem("Torch");
            room.AddItem(item);

            Debug.Assert(room.Items.Contains(item), "AddItem failed: Item not added.");
            Console.WriteLine("TestAddItem passed.");
        }

        public static void TestAddExit()
        {
            var room = new Room("Test Room", "A room for testing exits", new List<Item>(), new Dictionary<string, PointOfInterest>(), new Dictionary<string, Door>());
            var door = new Door("North", false);
            room.AddExit("North", door);

            Debug.Assert(room.Exits.ContainsKey("North"), "AddExit failed: 'North' exit not found.");
            Debug.Assert(room.Exits["North"] == door, "AddExit failed: Door object mismatch.");
            Console.WriteLine("TestAddExit passed.");
        }

        public static void TestAddPotion()
        {
            var player = new Player("Tester");
            player.AddPotion();
            Debug.Assert(player.Potions == 1, "AddPotion failed: Potion count incorrect.");
            Console.WriteLine("TestAddPotion passed.");
        }

        public static void TestAddKey()
        {
            var player = new Player("Tester");
            player.AddKey(2);
            Debug.Assert(player.Keys == 2, "AddKey failed: Key count incorrect.");
            Console.WriteLine("TestAddKey passed.");
        }

        public static void TestUsePotion()
        {
            var player = new Player("Tester", health: 50);
            player.AddPotion();
            player.UsePotion();
            Debug.Assert(player.Stats.Health == 75, "UsePotion failed: Health not restored correctly.");
            Debug.Assert(player.Potions == 0, "UsePotion failed: Potion not consumed.");
            Console.WriteLine("TestUsePotion passed.");
        }

        public static void TestUseKey()
        {
            var player = new Player("Tester");
            player.AddKey();
            player.UseKey();
            Debug.Assert(player.Keys == 0, "UseKey failed: Key not consumed.");
            Console.WriteLine("TestUseKey passed.");
        }

        public static void TestInventoryAddAndUse()
        {
            var player = new Player("Tester");
            var item = new TestItem("Magic Ring");
            player.AddToInventory(item);
            Debug.Assert(player.Inventory.Contains(item), "AddToInventory failed.");
            player.UseItem("Magic Ring");
            Console.WriteLine("TestInventoryAddAndUse passed.");
        }

        public static void RunTests()
        {
            TestAddItem();
            TestAddExit();
            TestAddPotion();
            TestAddKey();
            TestUsePotion();
            TestUseKey();
            TestInventoryAddAndUse();

            Console.WriteLine("All tests completed.");
        }
    }
}

