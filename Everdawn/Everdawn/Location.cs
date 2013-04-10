using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1
{
    class Location : Entity
    {
        // the world map
        //public int indexId;
        public bool isHidden = false;
        public List<Item> items = new List<Item>();
        public List<Int32> exits = new List<Int32>();

        /// <summary>
        /// Constructor!
        /// </summary>
        /// <param name="aIndexId"></param>
        /// <param name="aName"></param>
        /// <param name="aDescription"></param>                      
        public Location(/*int aIndexId,*/ string aName, string aDescription)
            :base(aName, aDescription)
        {
            //indexId = aIndexId;
        }

        /// <summary>
        /// Add an Item to a Location
        /// </summary>
        /// <param name="aItem"></param>
        public void AddItem(Item aItem)
        {
            items.Add(aItem);
        }

        public void AddExit(int indexId)
        {
            if (indexId >= 0)
            {
                exits.Add(indexId);
            }
        }
    }
}
