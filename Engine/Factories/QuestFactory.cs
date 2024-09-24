using Engine.Models;

namespace Engine.Factories
{
    internal class QuestFactory
    {

        private static readonly List<Quest> _quests = new List<Quest>();

        static QuestFactory() 
        {
            List<ItemQuantity> itemsToComplete = [];
            List<ItemQuantity> rewardItems = [];

            itemsToComplete.Add(new ItemQuantity(9001, 1));
            rewardItems.Add(new ItemQuantity(1002, 1));

            _quests.Add(new Quest(1, "Clear the herb garden",
                "Defeat the snakes in the Herbalist's garden",
                itemsToComplete, 25, 10,
                rewardItems));
        }

        internal static Quest? GetQuest(int id)
        {
            return _quests.FirstOrDefault(
                quest => quest.Id == id);
        }

        internal static Quest? GetQuest(string name)
        {
            return _quests.FirstOrDefault(
                queest => queest.Name.Equals(name));
        }
    }
}
