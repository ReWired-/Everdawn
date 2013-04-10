uusing System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Runtime.Serialization;
//using System.Runtime.Serialization.Formatters.Binary;

namespace Project1
{
    class  World 
    {
        public static List<Location> map = new List<Location>();

        /// <summary>
        ///  Create the World
        /// </summary>
        public static void GenerateWorld()
        {
            map.Add(new Location("Alley Hidehout", "Your small hideout, the sun barely makes it through the tattered covers that protect you from the elements. Your friend Remus sits on a few cinderblocks reading a tattered newspaper. Max, your dog, seems to be nowhere in sight. The rags in lieu of a door are fluttering in the breeze."));
            map[0].AddItem(new Item("", "A small stone that looks good for throwing.", 18));
            map[0].AddExit(1);
			
			map.Add(new Location("a", "a"));
        }

        /// <summary>
        /// Use a item in a location
        /// </summary>
        /// <param name="item"></param>
        public static void UseItemInLocation(string item)
        {
            string message = "";
            switch (item.ToLower())
            {
                // open passage from bedroom to throne room
                case "stone":
                    map[18].AddExit(22);
                    message = "You hurl the stone at the vanity mirror!   The mirror cracks sharply into many pieces which fall to the floor and shatter.   Behind the mirror, you find a secret passage!";
                    // mirror is broken
                    map[18].description = "A very large bed with many fine pillows on it commands the middle of the room.  A master wardobe and sitting area take up the north half of the room.  And a tall vanity is now broken on the floor.";
                    break;

                // unlock storage room door
                case "brass key":
                    map[16].AddExit(17);
                    message = "The Brass Key fits neatly into the lock and turns with a click.";
                    // door is not locked
                    map[16].description = "The small arched-cealing hallway seems to impose upon your will with each step.  At the bottom of the stairwell is a stone wall with an unlit torch hanging on it.";
                    break;

                // sword cuts through tapestry
                case "rusty sword":
                    map[3].AddExit(23);
                    message = "You take the Rusty Sword in both hands and begin wildly hacking at the tapestry on the north wall.  Behind it you find a small foyer leading into the Library.";
                    // tapestry is tattered
                    map[3].description = "A long narrow room lined with covered paintings and statues.  Cobwebs fill every corner of the room, and a broken mirror lies against the west wall, reflecting the room a hundred times over in it's many pieces.  Covering the north wall a once enormous tapestry depicting this castle beneath a terrible dark maroon vortex, is now slashed to pieces.";
                    break;

                // crown in throne room wins game
                case "crown":
                    Program.WinGame();
                    break;

                // book in crypts finds silver key
                case "ancient tome":
                    map[10].AddItem(new Item("Silver Key", "A tarnished silver key.", 12));
                    message = "As you finish reading the first incantation in the Ancient Tome, the walls around you begin rumbling and shaking.  Dust and debris fill the air, as the small toy horse begins to rock back and forth vigorously.  A crack opens in the wall and a small silver key falls to the floor with a loud ring.  The Ancient Tome crumbles in your hands.";
                    // crack in wall
                    map[10].description = "Cobwebs cover everything in this dank room except a small rocking horse placed against the wall.  There is a large crack in the west wall.";
                    break;

                // silver key opens cell 1
                case "silver key":
                    map[12].AddExit(13);
                    message = "You pry the dead man's finger bones away from the lock, and slip the silver key in and turn it.  The cell door opens with a click.";
                    // door is open skeleton on floor
                    map[12].description = "This small room has little in it besides a rotten wooden table and chairs.   On the table is a ring of rusty keys and a stack of crude paper covered in an strange script.  Both cell doors are open. ";
                    break;

                // gear in clock tower open  treasure room
                case "gear":
                    map[19].AddExit(20);
                    message = "You slip the tiny gear over a square peg.  With many loud groans the machinery all around you begins to move.";
                    // machinery is moving, door is open
                    map[19].description = "Gears, cogs are in motion all around you.  The wooden floors still creak as you walk, and you catch the sounds and smells of rats. gas/oil lamp cast an erie glow.";
                    break;

                // lever opens armory
                case "lever":
                    map[4].AddExit(5);
                    message = "You slip the small hooked end of the lever into the hole beside the metal door, and it clicks into place.   You pull downward on it and the thick metal door swings open slowly.";
                    // door is open
                    map[4].description = "Matching suits of armor line the hall as a tattered red carpet stretches between them.   The last one armor on the left is missing it's strangely shaped helmet.  To the north a door leads into a kitchen.  To the west, a thick metal door blocks your way.   Beside the thick metal door is a lever in the wall.   The eastern doorway stands open.";
                    break;

                // potion clears vines, finds cloak
                case "potion":
                    map[1].AddItem(new Item("Cloak", "A rotten cloak that looks like it has been in the harsh weather for decades.", 15));
                    message = "You dump the potion into the overgrown fountain.  The vines within the fountain begin to smoke and hiss, finally burning away until you can see a dirty cloak laying over the drain in the fountain's base.";
                    // fountain is empty
                    map[1].description = "It's hard to make out details at this time of night with no moon.  With mostly touch, you decide the courtyard must be surrounded by ancient statues of indistinct forms, with an old fountian in the middle.  To the north, the once decadent great doors to the king's throne room are blocked with the ruins of a collapsed balcony. Along the western wall of the courtyard is another set of large doors inlaid with colored glass in the shape of some unknown religios symbols.  To the east, a passage whos threshold is covered with many thin vines leads into darkness.  The door to the south leads into the foyer.";
                    break;

                case "cloak":
                    map[15].AddExit(16);
                    message = "You cover the statue with the rotten cloak hiding it's gaze.   The barrier of force no long impedes your progress up the stairs.";
                    // statue is covered
                    map[15].description = "It seems to take forever to reach then end of the stairs.  On the landing half way up, an inset bookcase holds a tattered portrait and a statue of a young girl is covered in an old cloak.";
                    break;

                // candle lights torch to throne room
                case "candle":
                    map[22].AddExit(21);
                    message = "You light the torch at the base of the stairs, and the wall in front of you begins to rumble and move.   When it stops, you are looking into the Throne Room from behind the Throne.";
                    // door is open
                    map[22].description = "The small arched-cealing hallway seems to impose upon your will with each step.  At the bottom of the stairwell is a door leading into the throne room.";
                    break;
            }

            // remove item from inventory
            Player.RemoveInventoryItem(item);
                    
            // write out the message with some default colors
            Text.WriteColor("|gr|" + message + "|g|");
            Text.BlankLines(2);
        }

