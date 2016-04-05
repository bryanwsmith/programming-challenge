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
    }
}
