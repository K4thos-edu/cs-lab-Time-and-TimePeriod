using TimeLibrary;
using System;
using System.Threading;

namespace TimeApp
{
    internal class Stopwatch : Program
    {
        /// <summary>
        /// Initiates Stopwatch app
        /// </summary>
        public static void Start()
        {
            Console.Clear();
            var timePeriod = new TimePeriod();
            var running = false;
            while (true)
            {
                DrawTime(timePeriod.ToString());
                Console.WriteLine("Select what to do with timer\n" +
                    "1: Start\n" +
                    "2: Stop\n" +
                    "3: Reset\n");
                Console.WriteLine("Press ESC to exit program.");
                Thread.Sleep(1);
                if (running)
                {
                    timePeriod += new TimePeriod(0, 0, 0, 15);
                }
                _ = TaskConsoleKey();
                var key = consoleKey;
                consoleKey = default;
                switch (key)
                {
                    case ConsoleKey.Escape:
                        return;
                    case ConsoleKey.D1: // Start
                        running = true;
                        break;
                    case ConsoleKey.D2: // Stop
                        running = false;
                        break;
                    case ConsoleKey.D3: // Reset
                        timePeriod = new TimePeriod();
                        break;
                }
            }
        }
    }
}
