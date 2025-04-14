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
                new List<string> { "North", "East" },  //Available doors
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

            predefinedRooms.Add("The Forgotten Chamber", new Room(
                "The Forgotten Chamber",
                "A musty chamber filled with the scent of mildew, with vines creeping up the walls.",
                new List<string> { "West", "South" },  //Available doors
                new Dictionary<string, PointOfInterest>
                {
                    {
                        "Bookshelf",
                        new PointOfInterest("Bookshelf", "A dusty bookshelf filled with ancient, forgotten tomes.", new List<string> { "Potion" })
                    },
                    {
                        "Statue",
                        new PointOfInterest("Statue", "A broken statue of a long-forgotten hero.", new List<string> { "Hammer" })
                    }
                }
            ));

            predefinedRooms.Add("The Silent Corridor", new Room(
                "The Silent Corridor",
                "A narrow hallway with an eerie silence, the air feels thick with tension.",
                new List<string> { "North", "East" },  //Available doors
                new Dictionary<string, PointOfInterest>
                {
                    {
                        "Portrait",
                        new PointOfInterest("Portrait", "A painting of an ancient family, their eyes seeming to follow you.", new List<string> { "Sword" })
                    },
                    {
                        "Candles",
                        new PointOfInterest("Candles", "A set of candles flickering in the cold air.", new List<string> { "Torch" })
                    }
                }
            ));

            predefinedRooms.Add("The Grand Hall", new Room(
                "The Grand Hall",
                "A majestic hall with high vaulted ceilings and broken chandeliers.",
                new List<string> { "West", "South" },  //Available doors
                new Dictionary<string, PointOfInterest>
                {
                    {
                        "Throne",
                        new PointOfInterest("Throne", "An old throne, covered in dust and cobwebs.", new List<string> { "Crown" })
                    },
                    {
                        "Pillars",
                        new PointOfInterest("Pillars", "Massive stone pillars that support the hall's great ceiling.", new List<string> { "Shield" })
                    }
                }
            ));

            predefinedRooms.Add("The Dark Vault", new Room(
                "The Dark Vault",
                "A dimly lit room with ancient, rusty vault doors, a sense of danger lurking.",
                new List<string> { "North", "East" },  //Available doors
                new Dictionary<string, PointOfInterest>
                {
                    {
                        "Vault",
                        new PointOfInterest("Vault", "A heavy vault door that looks almost impossible to open.", new List<string> { "Key" })
                    },
                    {
                        "Safe",
                        new PointOfInterest("Safe", "A small metal safe, buried beneath a pile of rubble.", new List<string> { "Gold" })
                    }
                }
            ));

            predefinedRooms.Add("The Abandoned Workshop", new Room(
                "The Abandoned Workshop",
                "A workshop littered with broken tools, cobwebs, and old blueprints.",
                new List<string> { "West", "South" },  //Available doors
                new Dictionary<string, PointOfInterest>
                {
                    {
                        "Workbench",
                        new PointOfInterest("Workbench", "A cluttered workbench with various tools scattered about.", new List<string> { "Wrench" })
                    },
                    {
                        "Toolbox",
                        new PointOfInterest("Toolbox", "A rusty toolbox containing a variety of old tools.", new List<string> { "Nails" })
                    }
                }
            ));

            predefinedRooms.Add("The Wretched Tomb", new Room(
                "The Wretched Tomb",
                "A dark tomb with crumbling stone walls and the stench of decay.",
                new List<string> { "North" },  //Dead-end room, only North, no exit
                new Dictionary<string, PointOfInterest>
                {
                    {
                        "Coffin",
                        new PointOfInterest("Coffin", "An ancient coffin, sealed shut with a rusted lock.", new List<string> { "Ring" })
                    },
                    {
                        "Skull",
                        new PointOfInterest("Skull", "A skull resting on a stone pedestal.", new List<string> { "Bone" })
                    }
                }
            ));

            predefinedRooms.Add("The Escape Room", new Room(
                "The Escape Room",
                "A brightly lit room with an open door, leading to your freedom.",
                new List<string> { "West" },  //Available door (Escape!)
                new Dictionary<string, PointOfInterest>
                {
                    {
                        "Exit Door",
                        new PointOfInterest("Exit Door", "A large wooden door, easily opened from the inside.", new List<string> { "Exit" })
                    }
                }
            ));

            //Dead-end room with no exit, just a room with a blocked path
            predefinedRooms.Add("The Forgotten Passage", new Room(
                "The Forgotten Passage",
                "A dark, narrow passage with a dead end. The walls are cold and damp, and there seems to be no way out.",
                new List<string> { "South" },  //Dead-end room
                new Dictionary<string, PointOfInterest>
                {
                    {
                        "Dead End",
                        new PointOfInterest("Dead End", "A dead end with a solid stone wall. There is no way forward.", new List<string> { })
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