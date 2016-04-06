using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace com.exam.tests
{
    public class OrderTests
    {
        [Fact]
        public void ShouldNotBeAbleToChangeItems()
        {
            var items = new OrderItem[] {
                new OrderItem {
                    Item = new Item(1, "item1", .01m),
                    ItemType =OrderItemType.Material,
                    Quantity =1 },
                new OrderItem {
                    Item = new Item(2, "item2", .02m),
                    ItemType =OrderItemType.Material ,
                    Quantity =2
                }
            };

            var order = new Order(items);

            items[0].Item = new Item(3, "item3", .03m);
            items[0].ItemType = OrderItemType.Service;
            items[0].Quantity = 3;

            Assert.DoesNotContain(order.GetItems(), (oi) => oi.Item.Key == 3);
            Assert.DoesNotContain(order.GetItems(), (oi) => oi.Item.Name == "item3");
            Assert.DoesNotContain(order.GetItems(), (oi) => oi.Item.Price == .03m);
            Assert.DoesNotContain(order.GetItems(), (oi) => oi.ItemType == OrderItemType.Service);
            Assert.DoesNotContain(order.GetItems(), (oi) => oi.Quantity == 3);

            Assert.Contains(order.GetItems(), (oi) => oi.Item.Key == 1);
            Assert.Contains(order.GetItems(), (oi) => oi.Item.Name == "item1");
            Assert.Contains(order.GetItems(), (oi) => oi.Item.Price == .01m);
            Assert.Contains(order.GetItems(), (oi) => oi.ItemType == OrderItemType.Material);
            Assert.Contains(order.GetItems(), (oi) => oi.Quantity == 1);
        }

        [Fact]
        public void ShouldNotBeAbleToRemoveItems()
        {
            var items = new OrderItem[] {
                new OrderItem {
                    Item = new Item(1, "item1", .01m),
                    ItemType =OrderItemType.Material,
                    Quantity =1 },
                new OrderItem {
                    Item = new Item(2, "item2", .02m),
                    ItemType =OrderItemType.Service ,
                    Quantity =2
                }
            };

            var order = new Order(items);

            items[0] = null;
            items[1] = null;

            Assert.Contains(order.GetItems(), (oi) => oi.Item.Key == 1);
            Assert.Contains(order.GetItems(), (oi) => oi.Item.Name == "item1");
            Assert.Contains(order.GetItems(), (oi) => oi.Item.Price == .01m);
            Assert.Contains(order.GetItems(), (oi) => oi.ItemType == OrderItemType.Material);
            Assert.Contains(order.GetItems(), (oi) => oi.Quantity == 1);

            Assert.Contains(order.GetItems(), (oi) => oi.Item.Key == 2);
            Assert.Contains(order.GetItems(), (oi) => oi.Item.Name == "item2");
            Assert.Contains(order.GetItems(), (oi) => oi.Item.Price == .02m);
            Assert.Contains(order.GetItems(), (oi) => oi.ItemType == OrderItemType.Material);
            Assert.Contains(order.GetItems(), (oi) => oi.Quantity == 2);
        }

        [Fact]
        public void ShouldSerializeOrderItems()
        {
            var items = new OrderItem[] {
                new OrderItem {
                    Item = new Item(1, "item1", .01m),
                    ItemType =OrderItemType.Material,
                    Quantity =1 },
                new OrderItem {
                    Item = new Item(2, "item2", .02m),
                    ItemType =OrderItemType.Material ,
                    Quantity =2
                }
            };

            var order = new Order(items);

            var formatter = new BinaryFormatter();
            var stream = new MemoryStream();

            formatter.Serialize(stream, order);

            order = null;

            stream.Seek(0, SeekOrigin.Begin);

            order = formatter.Deserialize(stream) as Order;

            Assert.Contains(order.GetItems(), (oi) => oi.Item.Key == 1);
            Assert.Contains(order.GetItems(), (oi) => oi.Item.Name == "item1");
            Assert.Contains(order.GetItems(), (oi) => oi.Item.Price == .01m);
            Assert.Contains(order.GetItems(), (oi) => oi.ItemType == OrderItemType.Material);
            Assert.Contains(order.GetItems(), (oi) => oi.Quantity == 1);

            Assert.Contains(order.GetItems(), (oi) => oi.Item.Key == 2);
            Assert.Contains(order.GetItems(), (oi) => oi.Item.Name == "item2");
            Assert.Contains(order.GetItems(), (oi) => oi.Item.Price == .02m);
            Assert.Contains(order.GetItems(), (oi) => oi.ItemType == OrderItemType.Material);
            Assert.Contains(order.GetItems(), (oi) => oi.Quantity == 2);
        }

        [Fact]
        public void ShouldReturnTotalCostOfAllOrderItems()
        {
            var items = new OrderItem[] {
                new OrderItem {
                    Item = new Item(1, "item1", .10m),
                    ItemType =OrderItemType.Material,
                    Quantity =1 },
                new OrderItem {
                    Item = new Item(2, "item2", .20m),
                    ItemType =OrderItemType.Material ,
                    Quantity =2
                }
            };

            var order = new Order(items);

            Assert.Equal(.50m, order.GetOrderTotal(0m));
        }

        [Fact]
        public void ShouldReturnTotalCostOfAllOrderItemsPlusTaxes()
        {
            var items = new OrderItem[] {
                new OrderItem {
                    Item = new Item(1, "item1", .10m),
                    ItemType =OrderItemType.Material,
                    Quantity =1 },
                new OrderItem {
                    Item = new Item(2, "item2", .20m),
                    ItemType =OrderItemType.Material ,
                    Quantity =2
                }
            };

            var order = new Order(items);

            Assert.Equal(1m, order.GetOrderTotal(1m));
        }

        [Fact]
        public void ShouldRoundOrderTotalDownToNearestPenny()
        {
            var items = new OrderItem[] {
                new OrderItem {
                    Item = new Item(1, "item1", .10m),
                    ItemType =OrderItemType.Material,
                    Quantity =1 },
                new OrderItem {
                    Item = new Item(2, "item2", .20m),
                    ItemType =OrderItemType.Material ,
                    Quantity =2
                }
            };

            var order = new Order(items);

            Assert.NotEqual(.52m, order.GetOrderTotal(0.025m));
            Assert.Equal(.51m, order.GetOrderTotal(0.025m));
        }

        [Fact]
        public void ShouldRoundOrderTotalUpToNearestPenny()
        {
            var items = new OrderItem[] {
                new OrderItem {
                    Item = new Item(1, "item1", .10m),
                    ItemType =OrderItemType.Material,
                    Quantity =1 },
                new OrderItem {
                    Item = new Item(2, "item2", .20m),
                    ItemType =OrderItemType.Material ,
                    Quantity =2
                }
            };

            var order = new Order(items);

            Assert.NotEqual(.52m, order.GetOrderTotal(0.07m));
            Assert.Equal(.54m, order.GetOrderTotal(0.07m));
        }
    }
}
