using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace csharpcore
{
    public class GildedRoseTest
    {
        [Fact]
        public void UpdateQuality_RegularItem_ShouldUpdateBothValues()
        {
            // Arrange
            var item = new Item {Name = "Pizza", SellIn = 5, Quality = 40};
            var items = new List<Item> {item};
            var app = new GildedRose(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(4, item.SellIn);
            Assert.Equal(39, item.Quality);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(0, 2)]
        [InlineData(-1, 2)]
        public void UpdateQuality_ItemSellDateHasPassed_ShouldDegradeQualityTwice(int sellIn, int expectedDegradation)
        {
            // Arrange
            var item = new Item {Name = "Pizza", SellIn = sellIn, Quality = 10};
            var items = new List<Item> {item};
            var app = new GildedRose(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(10 - expectedDegradation, item.Quality);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(0)]
        [InlineData(-5)]
        public void UpdateQuality_ZeroQualityItem_ShouldNotGoNegative(int sellIn)
        {
            // Arrange
            var item = new Item {Name = "Pizza", SellIn = sellIn, Quality = 0};
            var items = new List<Item> {item};
            var app = new GildedRose(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(0, item.Quality);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(0)]
        [InlineData(-5)]
        public void UpdateQuality_AgedBrie_ShouldIncreaseInQuality(int sellIn)
        {
            // Arrange
            var item = new Item {Name = "Aged Brie", SellIn = sellIn, Quality = 10};
            var items = new List<Item> {item};
            var app = new GildedRose(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(11, item.Quality);
        }

        [Theory]
        [InlineData("Aged Brie", 10)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 20)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 10)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 5)]
        public void UpdateQuality_MaxQualityItem_ShouldNotIncreaseInQuality(string name, int sellIn)
        {
            // Arrange
            var item = new Item {Name = name, SellIn = sellIn, Quality = 50};
            var items = new List<Item> {item};
            var app = new GildedRose(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(50, item.Quality);
        }

        [Fact]
        public void UpdateQuality_LegendaryItem_ShouldNotChangeValues()
        {
            // Arrange
            var item = new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 5, Quality = 80};
            var items = new List<Item> {item};
            var app = new GildedRose(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(5, item.SellIn);
            Assert.Equal(80, item.Quality);
        }

        [Theory]
        [InlineData(50, 1)]
        [InlineData(11, 1)]
        [InlineData(10, 2)]
        [InlineData(9, 2)]
        [InlineData(6, 2)]
        [InlineData(5, 3)]
        [InlineData(4, 3)]
        [InlineData(1, 3)]
        public void UpdateQuality_BackstagePasses_ShouldIncreaseInQualityBeforeSellIn(int sellIn, int qualityIncrease)
        {
            // Arrange
            var item = new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = sellIn, Quality = 10};
            var items = new List<Item> {item};
            var app = new GildedRose(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(10 + qualityIncrease, item.Quality);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void UpdateQuality_ExpiredBackstagePasses_ShouldDropQualityToZero(int sellIn)
        {
            // Arrange
            var item = new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = sellIn, Quality = 10};
            var items = new List<Item> {item};
            var app = new GildedRose(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(0, item.Quality);
        }

        [Fact]
        public void UpdateQuality_ConjuredItem_ShouldDecreaseQualityTwice()
        {
            // Arrange
            var item = new Item {Name = "Conjured", SellIn = 10, Quality = 10};
            var items = new List<Item> {item};
            var app = new GildedRose(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(8, item.Quality);
        }
    }
}