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

        // Shared doors to prevent duplication and state reset
        private Door lostHallToForgottenChamber;
        private Door lostHallToSilentCorridor;
        private Door forgottenChamberToGrandHall;
        private Door silentCorridorToGrandHall;
        private Door silentCorridorToAbandonedWorkshop;
        private Door grandHallToWretchedTomb;
        private Door darkVaultToForgottenPassage;
        private Door darkVaultToEscapeRoom;
        private Door abandonedWorkshopToDarkVault;
        private Door wretchedTombToForgottenPassage;
        private Door wretchedTombToGrandHall;
        private Door forgottenPassageToWretchedTomb;
        private Door escapeRoomToDarkVault;

        //Constructor to initialize predefined rooms
        public RoomManager()
        {
            predefinedRooms = new Dictionary<string, Room>();

            // Initialize shared doors
            lostHallToForgottenChamber = new Door("The Lost Hall to The Forgotten Chamber", false) { LeadsTo = "The Forgotten Chamber" };
            lostHallToSilentCorridor = new Door("The Lost Hall to The Silent Corridor", true) { LeadsTo = "The Silent Corridor" };
            forgottenChamberToGrandHall = new Door("The Forgotten Chamber to The Grand Hall", true) { LeadsTo = "The Grand Hall" };
            silentCorridorToGrandHall = new Door("The Silent Corridor to The Grand Hall", false) { LeadsTo = "The Grand Hall" };
            silentCorridorToAbandonedWorkshop = new Door("The Silent Corridor to The Abandoned Workshop", true) { LeadsTo = "The Abandoned Workshop" };
            grandHallToWretchedTomb = new Door("The Grand Hall to The Wretched Tomb", false) { LeadsTo = "The Wretched Tomb" };
            darkVaultToForgottenPassage = new Door("The Dark Vault to The Forgotten Passage", true) { LeadsTo = "The Forgotten Passage" };
            darkVaultToEscapeRoom = new Door("The Dark Vault to The Escape Room", false) { LeadsTo = "The Escape Room" };
            abandonedWorkshopToDarkVault = new Door("The Abandoned Workshop to The Dark Vault", false) { LeadsTo = "The Dark Vault" };
            wretchedTombToForgottenPassage = new Door("The Wretched Tomb to The Forgotten Passage", false) { LeadsTo = "The Forgotten Passage" };
            wretchedTombToGrandHall = new Door("The Wretched Tomb to The Grand Hall", false) { LeadsTo = "The Grand Hall" };
            forgottenPassageToWretchedTomb = new Door("The Forgotten Passage to The Wretched Tomb", true) { LeadsTo = "The Wretched Tomb" };
            escapeRoomToDarkVault = new Door("The Escape Room to The Dark Vault", false) { LeadsTo = "The Dark Vault" };

            // Predefine rooms here
            predefinedRooms.Add("The Lost Hall", new Room(
                "The Lost Hall",
                "A dark room with decaying stone walls and a rotten wooden floor.",
                new List<string> { "" },
                new Dictionary<string, PointOfInterest>
                {
                    { "Desk", new PointOfInterest("Desk", "A sturdy wooden desk with a drawer.", new List<string> { "Key" }) },
                    { "Chest", new PointOfInterest("Chest", "A small wooden chest that is slightly cracked open.", new List<string> { "Torch" }) }
                },
                new Dictionary<string, Door>
                {
                    { "North", lostHallToForgottenChamber },
                    { "East", lostHallToSilentCorridor }
                }
            ));

            predefinedRooms.Add("The Forgotten Chamber", new Room(
                "The Forgotten Chamber",
                "A musty chamber filled with the scent of mildew, with vines creeping up the walls.",
                new List<string> { "Key" },
                new Dictionary<string, PointOfInterest>
                {
                    { "Bookshelf", new PointOfInterest("Bookshelf", "A dusty bookshelf filled with ancient, forgotten tomes.", new List<string> { "Potion" }) },
                    { "Statue", new PointOfInterest("Statue", "A broken statue of a long-forgotten hero.", new List<string> { "Hammer" }) }
                },
                new Dictionary<string, Door>
                {
                    { "South", lostHallToForgottenChamber },
                    { "East", forgottenChamberToGrandHall }
                }
            ));

            predefinedRooms.Add("The Silent Corridor", new Room(
                "The Silent Corridor",
                "A narrow hallway with an eerie silence, the air feels thick with tension.",
                new List<string> { "" },
                new Dictionary<string, PointOfInterest>
                {
                    { "Portrait", new PointOfInterest("Portrait", "A painting of an ancient family, their eyes seeming to follow you.", new List<string> { "Sword" }) },
                    { "Candles", new PointOfInterest("Candles", "A set of candles flickering in the cold air. The glint of light reflecting on something next to them.", new List<string> { "Key" }) }
                },
                new Dictionary<string, Door>
                {
                    { "North", silentCorridorToGrandHall },
                    { "East", silentCorridorToAbandonedWorkshop },
                    { "West", lostHallToSilentCorridor }
                }
            ));

            predefinedRooms.Add("The Grand Hall", new Room(
                "The Grand Hall",
                "A majestic hall with high vaulted ceilings and broken chandeliers.",
                new List<string> { "" },
                new Dictionary<string, PointOfInterest>
                {
                    { "Throne", new PointOfInterest("Throne", "An old throne, covered in dust and cobwebs.", new List<string> { "Crown" }) },
                    { "Pillars", new PointOfInterest("Pillars", "Massive stone pillars that support the hall's great ceiling.", new List<string> { "Shield" }) }
                },
                new Dictionary<string, Door>
                {
                    { "West", forgottenChamberToGrandHall },
                    { "South", silentCorridorToGrandHall },
                    { "North", grandHallToWretchedTomb }
                }
            ));

            predefinedRooms.Add("The Dark Vault", new Room(
                "The Dark Vault",
                "A dimly lit room with ancient, rusty vault doors, a sense of danger lurking.",
                new List<string> { "" },
                new Dictionary<string, PointOfInterest>
                {
                    { "Vault", new PointOfInterest("Vault", "A heavy vault door that looks almost impossible to open.", new List<string> { "Key" }) },
                    { "Safe", new PointOfInterest("Safe", "A small metal safe, buried beneath a pile of rubble.", new List<string> { "Gold" }) }
                },
                new Dictionary<string, Door>
                {
                    { "North", darkVaultToEscapeRoom },
                    { "South", darkVaultToForgottenPassage }
                }
            ));

            predefinedRooms.Add("The Abandoned Workshop", new Room(
                "The Abandoned Workshop",
                "A workshop littered with broken tools, cobwebs, and old blueprints.",
                new List<string> { "" },
                new Dictionary<string, PointOfInterest>
                {
                    { "Workbench", new PointOfInterest("Workbench", "A cluttered workbench with various tools scattered about.", new List<string> { "Wrench" }) },
                    { "Toolbox", new PointOfInterest("Toolbox", "A rusty toolbox containing a variety of old tools.", new List<string> { "Nails" }) }
                },
                new Dictionary<string, Door>
                {
                    { "West", silentCorridorToAbandonedWorkshop },
                    { "North", abandonedWorkshopToDarkVault }
                }
            ));

            predefinedRooms.Add("The Wretched Tomb", new Room(
                "The Wretched Tomb",
                "A dark tomb with crumbling stone walls and the stench of decay.",
                new List<string> { "" },
                new Dictionary<string, PointOfInterest>
                {
                    { "Coffin", new PointOfInterest("Coffin", "An ancient coffin, sealed shut with a rusted lock.", new List<string> { "Ring" }) },
                    { "Skull", new PointOfInterest("Skull", "A skull resting on a stone pedestal. Inside something shiny.", new List<string> { "Key" }) }
                },
                new Dictionary<string, Door>
                {
                    { "North", wretchedTombToGrandHall },
                    { "West", wretchedTombToForgottenPassage }
                }
            ));

            predefinedRooms.Add("The Escape Room", new Room(
                "The Escape Room",
                "A brightly lit room with an open door, leading to your freedom.",
                new List<string> { "" },
                new Dictionary<string, PointOfInterest>
                {
                    { "Exit Door", new PointOfInterest("Exit Door", "A large wooden door, easily opened from the inside.", new List<string> { "Exit" }) }
                },
                new Dictionary<string, Door>
                {
                    { "West", escapeRoomToDarkVault }
                }
            ));

            predefinedRooms.Add("The Forgotten Passage", new Room(
                "The Forgotten Passage",
                "A dark, narrow passage with a dead end. The walls are cold and damp, and there seems to be no way out.",
                new List<string> { "" },
                new Dictionary<string, PointOfInterest>
                {
                    { "Dead End", new PointOfInterest("Dead End", "A dead end with a solid stone wall. There is no way forward.", new List<string> { }) }
                },
                new Dictionary<string, Door>
                {
                    { "East", forgottenPassageToWretchedTomb },
                    { "North", darkVaultToForgottenPassage }
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

        //Method to get a room or auto-create one if it doesn't exist
        public Room GetOrCreateRoom(string roomName)
        {
            if (!predefinedRooms.ContainsKey(roomName))
            {
                Room newRoom = new Room(
                    roomName,
                    "An uncharted room that seems newly formed, with minimal features.",
                    new List<string> { "" },
                    new Dictionary<string, PointOfInterest>
                    {
                        { "Dusty Corner", new PointOfInterest("Dusty Corner", "Just a pile of dust. Might be something buried underneath.", new List<string> { }) }
                    },
                    new Dictionary<string, Door>()
                );

                predefinedRooms.Add(roomName, newRoom);
            }

            return predefinedRooms[roomName];
        }
    }
}
