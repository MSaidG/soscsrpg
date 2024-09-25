namespace Engine.Models
{
    public class Monster : LivingEntity
    {
        public Monster(string name, string imagePath, int maxHitPoints,
            int currentHitPoints, int minDamage, int maxDamage,
            int rewardXP, int gold) : base(name, currentHitPoints, maxHitPoints, gold)
        {
            ImagePath = $"/soscsrpg;component/Images/Monsters/{imagePath}";
            MaxDamage = maxDamage;
            MinDamage = minDamage;
            RewardXP = rewardXP;
        }

        public string ImagePath { get;  }
        public int MinDamage { get;  }
        public int MaxDamage { get;  }
        public int RewardXP { get; }
    }
}
