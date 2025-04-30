using DungeonExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DungeonExplorer
{
    // RoomManager class to manage predefined rooms and their connections
    public class RoomManager
    {
        private Dictionary<string, Room> predefinedRooms; // Dictionary to store predefined rooms

        public RoomManager()
        {
            predefinedRooms = new Dictionary<string, Room>();
            // Create rooms
            predefinedRooms.Add("The Lost Hall", new Room("The Lost Hall", "A dark room with decaying stone walls and a rotten wooden floor.",
                new List<Item> { },
                new Dictionary<string, PointOfInterest> {
        { "Desk", new PointOfInterest("Desk", "A sturdy wooden desk with a drawer.", new List<Item> { new Key() }) },
        { "Chest", new PointOfInterest("Chest", "An old chest with rusted hinges.", new List<Item> { new Potion() }) }
                }, new Dictionary<string, Door>()));

            predefinedRooms.Add("The Forgotten Chamber", new Room("The Forgotten Chamber", "A musty chamber filled with the scent of mildew...",
                 new List<Item> { ItemDatabase.Items["Potion"] },
                new Dictionary<string, PointOfInterest> {
        { "Bookshelf", new PointOfInterest("Bookshelf", "A dusty bookshelf covered in cobwebs.", new List<Item> { ItemDatabase.Items["Leather Armor"] }) },
        { "Statue", new PointOfInterest("Statue", "A broken statue missing its head.", new List<Item> { new Key() }) }
                }, new Dictionary<string, Door>()));

            predefinedRooms.Add("The Silent Corridor", new Room("The Silent Corridor", "A narrow hallway filled with an eerie silence.",
                new List<Item> { },
                new Dictionary<string, PointOfInterest> {
        { "Portrait", new PointOfInterest("Portrait", "A faded painting of an unknown noble.", new List<Item> { ItemDatabase.Items["Rusty Sword"] }) },
        { "Candles", new PointOfInterest("Candles", "A few candles flicker weakly.", new List<Item> { new Key() }) }
                }, new Dictionary<string, Door>()));

            predefinedRooms.Add("The Grand Hall", new Room("The Grand Hall", "A majestic hall with towering stone pillars.",
                new List<Item> { ItemDatabase.Items["Potion"] },
                new Dictionary<string, PointOfInterest> {
        { "Throne", new PointOfInterest("Throne", "An ancient throne encrusted with faded jewels.", new List<Item> { ItemDatabase.Items["Chainmail Armor"] }) },
        { "Pillars", new PointOfInterest("Pillars", "Massive cracked pillars line the hall.", new List<Item> {}) }
                }, new Dictionary<string, Door>()));

            predefinedRooms.Add("The Dark Vault", new Room("The Dark Vault", "A dimly lit, claustrophobic room with thick air.",
                new List<Item> { ItemDatabase.Items["Steel Armor"] },
                new Dictionary<string, PointOfInterest> {
        { "Vault", new PointOfInterest("Vault", "A heavy metal vault door slightly ajar.", new List<Item> { new Key() }) },
        { "Safe", new PointOfInterest("Safe", "A small rusted safe hidden behind a wall panel.", new List<Item> { ItemDatabase.Items["Battle Axe"] }) }
                }, new Dictionary<string, Door>()));

            predefinedRooms.Add("The Abandoned Workshop", new Room("The Abandoned Workshop", "A workshop littered with shattered tools and broken dreams.",
                new List<Item> { },
                new Dictionary<string, PointOfInterest> {
        { "Workbench", new PointOfInterest("Workbench", "A cluttered workbench with strange tools.", new List<Item> { ItemDatabase.Items["Dragon Scale Armor"] }) },
        { "Toolbox", new PointOfInterest("Toolbox", "An old toolbox missing its handle.", new List<Item> { new Potion() }) }
                }, new Dictionary<string, Door>()));

            predefinedRooms.Add("The Wretched Tomb", new Room("The Wretched Tomb", "A dark tomb filled with a heavy sense of loss.",
                new List<Item> { },
                new Dictionary<string, PointOfInterest> {
        { "Coffin", new PointOfInterest("Coffin", "An ancient coffin sealed with wax.", new List<Item> { new Key() }) },
        { "Skull", new PointOfInterest("Skull", "A lone skull resting atop a cracked pedestal.", new List<Item> { ItemDatabase.Items["Enchanted Dagger"] }) }
                }, new Dictionary<string, Door>()));

            predefinedRooms.Add("The Forgotten Passage", new Room("The Forgotten Passage", "A dark, narrow passage that seems endless.",
                new List<Item> { ItemDatabase.Items["Greatsword"] },
                new Dictionary<string, PointOfInterest> {
        { "Dead End", new PointOfInterest("Dead End", "A stone wall blocking your way. A pile of rubble infront crushing chests overflowing with gold.", new List<Item>{new GoldenKey() }) }
                }, new Dictionary<string, Door>()));

            predefinedRooms.Add("The Escape Room", new Room("The Escape Room", "A brightly lit room signaling freedom.",
                new List<Item> { },
                new Dictionary<string, PointOfInterest> {
        { "Exit Door", new PointOfInterest("Exit Door", "A massive reinforced door that leads outside.", new List<Item> {}) }
                }, new Dictionary<string, Door>()));

            // Initialize bidirectional doors
            InitializeBidirectionalDoors();
        }
        // predefined Bidirectional doors
        public void InitializeBidirectionalDoors()
        {
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

        // Method to get a room by name
        public Room GetRoom(string name)
        {
            return predefinedRooms.TryGetValue(name, out var room) ? room : null;
        }

        // Method to get or create a room
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

