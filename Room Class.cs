using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    //Creates Room Class
    public class Room
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Items { get; set; }
        public bool BeenHere = false;
        public Dictionary<string, PointOfInterest> PointsOfInterest { get; set; }
        public Dictionary<string, string> Exits { get; set; }
        public Door RoomDoor { get; set; }

        public Room(string name, string description, List<string> items = null, Dictionary<string, PointOfInterest> pointsOfInterest = null, Door door = null)
        {
            Name = name;
            Description = description;
            Items = items ?? new List<string>();
            PointsOfInterest = pointsOfInterest ?? new Dictionary<string, PointOfInterest>(StringComparer.OrdinalIgnoreCase);
            Exits = new Dictionary<string, string>();
            RoomDoor = door ?? new Door();
            BeenHere = false;
        }


        //When the player enters the room
        public void Enter()
        {
            //If the player has not been here before
            if (!BeenHere)
            {
                //Display the room details
                Console.WriteLine("You enter a new room... it is clearly " + Name);
                Console.WriteLine(Description);
                DisplayItems();
                DisplayPointsOfInterest();

                //The player has now been in this room
                BeenHere = true;
            }
            //If the player has been here before
            else
            {
                //Display a generic message
                Console.WriteLine("You are back in the " + Name);
                Console.WriteLine(Description);
                DisplayItems();
                DisplayPointsOfInterest();
            }

            Console.WriteLine("");
            Console.WriteLine("             ----                ");
            Console.WriteLine("");
            ProcessRoomActions(Program.currentPlayer);
        }


        //Process actions for the room
        public void ProcessRoomActions(Player currentPlayer)
        {
            string command;
            do
            {
                //Show available actions + reshow description
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("1. View Room description again");
                Console.WriteLine("2. View inventory/Health Status");
                Console.WriteLine("3. Interact with a point of interest");
                Console.WriteLine("4. Pick up an item");
                Console.WriteLine("5. Go to the door");
                Console.WriteLine("6. Exit the room / end game!");


                //Read player input
                command = Console.ReadLine();

                switch (command)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine(Description);  //Show room description
                        DisplayItems();
                        DisplayPointsOfInterest();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case "2":
                        currentPlayer.ViewInventory();  //Show inventory
                        Console.Clear();
                        break;
                    case "3":
                        Console.Clear();
                        DisplayPointsOfInterest();
                        Console.WriteLine("Which point of interest would you like to interact with?");
                        string poi = Console.ReadLine();
                        InteractWithPointOfInterest(poi, currentPlayer);  //Interact with a point of interest
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "4":
                        Console.WriteLine("Which item would you like to pick up?");
                        string item = Console.ReadLine();
                        PickUpItem(item, currentPlayer);  //Pick up an item
                        break;
                    case "5":
                        Console.WriteLine("You try the door but its broken, you are going to have to find another way out.");  //Check door status (locked/unlocked) Not Implemented yet!
                        break;
                    case "6":
                        Console.WriteLine("This feature has not been added yet and so you have opted to end the game! Thankyou for playing."); //Exit the room Not Implemented yet so this is the end game button.
                        Console.WriteLine("Press any key to continue...");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid command. Please try again.");
                        break;
                }
            } while (command != "6");  //Exit the room loop when the player chooses to leave
        }


        //Add an item to the room
        public void AddItem(string item)
        {
            Items.Add(item);
        }

        //Remove an item from the room
        public void RemoveItem(string item)
        {
            Items.Remove(item);
        }


        //Display the items in the room
        public void DisplayItems()
        {
            //Check if there is exactly one item in the list
            if (Items.Count == 1)
            {
                //If that item is empty, display "Items: N/A"
                if (string.IsNullOrEmpty(Items[0]))
                {
                    Console.WriteLine("Items: N/A");
                }
                else
                {
                    Console.WriteLine("Item in the room: " + Items[0]);
                }
            }
            //If there are more than one item
            else if (Items.Count > 1)
            {
                Console.WriteLine("Items in the room:");
                foreach (string item in Items)
                {
                    Console.WriteLine("- " + item);
                }
            }
            //If there are no items in the room
            else
            {
                Console.WriteLine("No items in the room.");
            }
        }


        //Display points of interest (like Desk, Bookshelf, etc)
        public void DisplayPointsOfInterest()
        {
            if (PointsOfInterest.Count > 0)
            {
                Console.WriteLine("Points of interest in the room:");
                foreach (var poi in PointsOfInterest)
                {
                    Console.WriteLine($"- {poi.Key}: {poi.Value.Description}");  //Display the point of interest and its description
                }
            }
        }


        //Interact with points of interest
        private void InteractWithPointOfInterest(string poiName, Player currentPlayer)
        {
            string matchedPoiKey = PointsOfInterest.Keys.FirstOrDefault(k => k.Equals(poiName, StringComparison.OrdinalIgnoreCase));
            if (matchedPoiKey != null && PointsOfInterest.TryGetValue(matchedPoiKey, out PointOfInterest point))
            {
                Console.WriteLine($"You interact with the {matchedPoiKey}. {point.Description}"); //If the point of interest exists in the room
                Console.WriteLine("Items here:");
                foreach (var item in point.Items)
                {
                    Console.WriteLine("- " + item);  //Display items in the point of interest
                }

                Console.WriteLine("Would you like to pick up an item from here? (yes/no)");
                string choice = Console.ReadLine().ToLower();
                if (choice == "yes")
                {
                    Console.WriteLine("Which item would you like to pick up?");
                    string itemToPick = Console.ReadLine();
                    string foundItem = point.Items.FirstOrDefault(i => i.Equals(itemToPick, StringComparison.OrdinalIgnoreCase)); //Ignores case when comparing

                    if (!string.IsNullOrEmpty(foundItem)) //If the item is in the point of interest
                    {
                        Console.WriteLine($"You pick up the {foundItem}.");
                        point.RemoveItem(foundItem);
                        currentPlayer.AddToInventory(foundItem);  
                    }
                    else
                    {
                        Console.WriteLine("That item is not available at this point of interest.");
                    }
                }
            }
            else
            {
                Console.WriteLine("That point of interest doesn't exist in this room.");    
            }
        }


        //Pick up an item from the room
        public void PickUpItem(string item, Player currentPlayer)
        {
            string foundItem = Items.FirstOrDefault(i => i.Equals(item, StringComparison.OrdinalIgnoreCase));  //Ignores case when comparing
            if (!string.IsNullOrEmpty(foundItem))
            {
                Console.WriteLine($"You pick up the {foundItem}."); //If the item is in the room
                Items.Remove(foundItem);
                currentPlayer.AddToInventory(foundItem);
            }
            else
            {
                Console.WriteLine("That item is not in the room."); //If the item is not in the room
                Console.ReadKey();
                Console.Clear();
            }
        }
    }

}