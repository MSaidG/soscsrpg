using Engine.Models;

namespace Engine.Factories
{
    public static class ItemFactory
    {

        private static readonly List<GameItem> _standardGameItems = new List<GameItem>();
        static ItemFactory()
        {
            _standardGameItems.Add(new Weapon(1001, "Pointy Stick",
                "Simple stick that can be used" +
                " as a weapon.", 1, 1, 2));
            _standardGameItems.Add(new Weapon(1002, "Rusty Sword",
                "Old rusty sword that can still" +
                " be used as weapon.", 1, 1, 2));
            _standardGameItems.Add(new GameItem(9001, "Snake fang", 
                "Fang of a dead snake", 1));
            _standardGameItems.Add(new GameItem(9002, "Snake skin",
                "Skin of a dead snake", 2));
            _standardGameItems.Add(new GameItem(9003, "Rat tail",
                "Tail of a dead rat", 1));
            _standardGameItems.Add(new GameItem(9004, "Rat fur",
                "Skin of a dead rat", 2));
            _standardGameItems.Add(new GameItem(9005, "Spider fang",
                "Fang of a dead spider", 1));
            _standardGameItems.Add(new GameItem(9006, "Spider silk",
                "Silk of a dead spider", 2));
        }

        public static GameItem? CreateGameItem(int id)
        {
            GameItem? standardItem = _standardGameItems.
                FirstOrDefault(item => item.Id == id);
            if (standardItem != null)
            {
                if (standardItem is Weapon standardWeapon)
                {
                    return standardWeapon.Clone();
                }
                return standardItem.Clone();
            }
            return null;
        }

    }
}
