using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.exam
{
    /**
    * Represents a part or service that can be sold.
    *
    * care should be taken to ensure that this class is immutable since it
    * is sent to other systems for processing that should not be able to    
    * change it in any way.
    *
    * <p>Copyright: Copyright (c) 2004</p>
    * <p>Company: Exams are us</p>
    * @author Joe Blow
    * @version 1.0
    */
    [Serializable]
    public class Item
    {
        public int Key { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public Item(int key, String name, decimal price)
        {
            this.Key = key;
            this.Name = name;
            this.Price = price;
        }

        public Item Clone()
        {
            return new Item(Key, Name, Price);
        }

        public override bool Equals(Object obj)
        {
            Item item = obj as Item;
            if (item == null)
                return false;
            else
                return Key.Equals(item.Key);
        }

        public override int GetHashCode()
        {
            return this.Key.GetHashCode();
        }

        //public override int GetHashCode()
        //{
        //    unchecked // Overflow is fine, just wrap
        //    {
        //        int hash = (int)2166136261;
        //        // Suitable nullity checks etc, of course :)
        //        hash = (hash * 16777619) ^ Key.GetHashCode();
        //        hash = (hash * 16777619) ^ Name.GetHashCode();
        //        hash = (hash * 16777619) ^ Price.GetHashCode();
        //        return hash;
        //    }
        //}
    }
}




