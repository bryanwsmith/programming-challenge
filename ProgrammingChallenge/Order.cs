using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.exam
{
    /**
     * Represents and Order that contains a collection of items.
     *
     * care should be taken to ensure that this class is immutable since it
     * is sent to other systems for processing that should not be able 
     * to change it in any way.
     *
     * <p>Copyright: Copyright (c) 2004</p>
     * <p>Company: Exams are us</p>
     * @author Joe Blow
     * @version 1.0
     */
    [Serializable]
    public class Order
    {
        private OrderItem[] orderItems;

        public Order(OrderItem[] orderItems)
        {
            this.orderItems = Clone(orderItems);
        }

        private OrderItem[] Clone(OrderItem[] orderItems)
        {
            return orderItems.Select(i => new OrderItem()
            {
                Item = new Item(i.Item.Key, i.Item.Name, i.Item.Price),
                ItemType = i.ItemType,
                Quantity = i.Quantity
            }
            ).ToArray();
        }

        // Returns the total order cost after the tax has been applied
        public decimal GetOrderTotal(decimal taxRate)
        {
            var total = orderItems.Aggregate(0.0m, (sum, item) => sum + item.GetItemTotal());
            total = (decimal) total+(total * taxRate);
            return Math.Round(total, 2, MidpointRounding.AwayFromZero);
        }

        /**
         * Returns a Collection of all items sorted by item name
         * (case-insensitive).
         *
         * @return Collection
         */
        public List<OrderItem> GetItems()
        {
            return orderItems.ToList();
        }
    }
}
