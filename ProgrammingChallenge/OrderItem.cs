using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.exam
{
    [Serializable]
    public class OrderItem
    {
        public Item Item { get; set; }
        public int Quantity { get; set; }
        public OrderItemType ItemType { get; set; }

        public bool IsTaxable { get { return ItemType == OrderItemType.Material; } }

        public decimal GetItemTotal()
        {
            return Item.Price * Quantity;
        }

        public virtual decimal GetTaxableAmount()
        {
            return 0;
        }

        public virtual OrderItem Clone()
        {
            var item = Create();
            item.Item = new Item(Item.Key, Item.Name, Item.Price);
            item.ItemType = ItemType;
            item.Quantity = Quantity;
            return item;
        }

        public virtual OrderItem Create()
        {
            return new OrderItem();
        }
    }
}
