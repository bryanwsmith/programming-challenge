using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.exam
{
    [Serializable]
    public abstract class OrderItem
    {
        public Item Item { get; set; }
        public int Quantity { get; set; }

        public abstract bool IsTaxable { get; }

        public decimal GetItemTotal()
        {
            return Item.Price * Quantity;
        }

        public abstract decimal GetTaxableAmount();

        public OrderItem Clone()
        {
            var item = Create();
            item.Item = new Item(Item.Key, Item.Name, Item.Price);
            item.Quantity = Quantity;
            return item;
        }

        public abstract OrderItem Create();
    }
}
