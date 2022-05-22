using System;
using System.Text;
using System.Threading.Tasks;

namespace TimeApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Please select an app to run:\n" +
                "1: Clock\n" +
                "2: Stopwatch");

            switch (Console.ReadLine().Trim())
            {
                case "1": // Clock
                    Clock.Start();
                    return;
                case "2": // Stopwatch
                    Stopwatch.Start();
                    return;
            }
        }

        /// <summary>
        /// Draw LED numbers
        /// </summary>
        /// <param name="time">string to write</param>
        public static void DrawTime(string time)
        {
            var sb = new StringBuilder();
            var rows = new string[3];
            for (int i = 0; i < time.Length; i++)
            {
                string[] a;
                switch (time[i])
                {
                    case ':':
                        a = new string[9] { " ", " ", " ", " ", ".", " ", " ", ".", " " };
                        break;
                    case '0':
                        a = new string[9] { " ", "_", " ", "|", " ", "|", "|", "_", "|" };
                        break;
                    case '1':
                        a = new string[9] { " ", " ", " ", " ", " ", "|", " ", " ", "|" };
                        break;
                    case '2':
                        a = new string[9] { " ", "_", " ", " ", "_", "|", "|", "_", " " };
                        break;
                    case '3':
                        a = new string[9] { " ", "_", " ", " ", "_", "|", " ", "_", "|" };
                        break;
                    case '4':
                        a = new string[9] { " ", " ", " ", "|", "_", "|", " ", " ", "|" };
                        break;
                    case '5':
                        a = new string[9] { " ", "_", " ", "|", "_", " ", " ", "_", "|" };
                        break;
                    case '6':
                        a = new string[9] { " ", "_", " ", "|", "_", " ", "|", "_", "|" };
                        break;
                    case '7':
                        a = new string[9] { " ", "_", " ", " ", " ", "|", " ", " ", "|" };
                        break;
                    case '8':
                        a = new string[9] { " ", "_", " ", "|", "_", "|", "|", "_", "|" };
                        break;
                    case '9':
                        a = new string[9] { " ", "_", " ", "|", "_", "|", " ", " ", "|" };
                        break;
                    default:
                        return;
                }

                for (int j = 0; j < a.Length; j++)
                {
                    if (j < 3)
                    {
                        rows[0] += a[j];
                    }
                    else if (j < 6)
                    {
                        rows[1] += a[j];
                    }
                    else
                    {
                        rows[2] += a[j];
                    }
                }
            }
            foreach (var item in rows)
            {
                sb.AppendLine(item);
            }
            sb.UpdateConsole();
        }

        /// <summary>
        /// Asynchronous button detection
        /// </summary>
        public static volatile ConsoleKey consoleKey = default;
        public static async Task<ConsoleKey> TaskConsoleKey()
        {
            ConsoleKey key = default;
            await Task.Run(() => key = Console.ReadKey(true).Key);
            consoleKey = key;
            return key;
        }
    }

    public static class StringBuilderExtensions
    {
        /// <summary>
        /// Hides the console cursor, resets its position and writes out the string builder
        /// </summary>
        public static void UpdateConsole(this StringBuilder c)
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(c);
        }
    }
}
