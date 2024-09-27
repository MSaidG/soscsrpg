namespace Engine.Models
{
    public class Monster : LivingEntity
    {
        public Monster(string name, string imagePath, int maxHitPoints,
            int currentHitPoints, int rewardXP, int gold) : base(name, currentHitPoints, maxHitPoints, gold)
        {
            ImagePath = $"/soscsrpg;component/Images/Monsters/{imagePath}";
            RewardXP = rewardXP;
        }

        public string ImagePath { get;  }
        public int RewardXP { get; }
    }
}
