using Engine.Actions;
using Engine.Models;
using System.Xml;

namespace Engine.Factories
{
    public static class ItemFactory
    {
        private const string GAME_DATA_FILENAME = ".\\GameData\\GameItems.xml";
        private static readonly List<GameItem> _standardGameItems = new List<GameItem>();
        static ItemFactory()
        {
            if (File.Exists(GAME_DATA_FILENAME))
            {
                XmlDocument data = new XmlDocument();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILENAME));
                LoadItemsFromNodes(data.SelectNodes("/GameItems/Weapons/Weapon"));
                LoadItemsFromNodes(data.SelectNodes("/GameItems/HealingItems/HealingItem"));
                LoadItemsFromNodes(data.SelectNodes("/GameItems/MiscellaneousItems/MiscellaneousItem"));
            }
            else
            {
                throw new FileNotFoundException($"Missing data file: {GAME_DATA_FILENAME}");
            }
        }

        private static void LoadItemsFromNodes(XmlNodeList nodes)
        {
            if (nodes == null) return;

            foreach (XmlNode node in nodes)
            {
                GameItem.ItemType itemType = DetermineItemType(node.Name);
                GameItem gameItem = new GameItem(itemType,
                    GetXmlAttributesAsInt(node, "ID"),
                    GetXmlAttributesAsString(node, "Name"),
                    GetXmlAttributesAsString(node, "Description"),
                    GetXmlAttributesAsInt(node, "Price"),
                    itemType == GameItem.ItemType.Weapon);

                if (itemType == GameItem.ItemType.Weapon)
                {
                    gameItem.Action = new AttackWithWeapon(gameItem,
                        GetXmlAttributesAsInt(node, "MinDamage"),
                        GetXmlAttributesAsInt(node, "MaxDamage") );
                }
                else if (itemType == GameItem.ItemType.Consumable)
                {
                    gameItem.Action = new Heal(gameItem,
                        GetXmlAttributesAsInt(node, "HitPointsToHeal"));
                }
                _standardGameItems.Add(gameItem);
            }

        }

        private static string GetXmlAttributesAsString(XmlNode node, string attributeName)
        {
            return GetXmlAttribute(node, attributeName);
        }


        private static int GetXmlAttributesAsInt(XmlNode node, string attributeName)
        {
            return Convert.ToInt32(GetXmlAttribute(node, attributeName));
        }

        private static string GetXmlAttribute(XmlNode node, string attributeName)
        {
            XmlAttribute? attribute = node.Attributes?[attributeName];
            if (attribute == null)
            {
                throw new ArgumentException(
                    $"The attribute '{attributeName}' does not exist.");
            }
            return attribute.Value;
        }

        private static GameItem.ItemType DetermineItemType(string itemType)
        {
            switch (itemType)
            {
                case "Weapon":
                    return GameItem.ItemType.Weapon;
                case "HealingItem":
                    return GameItem.ItemType.Consumable;
                default:
                    return GameItem.ItemType.Miscellaneous;
            }
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
        public static string ItemName(int id)
        {
            return _standardGameItems.FirstOrDefault(i => i.Id == id)?.Name ?? "";
        }

    }
}