        public static bool IsLocation(string aName)
        {
            for (int i = 0; i < map.Count; i++)
            {
                if (map[i].name.ToLower() == aName.ToLower())
                {
                    return true;
                }
            }
            // not found
            return false;
        }

        public static bool IsLocationExit(string aName)
        {
            for (int i = 0; i < map[Player.location].exits.Count; i++)
            {
                if (map[Player.location].exits[i] == World.GetLocationIdByName(aName))
                {
                    // legitimate exit
                    return true;
                }
            }
            return false;
        }

        public static int GetLocationIdByName(string aName)
        {
            for (int i = 0; i < map.Count; i++)
            {
                if (map[i].name.ToLower() == aName.ToLower())
                {
                    return i;
                }
            }
            // not found
            return -1;
        }


        /// <summary>
        /// Draw this Location
        /// </summary>
        public static void LocationDescription()
        {
            Text.WriteLine(map[Player.location].description);
            LocationItems();
            LocationExits();
        }
        public static void LocationItems()
        {
            Text.BlankLines();
            Text.WriteColor("|gr|-- Items --|g|\n");
            for (int i = 0; i < map[Player.location].items.Count; i++)
            {
                if (map[Player.location].items[i].isHidden == false)
                    Text.WriteLine(map[Player.location].items[i].name);
            }
        }
        public static void LocationExits()
        {
            Text.BlankLines();
            Text.WriteColor("|b|-- Exits --|g|\n");
            for (int i = 0; i < map[Player.location].exits.Count; i++)
            {
                Text.WriteLine(map[map[Player.location].exits[i]].name);
            }
        }
        public void LocationName()
        {
            Text.WriteLine(map[Player.location].name);
        }
        public static void ShowHiddenItems()
        {
            for (int i = 0; i < map[Player.location].items.Count; i++)
            {
                if (map[Player.location].items[i].isHidden == true)
                {
                    map[Player.location].items[i].isHidden = false;
                    Text.WriteColor("\n|r|You found an item!|g| |gr|" + map[Player.location].items[i].name + "|g|\n");
                }
            }
        }
    }
}