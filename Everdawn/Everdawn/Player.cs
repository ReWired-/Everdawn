using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Runtime.Serialization;
//using System.Runtime.Serialization.Formatters.Binary;

namespace Everdawn
{
    //[Serializable()]
    class Player //: ISerializable
    {
        public static string name;
        //public static string desc;
        //public static int[] stats;
        public static int location;
        public static List<Item> inventory = new List<Item>();
        public static string[] possibleActions = new string[] { "move", "m", "look", "l", "take", "t", "drop", "d", "use", "u", "inventory", "i", "help", "h", "quit", "exit", "save", "load" };

        /// <summary>
        /// Player action, array of predicate and target
        /// </summary>
        /// <param name="aAction"></param>
        /// <returns></returns>
        public static void Do(string aText)
        {
            string verb = "";
            string noun = "";

            // check if there is a space in the given command
            if (aText.IndexOf(' ') != -1)
            {
                // split the string into the verb and noun
                string[] temp = aText.Split(new char[] { ' ' }, 2);
                verb = temp[0].ToLower();
                noun = temp[1].ToLower();
            }
            else
            {
                // verb only
                verb = aText.ToLower();
            }

            if (IsAction(verb))
            {
                // do whatever action
                switch (verb)
                {
                    case "move":
                    case "m":
                        if (noun == "")
                            Program.SetError("You must specify a location to move.");
                        else
                            MoveTo(noun);
                        break;

                    case "look":
                    case "l":
                        if (noun == "")
                            noun = World.map[Player.location].name;

                        LookAt(noun);
                        break;

                    case "take":
                    case "t":
                        if (noun == "")
                            Program.SetError("You haven't specified an item to take!");
                        else
                            PickUpItem(noun);
                        break;

                    case "drop":
                    case "d":
                        if (noun == "")
                            Program.SetError("You haven't specified an item to drop!");
                        else
                            DropItem(noun);
                        break;

                    case "use":
                    case "u":
                        if (noun == "")
                            Program.SetError("You haven't specified an item to use!");
                        else
                            UseItem(noun);
                        break;

                    case "inventory":
                    case "i":
                        ListInventory();
                        break;

                    case "help":
                    case "h":
                        ListActions();
                        break;

                    case "quit":
                    case "exit":
                        QuitPrompt();
                        break;

                    case "save":
                        Program.SaveGame();
                        break;

                    case "load":
                        Program.LoadGame();
                        break;
                }
            }
            else
            {
                // not a real action
                Program.SetError("Action not found.");
            }
        }

        public static void MoveTo(string location)
        {
            // is location?
            if (World.IsLocation(location))
            {
                int locationId = World.GetLocationIdByName(location);

                if (World.IsLocationExit(location))
                {
                    // set the player's new location
                    Player.location = locationId;
                }
                else
                {
                    Program.SetError("Your path is not valid!");
                }
            }
            else
            {
                Program.SetError("That isn't a place!");
            }
        }

        public static void LookAt(string noun)
        {
            // is location?  
            if (World.IsLocation(noun))
            {
                Console.Clear();
                World.ShowHiddenItems();
                Text.BlankLines(2);
            }
            // is item?
            else if (Item.IsItemInInventory(noun) || Item.IsItemInLocation(noun))
            {
                Console.Clear();
                Text.WriteLine(Item.GetItemDescByName(noun));
                Text.BlankLines(2);
            }
        }

        public static void PickUpItem(string item)
        {
            // is item?
            if (Item.IsItemInLocation(item))
            {
                // get description
                string desc = Item.GetItemDescByName(item);
                int actionLocationId = Item.GetItemActionLocationIdByName(item);
                // remove item from location
                Item.RemoveItemFromLocation(item);
                // add item to inventory
                Player.inventory.Add(new Item(item, desc, actionLocationId));
            }
            else
            {
                Program.SetError("Item not found here.");
            }
        }

        public static void DropItem(string item)
        {
            //is item?
            if (Item.IsItemInInventory(item))
            {
                string desc = Item.GetItemDescByName(item);
                int actionLocationId = Item.GetItemActionLocationIdByName(item);

                // remove item from inventory
                RemoveInventoryItem(item);

                // add item to location
                World.map[Player.location].items.Add(new Item(item, desc, actionLocationId));
            }
            else
            {
                Program.SetError("Item not in your inventory.");
            }
        }

        public static void RemoveInventoryItem(string item)
        {
            for (int i = 0; i < Player.inventory.Count; i++)
            {
                if (Player.inventory[i].name.ToLower() == item.ToLower())
                {
                    Player.inventory.RemoveAt(i);
                }
            }
        }

        public static void UseItem(string item)
        {
            // is item?
            if (Item.IsItemInInventory(item))
            {
                // get item actionLocationId
                int itemActionLocationId = Item.GetItemActionLocationIdByName(item);
                if (itemActionLocationId == Player.location)
                {
                    World.UseItemInLocation(item);
                }
                else
                {
                    Program.SetError("You're not sure how that helps here.");
                }
            }
            else
            {
                Program.SetError("Item not in inventory!");
            }
        }

        public static void ListInventory()
        {
            Text.WriteLine("\n-- Inventory -- \n");

            for (int i = 0; i < Player.inventory.Count; i++)
            {
                Text.WriteLine(i.ToString() + ": "+ Player.inventory[i].name);
            }

            Text.BlankLines(2);
        }

        public static void ListActions()
        {
            Text.BlankLines(2);

            //"move", "look", "take", "drop", "use", "inventory", "help"
            Text.WriteLine("\n-- Actions -- \n");
            Text.WriteLine("move - travel to another location.   usage: move alley (or) m alley");
            Text.WriteLine("look - look at a location or an item.   usage: look alley (or) look knife (or) l alley (or) l knife");
            Text.WriteLine("take - pick up an item in a location.   usage: take sword (or) t knife");
            Text.WriteLine("drop - drop an item from your inventory.   usage: drop knife (or) d knife");
            Text.WriteLine("use - use an item in your inventory.   usage: use key card (or) u key card");
            Text.WriteLine("inventory - show items in your inventory.   usage: inventory (or) i");
            Text.WriteLine("help - show this screen.   usage: help (or) h");
            Text.WriteLine("exit - quit the game.    usage: quit (or) exit");

            Text.BlankLines(2);
        }

        /// <summary>
        /// Check the legitmacy of the action
        /// </summary>
        /// <param name="aText"></param>
        /// <returns></returns>
        public static bool IsAction(string aText)
        {
            for (int i = 0; i < possibleActions.Length; i++)
            {
                if (aText == possibleActions[i])
                {
                    return true;
                }
            }
            return false;
        }

        public static void QuitPrompt()
        {
            Text.WriteLine("Are you sure you want to leave?  y/n");
            string answer = Console.ReadLine().ToLower();

            if (answer == "y" || answer == "yes")
            {
                Program.run = false;
            }
        }
    }
}
