using System;
using System.Collections.Generic;
using System.Linq;
using ApprovalUtilities.Utilities;

namespace csharpcore
{
    public class GildedRose
    {
        const string AgedBrie = "Aged Brie";
        const string BackstagePasses = "Backstage passes";
        const string Conjured = "Conjured";
        const string Sulfuras = "Sulfuras";
        const int MaxQuality = 50;
        readonly string[] _specialItems = { AgedBrie, BackstagePasses, Conjured, Sulfuras };
        Dictionary<Func<Item, bool>, Action<Item>> _itemRules;

        IList<Item> Items;

        public GildedRose(IList<Item> items)
        {
            Items = items;

            InitializeItemRules();
        }

        private void InitializeItemRules()
        {
            // Define item update rules
            _itemRules = new Dictionary<Func<Item, bool>, Action<Item>>();

            // Aged brie rule
            _itemRules.Add(item => item.Name.Contains(AgedBrie),
                item => item.Quality = Math.Min(MaxQuality, item.Quality + 1));

            // Backstage passes rule
            _itemRules.Add(item => item.Name.Contains(BackstagePasses),
                item =>
                {
                    var qualityChange = item.SellIn > 10 ? 1
                        : item.SellIn > 5 ? 2
                        : item.SellIn > 0 ? 3
                        : -item.Quality;
                    item.Quality = Math.Min(MaxQuality, item.Quality + qualityChange);
                });

            // Conjured items rule
            _itemRules.Add(item => item.Name.Contains(Conjured),
                item => item.Quality = Math.Max(0, item.Quality - (item.SellIn > 0 ? 2 : 4)));

            // Non-special items rule
            _itemRules.Add(item => !_specialItems.Any(si => item.Name.Contains(si)),
                item => item.Quality = Math.Max(0, item.Quality - (item.SellIn > 0 ? 1 : 2)));

            // Update sell in rule
            _itemRules.Add(item => !item.Name.Contains(Sulfuras), item => item.SellIn--);
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                // Apply rules
                _itemRules.Where(rule => rule.Key(item)).ForEach(rule => rule.Value(item));
            }
        }
    }
}
