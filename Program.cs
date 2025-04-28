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
        Testing.RunTests();
#endif

            // Start the game using GameManager
            GameManager.Instance.StartGame();
        }
    }
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
            Console.WriteLine("Press Any Key to continue....");
            Console.ReadKey();
            Console.Clear();

            // Story intro
            Console.WriteLine("The mysterious man at the entrance of The Dungeon suddenly disappeared...");
            Console.WriteLine("You are now trapped in The Dungeon, a hall of the lost.... How will you escape?");
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
    }
}
