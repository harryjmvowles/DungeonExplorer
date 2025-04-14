using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace DungeonExplorer
{
    internal class Program
    {
        //Creates new player object (The User's Player), initialized later after getting the name.
        public static Player currentPlayer;

        static void Main(string[] args)
        {
            //Debug if in Debug mode
#if DEBUG
            Testing.RunTests();
#endif

            //Calls Start of Game
            Start();
        }

        //Start of Game
        static void Start()
        {
            //Name Enter
            Console.WriteLine("|| THE DUNGEON ||");
            Console.WriteLine("Hello Adventurer, Brave enough to enter The Dungeon I see...");
            Console.WriteLine("What is your name?: ");
            string playerName = Console.ReadLine();

            //Name Check
            while (string.IsNullOrWhiteSpace(playerName))
            {
                Console.Clear();
                Console.WriteLine("You must enter a name!");
                playerName = Console.ReadLine();
            }
            Console.Clear();

            //Initialize the player with the given name
            currentPlayer = new Player(playerName);

            //Introduces item system (via potions)
            Console.WriteLine($"Well GoodLuck {currentPlayer.Name}! Take these.... You will need them.");
            currentPlayer.AddPotion(2);
            Console.WriteLine("Press Any Key to continue....");
            Console.ReadKey();
            Console.Clear();

            //World Building... Start of Dungeon.
            Console.WriteLine("The mysterious man at the entrance of The Dungeon suddenly disappeared, leaving you to face the tall, rusted metal door.");
            Console.WriteLine("You slowly push the door open, creaking and grating of the metal echoing through the many chambers as you enter.");
            Console.WriteLine("The door slams behind you, a sort of magic sealing you within.");
            Console.WriteLine("You are now trapped in The Dungeon.... How will you escape?");
            Console.WriteLine("Press Any Key to continue....");
            Console.ReadKey();
            Console.Clear();

            //Initialize RoomManager to access predefined rooms
            RoomManager roomManager = new RoomManager();

            //Retrieve the 'Lost Hall' room from the RoomManager
            Room lostHall = roomManager.GetRoom("The Lost Hall");
            currentPlayer.CurrentRoom = lostHall; //Set the current room for the player


            //Now let the player enter the Lost Hall room
            if (lostHall != null)
            {
                lostHall.Enter();
            }
            else
            {
                Console.WriteLine("The Lost Hall room could not be found.");
            }
        }
    }
}
