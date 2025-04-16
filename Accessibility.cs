using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
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
            Console.WriteLine("|| THE DUNGEON ||");
            Console.WriteLine("Hello Adventurer, Brave enough to enter The Dungeon I see...");
            Console.WriteLine("What is your name?: ");
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
            Console.WriteLine($"Well GoodLuck {CurrentPlayer.Name}! Take these.... You will need them.");
            CurrentPlayer.AddPotion(2);
            Console.WriteLine("Press Any Key to continue....");
            Console.ReadKey();
            Console.Clear();

            // Story intro
            Console.WriteLine("The mysterious man at the entrance of The Dungeon suddenly disappeared...");
            Console.WriteLine("You are now trapped in The Dungeon.... How will you escape?");
            Console.WriteLine("Press Any Key to continue....");
            Console.ReadKey();
            Console.Clear();

            // Start in the Lost Hall
            Room lostHall = RoomManager.GetRoom("The Lost Hall");
            CurrentPlayer.CurrentRoom = lostHall;

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
