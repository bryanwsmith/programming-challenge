using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace com.exam.tests
{
    public class ItemsTests
    {
        [Fact]
        public void ShouldBeEqualWhenKeysAreTheSame()
        {
            var item1 = new Item(1, "item1", .01m);
            var item2 = new Item(1, "item2", .02m);

            Assert.Equal(item1, item2);
        }

        [Fact]
        public void ShouldNotBeEqualWhenPropertiesAreSimilarExceptKeys()
        {
            var item1 = new Item(1, "item1", .01m);
            var item2 = new Item(2, "item1", .01m);

            Assert.NotEqual(item1, item2);
        }

        [Fact]
        public void ShouldNotBeAbleToIdenticalItemToHash()
        {
            var hash = new Hashtable();
            var item1 = new Item(1, "item1", .01m);
            var item2 = new Item(1, "item1", .01m);

            hash.Add(item1, "test item 1");

            Assert.Throws<ArgumentException>(() =>
            {
                hash.Add(item2, "test item 2");
            });
        }
    }
}
