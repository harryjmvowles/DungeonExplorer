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
        static void Main(string[] args)
        {
            // Debug testing
#if DEBUG
            Tests.RunTests(); // Call tests here
#endif

            // Start the game using GameManager
            GameManager.Instance.StartGame();
        }
    }


    // StringManipulator class to manipulate strings
    class StringManipulator
    {
        //Turns first letter of string to uppercase.
        public static string ToUpperFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return char.ToUpper(input[0]) + input.Substring(1);
        }
    }
    // GameManager class to manage game state and player
    public class GameManager
    {
        private static GameManager instance;
        public static GameManager Instance => instance ?? (instance = new GameManager());

        public Player CurrentPlayer { get; private set; }
        public RoomManager RoomManager { get; private set; }

        private GameManager()
        {
            RoomManager = new RoomManager();
        }

        // Start the game method
        public void StartGame()
        {
            Console.WriteLine(" /============================================================================\\ \r\n ||                                                                          || \r\n ||                                                                          || \r\n ||                                                                          || \r\n ||                                                                          || \r\n ||                                                                          || \r\n ||                         ████████╗██╗  ██╗███████╗                        || \r\n ||                         ╚══██╔══╝██║  ██║██╔════╝                        || \r\n ||                            ██║   ███████║█████╗                          || \r\n ||                            ██║   ██╔══██║██╔══╝                          || \r\n ||                            ██║   ██║  ██║███████╗                        || \r\n ||                            ╚═╝   ╚═╝  ╚═╝╚══════╝                        || \r\n ||                                                                          || \r\n ||     ██████╗ ██╗   ██╗███╗   ██╗ ██████╗ ███████╗ ██████╗ ███╗   ██╗      || \r\n ||     ██╔══██╗██║   ██║████╗  ██║██╔════╝ ██╔════╝██╔═══██╗████╗  ██║      || \r\n ||     ██║  ██║██║   ██║██╔██╗ ██║██║  ███╗█████╗  ██║   ██║██╔██╗ ██║      || \r\n ||     ██║  ██║██║   ██║██║╚██╗██║██║   ██║██╔══╝  ██║   ██║██║╚██╗██║      || \r\n ||     ██████╔╝╚██████╔╝██║ ╚████║╚██████╔╝███████╗╚██████╔╝██║ ╚████║      || \r\n ||     ╚═════╝  ╚═════╝ ╚═╝  ╚═══╝ ╚═════╝ ╚══════╝ ╚═════╝ ╚═╝  ╚═══╝      || \r\n ||                                                                          || \r\n ||                      PRESS ANY KEY TO CONTINUE■■■■                       || \r\n ||                                                                          || \r\n ||                                                                          || \r\n ||                                                                          || \r\n \\============================================================================/ ");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("'Why what do have we here? A brave new adventurer willing to explore the dungeon!' \nA strange hooded Man perched outside the massive decaying door then spoke directly to you...");
            Console.WriteLine("\n'What is your name?': ");
            string playerName = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(playerName))
            {
                Console.Clear();
                Console.WriteLine("You must enter a name!");
                playerName = Console.ReadLine();
            }
            Console.Clear();

            // Initialize the player
            CurrentPlayer = new Player(playerName);
            Console.WriteLine($"'Well GoodLuck {CurrentPlayer.Name}!'\n'Take these.... You will need them.'");
            CurrentPlayer.AddPotion(2);
            CurrentPlayer.AddToInventory(ItemDatabase.Items["Dagger"]);
            Console.WriteLine("Press Any Key to continue....");
            Console.ReadKey();
            Console.Clear();

            // Story intro
            Console.WriteLine("The mysterious man at the entrance of The Dungeon suddenly disappeared...");
            Console.WriteLine("You are now trapped in The Dungeon, a hall of the lost.... How will you escape?");
            Console.WriteLine("Press Any Key to continue....");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("You feel deep in your gut that you are not alone in this dungeon... and equip your new Dagger just incase.");
            CurrentPlayer.UseItem("Dagger");
            Console.WriteLine("Press Any Key to continue....");
            Console.ReadKey();
            Console.Clear();

            // Start in the Lost Hall
            Room lostHall = RoomManager.GetRoom("The Lost Hall");
            CurrentPlayer.CurrentRoom = lostHall;

            if (lostHall != null)
            {
                Encounter.FirstEncounter(); // Call FirstEncounter from Encounters class
                lostHall.Enter();
            }
            else
            {
                Console.WriteLine("The Lost Hall room could not be found.");
            }
        }

        // Game over method
        public static void GameOver(bool won)
        {
            if (won == true)
            {
                Console.Clear();
                Console.WriteLine("  /$$$$$$                                                     /$$    \r\n /$$__  $$                                                   | $$    \r\n| $$  \\__/  /$$$$$$  /$$$$$$$   /$$$$$$   /$$$$$$  /$$$$$$  /$$$$$$  \r\n| $$       /$$__  $$| $$__  $$ /$$__  $$ /$$__  $$|____  $$|_  $$_/  \r\n| $$      | $$  \\ $$| $$  \\ $$| $$  \\ $$| $$  \\__/ /$$$$$$$  | $$    \r\n| $$    $$| $$  | $$| $$  | $$| $$  | $$| $$      /$$__  $$  | $$ /$$\r\n|  $$$$$$/|  $$$$$$/| $$  | $$|  $$$$$$$| $$     |  $$$$$$$  |  $$$$/\r\n \\______/  \\______/ |__/  |__/ \\____  $$|__/      \\_______/   \\___/  \r\n                               /$$  \\ $$                             \r\n                              |  $$$$$$/                             \r\n                               \\______/                              \r\n           /$$             /$$     /$$                               \r\n          | $$            | $$    |__/                               \r\n /$$   /$$| $$  /$$$$$$  /$$$$$$   /$$  /$$$$$$  /$$$$$$$   /$$$$$$$ \r\n| $$  | $$| $$ |____  $$|_  $$_/  | $$ /$$__  $$| $$__  $$ /$$_____/ \r\n| $$  | $$| $$  /$$$$$$$  | $$    | $$| $$  \\ $$| $$  \\ $$|  $$$$$$  \r\n| $$  | $$| $$ /$$__  $$  | $$ /$$| $$| $$  | $$| $$  | $$ \\____  $$ \r\n|  $$$$$$/| $$|  $$$$$$$  |  $$$$/| $$|  $$$$$$/| $$  | $$ /$$$$$$$/ \r\n \\______/ |__/ \\_______/   \\___/  |__/ \\______/ |__/  |__/|_______/  ");
                Console.WriteLine("You have completed the dungeon!");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                Environment.Exit(0);
            }
            else
            {
                Console.Clear();
                Console.WriteLine(" ██████╗  █████╗ ███╗   ███╗███████╗     \r\n██╔════╝ ██╔══██╗████╗ ████║██╔════╝     \r\n██║  ███╗███████║██╔████╔██║█████╗       \r\n██║   ██║██╔══██║██║╚██╔╝██║██╔══╝       \r\n╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗     \r\n ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝     \r\n ██████╗ ██╗   ██╗███████╗██████╗        \r\n██╔═══██╗██║   ██║██╔════╝██╔══██╗       \r\n██║   ██║██║   ██║█████╗  ██████╔╝       \r\n██║   ██║╚██╗ ██╔╝██╔══╝  ██╔══██╗       \r\n╚██████╔╝ ╚████╔╝ ███████╗██║  ██║       \r\n ╚═════╝   ╚═══╝  ╚══════╝╚═╝  ╚═╝       ");
                Console.WriteLine("You have perished in the dungeon!");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }
    }
}
