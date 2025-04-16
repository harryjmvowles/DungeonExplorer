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
}
