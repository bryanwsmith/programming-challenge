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
                new MaterialItem {
                    Item = new Item(1, "item1", .01m),
                    Quantity =1 },
                new MaterialItem {
                    Item = new Item(2, "item2", .02m),
                    Quantity =2
                }
            };

            var order = new Order(items);

            items[0].Item = new Item(3, "item3", .03m);
            items[0].Quantity = 3;

            Assert.DoesNotContain(order.GetItems(), (oi) => oi.Item.Key == 3);
            Assert.DoesNotContain(order.GetItems(), (oi) => oi.Item.Name == "item3");
            Assert.DoesNotContain(order.GetItems(), (oi) => oi.Item.Price == .03m);
            Assert.DoesNotContain(order.GetItems(), (oi) => oi.GetType() == typeof(ServiceItem));
            Assert.DoesNotContain(order.GetItems(), (oi) => oi.Quantity == 3);

            Assert.Contains(order.GetItems(), (oi) => oi.Item.Key == 1);
            Assert.Contains(order.GetItems(), (oi) => oi.Item.Name == "item1");
            Assert.Contains(order.GetItems(), (oi) => oi.Item.Price == .01m);
            Assert.Contains(order.GetItems(), (oi) => oi.GetType() == typeof(MaterialItem));
            Assert.Contains(order.GetItems(), (oi) => oi.Quantity == 1);
        }

        [Fact]
        public void ShouldNotBeAbleToRemoveItems()
        {
            var items = new OrderItem[] {
                new MaterialItem {
                    Item = new Item(1, "item1", .01m),
                    Quantity =1 },
                new MaterialItem {
                    Item = new Item(2, "item2", .02m),
                    Quantity =2
                }
            };

            var order = new Order(items);

            items[0] = null;
            items[1] = null;

            Assert.Contains(order.GetItems(), (oi) => oi.Item.Key == 1);
            Assert.Contains(order.GetItems(), (oi) => oi.Item.Name == "item1");
            Assert.Contains(order.GetItems(), (oi) => oi.Item.Price == .01m);
            Assert.Contains(order.GetItems(), (oi) => oi.GetType() == typeof(MaterialItem));
            Assert.Contains(order.GetItems(), (oi) => oi.Quantity == 1);

            Assert.Contains(order.GetItems(), (oi) => oi.Item.Key == 2);
            Assert.Contains(order.GetItems(), (oi) => oi.Item.Name == "item2");
            Assert.Contains(order.GetItems(), (oi) => oi.Item.Price == .02m);
            Assert.Contains(order.GetItems(), (oi) => oi.GetType() == typeof(MaterialItem));
            Assert.Contains(order.GetItems(), (oi) => oi.Quantity == 2);
        }

        [Fact]
        public void ShouldSerializeOrderItems()
        {
            var items = new OrderItem[] {
                new MaterialItem {
                    Item = new Item(1, "item1", .01m),
                    Quantity =1 },
                new MaterialItem {
                    Item = new Item(2, "item2", .02m),
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
            Assert.Contains(order.GetItems(), (oi) => oi.GetType() == typeof(MaterialItem));
            Assert.Contains(order.GetItems(), (oi) => oi.Quantity == 1);

            Assert.Contains(order.GetItems(), (oi) => oi.Item.Key == 2);
            Assert.Contains(order.GetItems(), (oi) => oi.Item.Name == "item2");
            Assert.Contains(order.GetItems(), (oi) => oi.Item.Price == .02m);
            Assert.Contains(order.GetItems(), (oi) => oi.GetType() == typeof(MaterialItem));
            Assert.Contains(order.GetItems(), (oi) => oi.Quantity == 2);
        }

        [Fact]
        public void ShouldReturnTotalCostOfAllOrderItems()
        {
            var items = new OrderItem[] {
                new MaterialItem {
                    Item = new Item(1, "item1", .10m),
                    Quantity =1 },
                new MaterialItem {
                    Item = new Item(2, "item2", .20m),
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
                new MaterialItem {
                    Item = new Item(1, "item1", .10m),
                    Quantity =1 },
                new MaterialItem {
                    Item = new Item(2, "item2", .20m),
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
                new MaterialItem {
                    Item = new Item(1, "item1", .10m),
                    Quantity =1 },
                new MaterialItem {
                    Item = new Item(2, "item2", .20m),
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
                new MaterialItem {
                    Item = new Item(1, "item1", .10m),
                    Quantity =1 },
                new MaterialItem {
                    Item = new Item(2, "item2", .20m),
                    Quantity =2
                }
            };

            var order = new Order(items);

            Assert.NotEqual(.52m, order.GetOrderTotal(0.07m));
            Assert.Equal(.54m, order.GetOrderTotal(0.07m));
        }

        [Fact]
        public void ShouldNotCalculateTaxesForServiceOrderItems()
        {
            var items = new OrderItem[] {
                new ServiceItem {
                    Item = new Item(1, "item1", .10m),
                    Quantity =1 },
                new ServiceItem {
                    Item = new Item(2, "item2", .20m),
                    Quantity =2
                }
            };

            var order = new Order(items);

            Assert.NotEqual(1m, order.GetOrderTotal(1m));
            Assert.Equal(.5m, order.GetOrderTotal(1m));
        }

        [Fact]
        public void ShouldOnlyCalculateTaxesForMaterialOrderItems()
        {
            var items = new OrderItem[] {
                new ServiceItem {
                    Item = new Item(1, "item1", .10m),
                    Quantity =1 },
                new MaterialItem {
                    Item = new Item(2, "item2", .20m),
                    Quantity =2
                }
            };

            var order = new Order(items);

            Assert.NotEqual(1m, order.GetOrderTotal(1m));
            Assert.Equal(.9m, order.GetOrderTotal(1m));
        }

        [Fact]
        public void ShouldReturnCollectionOfAllItemsSorteByItemName()
        {
            var items = new OrderItem[] {
                new MaterialItem {
                    Item = new Item(1, "item-z", .01m),
                    Quantity =1
                },
                new MaterialItem {
                    Item = new Item(1, "item-b", .01m),
                    Quantity =1
                },
                new MaterialItem {
                    Item = new Item(1, "item-x", .01m),
                    Quantity =1
                },
               new MaterialItem {
                    Item = new Item(1, "item-c", .01m),
                    Quantity =1
                },
                new MaterialItem {
                    Item = new Item(1, "item-y", .01m),
                    Quantity =1
                },
                new MaterialItem {
                    Item = new Item(1, "item-a", .01m),
                    Quantity =1
                }
            };

            var order = new Order(items);

            var sorted = order.GetItems();

            Assert.Equal(new List<Item>() {
                    new Item(1, "item-a", .01m),
                    new Item(1, "item-b", .01m),
                    new Item(1, "item-c", .01m),
                    new Item(1, "item-x", .01m),
                    new Item(1, "item-y", .01m),
                    new Item(1, "item-z", .01m),
            }, sorted.Select(item => item.Item));
        }

    }
}
