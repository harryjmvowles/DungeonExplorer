using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DungeonExplorer.Weapon;

namespace DungeonExplorer
{
    // Creates Room Class
    public class Room
    {
        // Static dictionary to cache all created rooms
        public static Dictionary<string, Room> RoomCache = new Dictionary<string, Room>();
        public static Room currentRoom; // Current room the player is in

        // Room properties
        public string Name;
        public string Description;
        public List<Item> Items;
        public Dictionary<string, PointOfInterest> PointsOfInterest;
        public Dictionary<string, Door> Exits;
        public bool BeenHere;

        // Room Constructor accepting PointsOfInterest and Exits
        public Room(string name, string description, List<Item> items, Dictionary<string, PointOfInterest> pointsOfInterest, Dictionary<string, Door> exits)
        {
            Name = name;
            Description = description;
            Items = items ?? new List<Item>();
            PointsOfInterest = pointsOfInterest;
            Exits = exits;
            BeenHere = false;
        }

        // Method to add an exit door to the room
        public void AddExit(string direction, Door door)
        {
            Exits[direction] = door;
        }

        // When the player enters the room
        public void Enter()
        {
            if (!BeenHere)
            {
                // Display the room details
                Console.Clear();
                Console.WriteLine("You enter a new room... it is " + Name);
                Console.WriteLine(Description);
                DisplayItems();
                DisplayPointsOfInterest();

                // The player has now been in this room
                BeenHere = true;
            }
            else
            {
                // Display a generic message
                Console.Clear();
                Console.WriteLine("You are back in the " + Name);
                Console.WriteLine(Description);
                DisplayItems();
                DisplayPointsOfInterest();
            }

            Console.WriteLine("");
            Console.WriteLine("");
            ProcessRoomActions(GameManager.Instance.CurrentPlayer);
        }

        // Process actions for the room
        public void ProcessRoomActions(Player currentPlayer)
        {
            string command;
            do
            {
                // Show available actions + reshow description
                Console.WriteLine(" ______  __  __  ______  ______  ______  ______    \r\n/\\  ___\\/\\ \\_\\ \\/\\  __ \\/\\  __ \\/\\  ___\\/\\  ___\\   \r\n\\ \\ \\___\\ \\  __ \\ \\ \\/\\ \\ \\ \\/\\ \\ \\___  \\ \\  __\\   \r\n \\ \\_____\\ \\_\\ \\_\\ \\_____\\ \\_____\\/\\_____\\ \\_____\\ \r\n _\\/_____/\\/_/\\/_/\\/_____/\\/_____/\\/_____/\\/_____/ \r\n/\\  __ \\/\\  == \\/\\__  _\\/\\ \\/\\  __ \\/\\ \"-.\\ \\      \r\n\\ \\ \\/\\ \\ \\  _-/\\/_/\\ \\/\\ \\ \\ \\ \\/\\ \\ \\ \\-.  \\     \r\n \\ \\_____\\ \\_\\     \\ \\_\\ \\ \\_\\ \\_____\\ \\_\\\\\"\\_\\    \r\n  \\/_____/\\/_/      \\/_/  \\/_/\\/_____/\\/_/ \\/_/    \n");
                Console.WriteLine("1. View Room description");
                Console.WriteLine("2. View inventory/Health Status");
                Console.WriteLine("3. Interact with a point of interest");
                Console.WriteLine("4. Pick up an item");
                Console.WriteLine("5. Try a door");
                Console.WriteLine("6. Give Up! (END GAME)");

                // Read player input
                command = Console.ReadLine();

                switch (command)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine(Name);
                        Console.WriteLine(Description);  // Show room description
                        DisplayItems();
                        DisplayPointsOfInterest();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Clear();
                        currentPlayer.ViewInventory();  // Show inventory
                        Console.Clear();
                        break;
                    case "3":
                        Console.Clear();
                        DisplayPointsOfInterest();
                        Console.WriteLine("Which point of interest would you like to interact with?");
                        string poi = Console.ReadLine();
                        InteractWithPointOfInterest(poi, currentPlayer);  // Interact with a point of interest
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "4":
                        Console.Clear();
                        PickUpItem(currentPlayer);  // Pick up an item 
                        break;
                    case "5":
                        Console.Clear();
                        TryDoor(currentPlayer, GameManager.Instance.RoomManager);  // Try a door
                        break;
                    case "6":
                        Console.WriteLine("You have opted to end the game early, better luck next time! Thank you for playing."); // Early exit button
                        Console.WriteLine("Press any key to continue...");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid command. Please try again.");
                        Console.WriteLine("Press any key to continue...");
                        Console.Clear();
                        break;
                }
            } while (command != "6");  // Exit the room loop when the player chooses to leave
        }

