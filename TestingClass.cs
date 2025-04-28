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
            TestDoor();
            Console.WriteLine("All tests passed!");
        }

        private static void TestDoor()
        {
            Door door = new Door("Test Door", true);
            Debug.Assert(door.IsLocked, "Door should be locked initially");
        }
    }
}

