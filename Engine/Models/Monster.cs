using System.Collections.ObjectModel;

namespace Engine.Models
{
    public class Monster : BaseNotification
    {

        private int _hitPoints;

        public Monster(string name, string imagePath, int maxHitPoints,
            int hitPoints, int rewardXP, int rewardGold)
        {
            Name = name;
            ImagePath = $"/soscsrpg;component/Imgages/Monsters/{imagePath}";
            MaxHitPoints = maxHitPoints;
            HitPoints = hitPoints;
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

        public int RewardXP { get; private set; }
        public int RewardGold { get; private set; }
        public ObservableCollection<ItemQuantity> Inventory { get; set; }
    }
}
