namespace Engine.Models
{
    public class Monster : LivingEntity
    {
        public Monster(string name, string imagePath, int maxHitPoints,
            int hitPoints, int minDamage, int maxDamage,
            int rewardXP, int rewardGold)
        {
            Name = name;
            ImagePath = $"/soscsrpg;component/Images/Monsters/{imagePath}";
            MaxHitPoints = maxHitPoints;
            CurrentHitPoints = hitPoints;
            MaxDamage = maxDamage;
            MinDamage = minDamage;
            RewardXP = rewardXP;
            Gold = rewardGold;
        }

        public string ImagePath { get; set; }
        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public int RewardXP { get; private set; }
    }
}
