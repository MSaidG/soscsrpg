using Engine.Models;

namespace Engine.Factories
{
    internal class WorldFactory
    {
        internal World CreateWorld()
        {
            World world = new World();

            world.AddLocation(-2, -1, "Farmer's Field",
                "There are rows of corn growing here, with" +
                "giant rats hiding between them.",
                "/soscsrpg;component/Images/Locations/FarmFields.png");
            world.AddLocation(-1, -1, "Farmer's House",
                "This is the house of your neighbor, Farmer Ted.",
                "/soscsrpg;component/Images/Locations/Farmhouse.png");
            world.AddLocation(0, -1, "Home",
                "This is your home",
                "/soscsrpg;component/Images/Locations/Home.png");
            world.AddLocation(-1, 0, "Trading Shop",
                "The shop of Susan, the trader.",
                "/soscsrpg;component/Images/Locations/Trader.png");
            world.AddLocation(0, 0, "Town square",
                "You see a fountain here.",
                "/soscsrpg;component/Images/Locations/TownSquare.png");
            world.AddLocation(1, 0, "Town Gate",
                "There is a gate here, protecting the town from giant spiders.",
                "/soscsrpg;component/Images/Locations/TownGate.png");
            world.AddLocation(2, 0, "Spider Forest",
                "The trees in this forest are covered with spider webs.",
                "/soscsrpg;component/Images/Locations/SpiderForest.png");
            world.AddLocation(0, 1, "Herbalist's hut",
                "You see a small hut, with plants drying from the roof.",
                "/soscsrpg;component/Images/Locations/HerbalistsHut.png");
            world.AddLocation(0, 2, "Herbalist's garden",
                "There are many plants here, with snakes hiding behind them.",
                "/soscsrpg;component/Images/Locations/HerbalistsGarden.png");
            return world;
        }
    }
}
