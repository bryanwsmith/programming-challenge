using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace com.exam.tests
{
    public class OrderItemTests
    {
        [Fact]
        public void ShouldReturnQuantityMultipliedByItemPrice()
        {
            var orderItem = new OrderItem()
            {
                Item = new Item(1, "item1", .01m),
                Quantity = 100
            };

            Assert.Equal(1, orderItem.GetItemTotal());
        }
    }
}