        // Add an item to the room
        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        // Remove an item from the room
        public void RemoveItem(Item item)
        {
            Items.Remove(item);
        }

        // Display the items in the room
        public void DisplayItems()
        {
            var validItems = Items.Where(item => item != null && !string.IsNullOrWhiteSpace(item.Name)).ToList(); // Ignore null or items without names

            if (validItems.Count == 0)
            {
                Console.WriteLine("Items: N/A"); // No items in the room
            }
            else if (validItems.Count == 1)
            {
                Console.WriteLine("Item in the room: " + validItems[0].Name); // Only one item
            }
            else
            {
                Console.WriteLine("Items in the room:");
                foreach (Item item in validItems)
                {
                    Console.WriteLine("- " + item.Name); // Display the name of each item
                }
            }
        }

        // Display points of interest (like Desk, Bookshelf, etc)
        public void DisplayPointsOfInterest()
        {
            if (PointsOfInterest.Count > 0)
            {
                Console.WriteLine("Points of interest in the room:");
                foreach (var poi in PointsOfInterest)
                {
                    Console.WriteLine($"- {poi.Key}: {poi.Value.Description}");  // Display the point of interest and its description
                }
            }
        }

        private void InteractWithPointOfInterest(string poiName, Player currentPlayer)
        {
            string matchedPoiKey = PointsOfInterest.Keys.FirstOrDefault(k => k.Equals(poiName, StringComparison.OrdinalIgnoreCase));
            if (matchedPoiKey != null && PointsOfInterest.TryGetValue(matchedPoiKey, out PointOfInterest point))
            {
                Console.WriteLine($"You interact with the {matchedPoiKey}. {point.Description}");

                // Special logic for Exit Door in Escape Room
                if (matchedPoiKey.Equals("Exit Door", StringComparison.OrdinalIgnoreCase) && currentPlayer.CurrentRoom.Name == "The Escape Room")
                {
                    if (currentPlayer.HasGoldenKey)
                    {
                        Console.Clear();
                        Console.WriteLine("You insert the Golden Key into the Exit Door...");
                        Console.WriteLine("The door creaks open and sunlight floods the room.");
                        Console.WriteLine("Press any key to escape the dungeon...");
                        Console.ReadKey();
                        GameManager.GameOver(true); // Call GameOver method with true to indicate success
                    }
                    else
                    {
                        Console.WriteLine("The door is locked. It looks like it requires a special golden key...");
                        Console.WriteLine("You need to find the Golden Key before you can escape.");
                    }

                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                // Display items at the POI
                Console.WriteLine("Items here:");
                for (int i = 0; i < point.Items.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {point.Items[i].Name}");
                }

                Console.WriteLine("Would you like to pick up an item from here? (yes/no)");
                string choice = Console.ReadLine().ToLower();
                if (choice == "yes")
                {
                    Console.WriteLine("Which item would you like to pick up? (Enter the number)");

                    int itemIndex = -1;
                    while (itemIndex < 0 || itemIndex >= point.Items.Count)
                    {
                        string input = Console.ReadLine();
                        if (int.TryParse(input, out itemIndex) && itemIndex >= 1 && itemIndex <= point.Items.Count)
                        {
                            itemIndex--;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid number. Please choose a valid number from the list.");
                        }
                    }

                    Item matchedItem = point.Items[itemIndex];
                    if (matchedItem != null)
                    {
                        if (matchedItem is Key)
                        {
                            currentPlayer.AddKey(1);
                        }
                        else if (matchedItem is GoldenKey)
                        {
                            currentPlayer.AddGoldenKey();
                        }
                        else if (matchedItem is Potion)
                        {
                            currentPlayer.AddPotion(1);
                        }
                        else
                        {
                            currentPlayer.AddToInventory(matchedItem);
                        }

                        point.Items.Remove(matchedItem);
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("You chose not to pick up any items.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("That point of interest doesn't exist in this room.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }


        // Method for picking up an item in the room (by number)
        public void PickUpItem(Player currentPlayer)
        {
            if (Items.Count == 0)
            {
                Console.WriteLine("There are no items in this room.");
                return;
            }

            Console.WriteLine("Available items to pick up:");

            // Display available items in the room with a number list
            for (int i = 0; i < Items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Items[i].Name}");
            }

            // Ask the player which item they would like to pick up
            Console.WriteLine("Enter the number of the item you want to pick up:");
            int itemIndex = -1;

            // Ensure player input is valid
            while (itemIndex < 0 || itemIndex >= Items.Count)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out itemIndex) && itemIndex >= 1 && itemIndex <= Items.Count)
                {
                    itemIndex--; // Adjust for zero-based index
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid number. Please choose a valid item number from the list.");
                }
            }

            Item selectedItem = Items[itemIndex];
            Items.Remove(selectedItem);  // Remove item from room

            // Process the item (add to inventory, potion, key, etc.)
            if (selectedItem is Key)
            {
                currentPlayer.AddKey(1);
            }
            else if (selectedItem is Potion)
            {
                currentPlayer.AddPotion(1);
            }
            else
            {
                currentPlayer.AddToInventory(selectedItem);  // Add other items to inventory
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        // Try a door method
        public void TryDoor(Player currentPlayer, RoomManager roomManager)
        {
            Room currentRoom = currentPlayer.CurrentRoom;

            Console.WriteLine("Available directions:");
            foreach (var direction in currentRoom.Exits.Keys)
            {
                Console.WriteLine("- " + direction);
            }

            Console.WriteLine("Which direction would you like to try?");
            string chosenDirection1 = Console.ReadLine();
            string chosenDirection = StringManipulator.ToUpperFirstLetter(chosenDirection1);

            if (currentRoom.Exits.TryGetValue(chosenDirection, out Door door))
            {
                if (door.IsLocked)
                {
                    Console.WriteLine("The door is locked. Try using a key? (yes/no)");
                    string useKey = Console.ReadLine().ToLower();

                    if (useKey == "yes")
                    {
                        if (currentPlayer.Keys > 0)
                        {
                            currentPlayer.UseKey(); // Use a key
                            if (door.TryUnlock()) // Unlock the door
                            {
                                Console.WriteLine("You unlocked the door!");
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                                Console.Clear();

                                string otherRoomName = door.GetOtherSide(currentRoom.Name); // Get the other room using bidirectional link
                                Room nextRoom = roomManager.GetOrCreateRoom(otherRoomName); // Load the other room
                                currentPlayer.CurrentRoom = nextRoom;
                                Encounter.BasicEncounter(); // Start a basic encounter
                                nextRoom.Enter();
                            }
                        }
                        else
                        {
                            Console.WriteLine("You have no keys.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            Console.Clear();
                        }
                    }
                    else
                    {
                        Console.WriteLine("You back away from the door. (No or Incorrect Input)");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                else
                {
                    Console.WriteLine("You open the door...");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();

                    string otherRoomName = door.GetOtherSide(currentRoom.Name); // Get the other room using bidirectional link
                    Room nextRoom = roomManager.GetOrCreateRoom(otherRoomName); // Load the other room
                    currentPlayer.CurrentRoom = nextRoom;
                    Encounter.BasicEncounter(); // Start a basic encounter
                    nextRoom.Enter();
                }
            }
            else
            {
                Console.WriteLine("There's no door in that direction.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
