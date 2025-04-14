using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Door
    {
        //Door State
        public bool IsLocked { get; private set; }
        public string DoorDescription { get; private set; }

        //Constructor to create a door with a description and lock state
        public Door(string description = "A wooden door", bool isLocked = false)
        {
            DoorDescription = description;
            IsLocked = isLocked;
        }

        //Display the door's locked status
        public void CheckDoorStatus()
        {
            Console.WriteLine(IsLocked ? "The door is locked." : "The door is unlocked.");
        }

        //Unlock the door
        public void Unlock()
        {
            if (IsLocked)
            {
                IsLocked = false;
                Console.WriteLine("You have unlocked the door.");
            }
            else
            {
                Console.WriteLine("The door is already unlocked.");
            }
        }

        //Lock the door
        public void Lock()
        {
            if (!IsLocked)
            {
                IsLocked = true;
                Console.WriteLine("You have locked the door.");
            }
            else
            {
                Console.WriteLine("The door is already locked.");
            }
        }
    }
}
