using DungeonExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DungeonExplorer
{
    //RoomManager handles predefined rooms and provides them when needed
    public class RoomManager
    {
        //Dictionary to store rooms by name
        private Dictionary<string, Room> predefinedRooms;

        //Constructor to initialize predefined rooms
        public RoomManager()
        {
            predefinedRooms = new Dictionary<string, Room>();

            //Predefine rooms here
            predefinedRooms.Add("The Lost Hall", new Room(
                "The Lost Hall",
                "A dark room with decaying stone walls and a rotten wooden floor.",
                new List<string> {""},
                new Dictionary<string, PointOfInterest>
                {
                    {
                        "Desk",
                        new PointOfInterest("Desk", "A sturdy wooden desk with a drawer.", new List<string> { "Key"})
                    },
                    {
                        "Chest",
                        new PointOfInterest("Chest", "A small wooden chest that is slightly cracked open.", new List<string> { "Torch" })
                    }
                }
            ));
        }
    
     //Method to get a room by name
        public Room GetRoom(string roomName)
        {
            if (predefinedRooms.ContainsKey(roomName))
            {
                return predefinedRooms[roomName];
            }
            else
            {
                return null;
            }
        }
    }
}