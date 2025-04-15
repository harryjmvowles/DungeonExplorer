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
        public string LeadsTo { get; set; } //Room this door connects to

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
        public bool TryUnlock()
        {
            if (IsLocked)
            {
                IsLocked = false;
                Console.WriteLine("You have unlocked the door.");
                return true; //Door unlocked successfully
            }
            else
            {
                Console.WriteLine("The door is already unlocked.");
                return true; //Door is already unlocked
            }
        }
    }
}