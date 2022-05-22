using TimeLibrary;
using System;
using System.Threading;

namespace TimeApp
{
    internal class Clock : Program
    {
        /// <summary>
        /// Initiates Clock app
        /// </summary>
        public static void Start()
        {
            Console.Clear();
            while (true)
            {
                DrawTime(new Time(DateTime.Now.ToString("HH:mm:ss")).ToString());
                Console.WriteLine("Press ESC to exit program.");
                Thread.Sleep(100);
                _ = TaskConsoleKey();
                if (consoleKey == ConsoleKey.Escape)
                {
                    consoleKey = default;
                    Console.CursorVisible = true;
                    return;
                }
            }
        }
    }
}
