namespace Engine.Models
{
    public class Quest
    {

        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public List<ItemQuantity> ItemsToComplete { get; }
        public int RewardXP { get; }
        public int RewardGold { get; }
        public List<ItemQuantity> RewardItems { get; }

        public Quest(int id, string name, string description, List<ItemQuantity> itemsToComplete, int rewardXP, int rewardGold, List<ItemQuantity> rewardItems)
        {
            Id = id;
            Name = name;
            Description = description;
            ItemsToComplete = itemsToComplete;
            RewardXP = rewardXP;
            RewardGold = rewardGold;
            RewardItems = rewardItems;
        }

        public string ToolTipContents =>
                Description + Environment.NewLine + Environment.NewLine +
                "Items to complete the quest" + Environment.NewLine +
                "===========================" + Environment.NewLine +
                string.Join(Environment.NewLine, ItemsToComplete.Select(i => i.QuantityItemDescription)) +
                Environment.NewLine + Environment.NewLine +
                "Rewards\r\n" +
                "===========================" + Environment.NewLine +
                $"{RewardXP} experience points" + Environment.NewLine +
                $"{RewardGold} gold pieces" + Environment.NewLine +
                string.Join(Environment.NewLine, RewardItems.Select(i => i.QuantityItemDescription));
    }
}
