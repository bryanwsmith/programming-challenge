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
    }
}




