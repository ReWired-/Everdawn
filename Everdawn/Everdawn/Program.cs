using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Everdawn
{
    class Program
    {
        public static int windowWidth = 80;
        public static int windowHeight = 35;
        public static bool run = true;
        public static string errorMessage;
        public static bool isError = false;

        static void Main(string[] args)
        {
            //Setup the console window
            Console.Clear(); 
            //Set the size
            Console.SetWindowSize(windowWidth, windowHeight);
            //Remove scroll bar with buffer size equal to window size
            Console.BufferWidth = windowWidth;
            Console.BufferHeight = windowHeight;
            
            //Generate the world
            World.GenerateWorld();

            //Begin character creation
            string name = Text.Prompt("Hello there stranger.  What's your name?");
            Player.name = name;
			string age = TextReader.Prompt("Now how old are you? (Please pick an age over 17)");
			Player.age = age;
            Player.inventory.Add(new Item("Knife", "A dull blade which is your only protection in these dangerous streets.", 22));

            Text.WriteLine("Thanks, " + name);
            Text.WriteLine("Press any key to get started.");
            Console.ReadKey();
            Console.Clear();
            Text.WriteLine("Welcome to Everdawn. Prominent supercity of the planet Leo IV, boasting over 2 billion souls. This super-giant of magnificent architecture has been under a wave of homelessness and gang violence for over a decade. You'd just be as surprised as the next visitor if you weren't in the crowd of the impoverished. \nYour name is " + name + " and you've been on the streets for " + (age - 15) + " years. The last you remember of your parents are their bloody faces on the Velopide-Austerwan metro. You have perpetually nagging fear that somewhere someone wants you dead.\n \nPress a key...");
            Console.ReadKey();
            Console.Clear();

            while (run)
            {
                if (!isError)
                {
                    Text.SetPrompt();
                    World.LocationDescription();

                    string temp = Text.Prompt("");
                    Console.Clear();
                    Player.Do(temp);
                }
                //There is an error
                else
                {
                    DisplayError();
                }
            }
        }
        
        //Stream writer to write to file.
        public static void SaveGame()
        {
            StreamWriter stream = new StreamWriter("save.dat");
            stream.WriteLine(Player.location);
            stream.Close();
        }

        public static void LoadGame()
        {
            if (File.Exists("save.dat"))
            {
                StreamReader stream = new StreamReader("save.dat");
                Player.location = int.Parse(stream.ReadLine());
                stream.Close();
            }
        }
        //Stream reader to find file
        //public static void LoadGame()
        //{
        //    try
        //    {
        //        using (Stream stream = File.Open("data.bin", FileMode.Open))
        //        {
        //            BinaryFormatter bin = new BinaryFormatter();
        //            World.map = (List<Location>)bin.Deserialize(stream);
        //        }
        //    }
        //    catch (IOException)
        //    {
        //    }
        //}

        public static void WinGame()
        {
            run = false;
            Text.WriteLine("Filler text here.");
        }

        #region Error Handling
        public static void SetError(string aText)
        {
            isError = true;
            errorMessage = aText;
        }

        public static void UnsetError()
        {
            isError = false;
            errorMessage = "";
        }

        public static void DisplayError()
        {
            Text.WriteColor("|r|" + errorMessage + "|g|");
            Text.BlankLines(2);
            UnsetError();
        }
        #endregion
    }
}
