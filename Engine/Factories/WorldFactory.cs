using Engine.Models;

namespace Engine.Factories
{
    internal static class WorldFactory
    {
        internal static World CreateWorld()
        {
            World world = new World();

            world.AddLocation(-2, -1, "Farmer's Field",
                "There are rows of corn growing here, with" +
                "giant rats hiding between them.",
                "FarmFields.png");
            world.AddLocation(-1, -1, "Farmer's House",
                "This is the house of your neighbor, Farmer Ted.",
                "Farmhouse.png");
            world.AddLocation(0, -1, "Home",
                "This is your home",
                "Home.png");
            world.AddLocation(-1, 0, "Trading Shop",
                "The shop of Susan, the trader.",
                "Trader.png");
            world.AddLocation(0, 0, "Town square",
                "You see a fountain here.",
                "TownSquare.png");
            world.AddLocation(1, 0, "Town Gate",
                "There is a gate here, protecting the town from giant spiders.",
                "TownGate.png");
            world.AddLocation(2, 0, "Spider Forest",
                "The trees in this forest are covered with spider webs.",
                "SpiderForest.png");
            world.AddLocation(0, 1, "Herbalist's hut",
                "You see a small hut, with plants drying from the roof.",
                "HerbalistsHut.png");
            world.AddLocation(0, 2, "Herbalist's garden",
                "There are many plants here, with snakes hiding behind them.",
                "HerbalistsGarden.png");

            world.LocationAt(-2, -1).AddMonster(2, 100);
            world.LocationAt(2, 0).AddMonster(3, 100);
            world.LocationAt(0, 1).QuestAvailable.Add(QuestFactory.GetQuest(1));
            world.LocationAt(0, 2).AddMonster(1, 100);
            return world;
        }
    }
}
