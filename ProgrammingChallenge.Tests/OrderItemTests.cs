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
            var orderItem = new MaterialItem()
            {
                Item = new Item(1, "item1", .01m),
                Quantity = 100
            };

            Assert.Equal(1, orderItem.GetItemTotal());
        }

        [Fact]
        public void MaterialOrderItemShouldBeTaxable()
        {
            var orderItem = new MaterialItem()
            {
                Item = new Item(1, "item1", .01m),
                Quantity = 100,
                ItemType = OrderItemType.Material
            };

            Assert.True(orderItem.IsTaxable);
        }

        [Fact]
        public void ServiceOrderItemShouldNotBeTaxable()
        {
            var orderItem = new ServiceItem()
            {
                Item = new Item(1, "item1", .01m),
                Quantity = 100,
                ItemType = OrderItemType.Service
            };

            Assert.False(orderItem.IsTaxable);
        }

        [Fact]
        public void MaterialOrderItemShouldHaveTaxableAmount()
        {
            var orderItem = new MaterialItem()
            {
                Item = new Item(1, "item1", .01m),
                Quantity = 100,
                ItemType = OrderItemType.Material
            };

            Assert.Equal(1, orderItem.GetTaxableAmount());
        }

        [Fact]
        public void ServiceOrderItemShouldNotHaveTaxableAmount()
        {
            var orderItem = new ServiceItem()
            {
                Item = new Item(1, "item1", .01m),
                Quantity = 100,
                ItemType = OrderItemType.Service
            };

            Assert.Equal(0, orderItem.GetTaxableAmount());
        }
    }
}
