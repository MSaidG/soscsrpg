using Engine.Factories;

namespace Engine.Models
{
    public class Monster : LivingEntity
    {
        public Monster(int id, string name, string imagePath, int maxHitPoints, GameItem currentWeapon,
             int rewardXP, int gold) : base(name, maxHitPoints, maxHitPoints, gold)
        {
            ID = id;
            ImagePath = imagePath;
            CurrentWeapon = currentWeapon;
            RewardXP = rewardXP;
        }

        public void AddItemToLootTable(int id, int percentage)
        {
            _lootTable.RemoveAll(ip => ip.ID == id);
            _lootTable.Add(new ItemPercentage(id, percentage));
        }

        public Monster GetNewInstance()
        {
            Monster newMonster = new Monster(ID, Name, ImagePath,
                MaxHitPoints, CurrentWeapon, RewardXP, Gold);

            foreach(ItemPercentage itemPercentage in _lootTable)
            {
                newMonster.AddItemToLootTable(itemPercentage.ID, itemPercentage.Percentage);
                if (RandomNumberGenerator.NumberBetween(1, 100) <= itemPercentage.Percentage)
                {
                    newMonster.AddItemToInventory(ItemFactory.CreateGameItem(itemPercentage.ID));
                }
            }
            return newMonster;
        }

        private readonly List<ItemPercentage> _lootTable = 
            new List<ItemPercentage>();
        public int ID { get; }
        public string ImagePath { get;  }
        public int RewardXP { get; }
    }
}
