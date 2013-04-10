using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Everdawn
{
    class Text
    {
        public static string prompt = "# ";
        public static void SetPrompt()
        {
            prompt = "[ " + World.map[Player.location].name + " ] # ";
        }

        /// <summary>
        /// Specific prompt
        /// </summary>
        /// <returns></returns>
        public static string Prompt(string aText)
        {
            Console.WriteLine("\n"+aText);
            Console.Write(prompt);
            return Console.ReadLine();
        }
        public static void BlankLines(int num = 1)
        {
            string temp = "";
            for (int i = 0; i < num; i++)
            {
                temp += "\n";
            }
            Text.Write(temp);
        }        /// <summary>
        /// Console.Write Wrapper
        /// </summary>
        /// <param name="aText"></param>
        public static void Write(string aText)
        { 
            Console.Write(aText);
        }
        /// <summary>
        /// Console.WriteLine Wrapper
        /// </summary>
        /// <param name="aText"></param>
        public static void WriteLine(string aText)
        {
            int start = 0, end;
            int margin = (Program.windowWidth );
            var lines = new List<string>();
            aText = Regex.Replace(aText, @"\s", " ").Trim();

            while ((end = start + margin) < aText.Length)
            {
                while (aText[end] != ' ' && end > start)
                    end -= 1;

                if (end == start)
                    end = start + margin;

                lines.Add(aText.Substring(start, end - start));
                start = end + 1;
            }

            if (start < aText.Length)
                lines.Add(aText.Substring(start));


            for (int i = 0; i < lines.Count; i++)
            {
                Console.WriteLine(lines[i]);
            }
        }
        /// <summary>
        /// Console.Write the text using color denoted by |c| where c = Color Name
        /// </summary>
        /// <param name="aText"></param>
        public static void WriteColor(string aText)
        {
            string[] segments = aText.Split('|');

            for (int i = 0; i < segments.Length; i++)
            {
                if (i % 2 == 0)
                {
                    Console.Write(segments[i]);
                }
                else
                {
                    switch (segments[i])
                    {
                        case "b":
                            Console.ForegroundColor = ConsoleColor.Blue;
                            break;
                        case "gr":
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case "m":
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            break;
                        case "w":
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case "bl":
                            Console.ForegroundColor = ConsoleColor.Black;
                            break;
                        case "c":
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            break;
                        case "g":
                            Console.ForegroundColor = ConsoleColor.Gray;
                            break;
                        case "r":
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        case "y":
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;
                        case "db":
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            break;
                        case "dc":
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            break;
                        case "dg":
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            break;
                        case "dgr":
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            break;
                        case "dm":
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            break;
                        case "dr":
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            break;
                        case "dy":
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            break;
                        default:
                            aText = "|g|" + aText;
                            break;
                    }
                }
            }
        }
    }
}
