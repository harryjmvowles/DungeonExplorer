using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using DungeonExplorer;

namespace DungeonExplorer
{
    public class Door
    {
        // Door State
        public bool IsLocked { get; private set; }
        public string DoorDescription { get; private set; }
        public string Direction { get; set; }

        // Bidirectional room links
        public string RoomA { get; private set; }
        public string RoomB { get; private set; }

        // Constructor for bidirectional doors
        public Door(string description = "A wooden door", bool isLocked = false, string direction = "Unknown", string roomA = null, string roomB = null)
        {
            DoorDescription = description;
            IsLocked = isLocked;
            Direction = direction;
            RoomA = roomA;
            RoomB = roomB;
        }

        // Display the door's locked status
        public void CheckDoorStatus()
        {
            Console.WriteLine(IsLocked ? "The door is locked." : "The door is unlocked.");
        }

        // Unlock the door
        public bool TryUnlock()
        {
            if (IsLocked)
            {
                IsLocked = false;
                return true;
            }

            return true; // Already unlocked
        }

        // Get the opposite room from the current one
        public string GetOtherSide(string currentRoom)
        {
            if (currentRoom == RoomA) return RoomB;
            if (currentRoom == RoomB) return RoomA;

            throw new InvalidOperationException($"Room '{currentRoom}' is not connected to this door.");
        }
    }

}
