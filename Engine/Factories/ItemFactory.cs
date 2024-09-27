using Engine.Actions;
using Engine.Models;

namespace Engine.Factories
{
    public static class ItemFactory
    {

        private static readonly List<GameItem> _standardGameItems = new List<GameItem>();
        static ItemFactory()
        {
            BuildWeapon(1001, "Pointy Stick",
                "Simple stick that can be used" +
                " as a weapon.", 1, 1, 2);
            BuildWeapon(1002, "Rusty Sword",
                "Old rusty sword that can still" +
                " be used as weapon.", 1, 1, 2);
            BuildWeapon(1501, "Snake Fangs",
                "Old rusty sword that can still" +
                " be used as weapon.", 0, 0, 2);
            BuildWeapon(1502, "Rat Claws",
                "Old rusty sword that can still" +
                " be used as weapon.",0, 0, 2);
            BuildWeapon(1503, "Spider Fangs",
                "Old rusty sword that can still" +
                " be used as weapon.", 0, 0, 4);
            BuildHealingItem(2001, "Granola bar",
                "High protein bar that heal who" +
                " consume it.", 5, 2);
            BuildMiscellaneousItem(9001, "Snake fang", 
                "Fang of a dead snake", 1);
            BuildMiscellaneousItem(9002, "Snake skin",
                "Skin of a dead snake", 2);
            BuildMiscellaneousItem(9003, "Rat tail",
                "Tail of a dead rat", 1);
            BuildMiscellaneousItem(9004, "Rat fur",
                "Skin of a dead rat", 2);
            BuildMiscellaneousItem(9005, "Spider fang",
                "Fang of a dead spider", 1);
            BuildMiscellaneousItem(9006, "Spider silk",
                "Silk of a dead spider", 2);
            BuildMiscellaneousItem(3001, "Oats", "Ingredient", 1);
            BuildMiscellaneousItem(3002, "Honey", "Ingredient", 2);
            BuildMiscellaneousItem(3003, "Raisins", "Ingredient", 2);
        }

        public static GameItem? CreateGameItem(int id)
        {
            return _standardGameItems.
                FirstOrDefault(
                item => item.Id == id)?.
                Clone();
        }

        public static GameItem? CreateGameItem(string name)
        {
            return _standardGameItems.
                FirstOrDefault(
                item => item.Name.Equals(name))?.
                Clone();
        }

        private static void BuildMiscellaneousItem(int id, string name, 
            string description , int price)
        {
            _standardGameItems.Add(new GameItem(GameItem.ItemType.Miscellaneous,
                id, name, description, price));
        }

        private static void BuildWeapon(int id, string name, 
            string description, int price, int minDamage, int maxDamage)
        {
            GameItem weapon = new GameItem(GameItem.ItemType.Weapon, 
                id, name, description, price, true);

            weapon.Action = new AttackWithWeapon(weapon, minDamage, maxDamage);
            _standardGameItems.Add(weapon);
        }

        private static void BuildHealingItem(int id, string name, 
            string description, int price, int hitPoints)
        {
            GameItem item = new GameItem(GameItem.ItemType.Consumable,
                id, name, description, price);

            item.Action = new Heal(item, hitPoints);
            _standardGameItems.Add(item);
        }

        public static string ItemName(int id)
        {
            return _standardGameItems.FirstOrDefault(i => i.Id == id)?.Name ?? "";
        }

    }
}
