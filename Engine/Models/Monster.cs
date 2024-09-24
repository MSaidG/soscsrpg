using System.Collections.ObjectModel;

namespace Engine.Models
{
    public class Monster : BaseNotification
    {

        private int _hitPoints;

        public Monster(string name, string imagePath, int maxHitPoints,
            int hitPoints, int minDamage, int maxDamage,
            int rewardXP, int rewardGold)
        {
            Name = name;
            ImagePath = $"/soscsrpg;component/Images/Monsters/{imagePath}";
            MaxHitPoints = maxHitPoints;
            HitPoints = hitPoints;
            MaxDamage = maxDamage;
            MinDamage = minDamage;
            RewardXP = rewardXP;
            RewardGold = rewardGold;
            Inventory = new ObservableCollection<ItemQuantity>();
        }

        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int MaxHitPoints { get; private set; }
        public int HitPoints 
        { 
            get { return _hitPoints; }
            set 
            { 
                _hitPoints = value; 
                OnPropertyChanged();
            }
        }
        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public int RewardXP { get; private set; }
        public int RewardGold { get; private set; }
        public ObservableCollection<ItemQuantity> Inventory { get; set; }
    }
}
