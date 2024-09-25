using Engine.Models;

namespace Engine.Factories
{
    public static class TraderFactory
    {
        private static readonly List<Trader> _traders = new();

        static TraderFactory()
        {
            Trader susan = new Trader("Susan");
            susan.AddItemToInventory(ItemFactory.CreateGameItem(1001));
            AddTraderToList(susan);

            Trader farmerTed = new Trader("Ted");
            farmerTed.AddItemToInventory(ItemFactory.CreateGameItem(1001));
            AddTraderToList(farmerTed);

            Trader peteTheHerbalist = new Trader("Pete The Herbalist");
            peteTheHerbalist.AddItemToInventory(ItemFactory.CreateGameItem(1001));
            AddTraderToList(peteTheHerbalist);
        }

        public static Trader? GetTrader(string name)
        {
            return _traders.FirstOrDefault(t => t.Name == name);
        }

        private static void AddTraderToList(Trader trader)
        {
            if (_traders.Any(t => t.Name == trader.Name))
            {
                throw new ArgumentException(
                    $"There is already a trader named '{trader.Name}'.");
            }
            _traders.Add(trader);
        }
    }
}
