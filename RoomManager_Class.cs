using DungeonExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DungeonExplorer
{
    // RoomManager handles predefined rooms and provides them when needed
    public class RoomManager
    {
        private Dictionary<string, Room> predefinedRooms;
        public RoomManager()
        {
            predefinedRooms = new Dictionary<string, Room>();

            // Create rooms
            predefinedRooms.Add("The Lost Hall", new Room("The Lost Hall", "A dark room with decaying stone walls and a rotten wooden floor.", new List<string> { "" }, new Dictionary<string, PointOfInterest> { { "Desk", new PointOfInterest("Desk", "A sturdy wooden desk with a drawer.", new List<string> { "Key" }) }, { "Chest", new PointOfInterest("Chest", "A small wooden chest that is slightly cracked open.", new List<string> { "Torch" }) } }, new Dictionary<string, Door>()));
            predefinedRooms.Add("The Forgotten Chamber", new Room("The Forgotten Chamber", "A musty chamber filled with the scent of mildew...", new List<string> { "Key" }, new Dictionary<string, PointOfInterest> { { "Bookshelf", new PointOfInterest("Bookshelf", "A dusty bookshelf...", new List<string> { "Potion" }) }, { "Statue", new PointOfInterest("Statue", "A broken statue...", new List<string> { "Hammer" }) } }, new Dictionary<string, Door>()));
            predefinedRooms.Add("The Silent Corridor", new Room("The Silent Corridor", "A narrow hallway...", new List<string> { "" }, new Dictionary<string, PointOfInterest> { { "Portrait", new PointOfInterest("Portrait", "A painting...", new List<string> { "Sword" }) }, { "Candles", new PointOfInterest("Candles", "A set of candles...", new List<string> { "Key" }) } }, new Dictionary<string, Door>()));
            predefinedRooms.Add("The Grand Hall", new Room("The Grand Hall", "A majestic hall...", new List<string> { "" }, new Dictionary<string, PointOfInterest> { { "Throne", new PointOfInterest("Throne", "An old throne...", new List<string> { "Crown" }) }, { "Pillars", new PointOfInterest("Pillars", "Massive stone pillars...", new List<string> { "Shield" }) } }, new Dictionary<string, Door>()));
            predefinedRooms.Add("The Dark Vault", new Room("The Dark Vault", "A dimly lit room...", new List<string> { "" }, new Dictionary<string, PointOfInterest> { { "Vault", new PointOfInterest("Vault", "A heavy vault door...", new List<string> { "Key" }) }, { "Safe", new PointOfInterest("Safe", "A small metal safe...", new List<string> { "Gold" }) } }, new Dictionary<string, Door>()));
            predefinedRooms.Add("The Abandoned Workshop", new Room("The Abandoned Workshop", "A workshop littered with broken tools...", new List<string> { "" }, new Dictionary<string, PointOfInterest> { { "Workbench", new PointOfInterest("Workbench", "A cluttered workbench...", new List<string> { "Wrench" }) }, { "Toolbox", new PointOfInterest("Toolbox", "A rusty toolbox...", new List<string> { "Nails" }) } }, new Dictionary<string, Door>()));
            predefinedRooms.Add("The Wretched Tomb", new Room("The Wretched Tomb", "A dark tomb...", new List<string> { "" }, new Dictionary<string, PointOfInterest> { { "Coffin", new PointOfInterest("Coffin", "An ancient coffin...", new List<string> { "Ring" }) }, { "Skull", new PointOfInterest("Skull", "A skull resting...", new List<string> { "Key" }) } }, new Dictionary<string, Door>()));
            predefinedRooms.Add("The Escape Room", new Room("The Escape Room", "A brightly lit room...", new List<string> { "" }, new Dictionary<string, PointOfInterest> { { "Exit Door", new PointOfInterest("Exit Door", "A large wooden door...", new List<string> { "Exit" }) } }, new Dictionary<string, Door>()));
            predefinedRooms.Add("The Forgotten Passage", new Room("The Forgotten Passage", "A dark, narrow passage...", new List<string> { "" }, new Dictionary<string, PointOfInterest> { { "Dead End", new PointOfInterest("Dead End", "A dead end...", new List<string>()) } }, new Dictionary<string, Door>()));

            // Initialize and link bidirectional doors
            AddBidirectionalDoor("The Lost Hall", "North", "The Forgotten Chamber", "South", false); // unlocked
            AddBidirectionalDoor("The Lost Hall", "East", "The Silent Corridor", "West", true);  // locked
            AddBidirectionalDoor("The Forgotten Chamber", "East", "The Grand Hall", "West", true);  // locked
            AddBidirectionalDoor("The Silent Corridor", "North", "The Grand Hall", "South", false); // unlocked
            AddBidirectionalDoor("The Silent Corridor", "East", "The Abandoned Workshop", "West", true);  // locked
            AddBidirectionalDoor("The Grand Hall", "North", "The Wretched Tomb", "South", false);  // unlocked
            AddBidirectionalDoor("The Dark Vault", "South", "The Abandoned Workshop", "North", true);  // locked
            AddBidirectionalDoor("The Wretched Tomb", "West", "The Forgotten Passage", "East", true);  // locked
            AddBidirectionalDoor("The Escape Room", "South", "The Dark Vault", "North", false); // unlocked
        }

        // Method to link bidirectional doors with lock state
        private void AddBidirectionalDoor(string roomA, string dirA, string roomB, string dirB, bool isLocked)
        {
            Door door = new Door($"{roomA} to {roomB}", isLocked, dirA, roomA, roomB);
            predefinedRooms[roomA].Exits[dirA] = door;
            predefinedRooms[roomB].Exits[dirB] = door;
        }

        public Room GetRoom(string name)
        {
            return predefinedRooms.TryGetValue(name, out var room) ? room : null;
        }

        public Room GetOrCreateRoom(string name)
        {
            if (Room.RoomCache.TryGetValue(name, out var existingRoom))
                return existingRoom;

            var room = GetRoom(name);
            if (room != null)
                Room.RoomCache[name] = room;

            return room;
        }
    }


}
