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
        //Door State
        public bool IsLocked { get; private set; }
        public string DoorDescription { get; private set; }
        public string KeyRequired { get; private set; } //Key required to unlock the door

        //Constructor to create a door with a description, lock state, and key requirement
        public Door(string description = "A wooden door", bool isLocked = false, string keyRequired = "")
        {
            DoorDescription = description;
            IsLocked = isLocked;
            KeyRequired = keyRequired;
        }

        //Display the door's locked status
        public void CheckDoorStatus()
        {
            Console.WriteLine(IsLocked ? "The door is locked." : "The door is unlocked.");
        }

        //Unlock the door
        public bool TryUnlock(string key)
        {
            if (IsLocked)
            {
                if (key == KeyRequired)
                {
                    IsLocked = false;
                    Console.WriteLine("You have unlocked the door.");
                    return true; // Door unlocked successfully
                }
                else
                {
                    Console.WriteLine("This door is locked. You need the correct key.");
                    return false; // Incorrect key
                }
            }
            else
            {
                Console.WriteLine("The door is already unlocked.");
                return true; // Door is already unlocked
            }
        }
    }
}