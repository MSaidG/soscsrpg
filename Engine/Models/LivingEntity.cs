using System.Collections.ObjectModel;

namespace Engine.Models
{
    public abstract class LivingEntity : BaseNotification
    {
        public EventHandler OnKilled;
        private void RaiseOnKilledEvent()
        {
            OnKilled?.Invoke(this, new System.EventArgs());
        }

        protected LivingEntity(string name, int currentHitPoints, int maxHitPoints, int gold)
        {
            Name = name;
            CurrentHitPoints = currentHitPoints;
            MaxHitPoints = maxHitPoints;
            Gold = gold;
            Inventory = new ObservableCollection<GameItem>();
            GroupedInventory = new ObservableCollection<GroupedInventoryItem>();
        }

        public void TakeDamage(int hitPoints)
        {
            CurrentHitPoints -= hitPoints;
            if (IsDead)
            {
                CurrentHitPoints = 0;
                RaiseOnKilledEvent();
            }
        }

        public void Heal(int hitPoints)
        {
            CurrentHitPoints += hitPoints;
            if (CurrentHitPoints > MaxHitPoints)
            {
                CurrentHitPoints = MaxHitPoints;
            }
        }

        public void HealFull()
        {
            CurrentHitPoints = MaxHitPoints;
        }

        public void ReceiveGold(int amount)
        {
            Gold += amount;
        }

        public void SpendGold(int amount)
        {
            if (amount > Gold)
            {
                throw new ArgumentOutOfRangeException($"{Name} only has {Gold}, cannt spend {amount} gold!");
            }
            Gold -= amount;
        }

        public void AddItemToInventory(GameItem item)
        {
            Inventory.Add(item);

            if (item.IsUnique)
            {
                GroupedInventory.Add(new GroupedInventoryItem(item, 1));
            }
            else
            {
                if (!GroupedInventory.Any(gi => gi.Item.Id == item.Id))
                {
                    GroupedInventory.Add(new GroupedInventoryItem(item, 0));
                }
                GroupedInventory.First(gi => gi.Item.Id == item.Id).Count++;
            }

            OnPropertyChanged(nameof(Weapons));
        }

        public void RemoveItemFromInventory(GameItem item)
        {
            Inventory.Remove(item);

            GroupedInventoryItem groupedInventoryItem = item.IsUnique ?
                GroupedInventory.FirstOrDefault(gi => gi.Item == item) : GroupedInventory.FirstOrDefault(gi => gi.Item.Id == item.Id);

            if (groupedInventoryItem != null)
            {
                if (groupedInventoryItem.Count == 1)
                {
                    GroupedInventory.Remove(groupedInventoryItem);
                }
                else
                {
                    groupedInventoryItem.Count--;
                }
            }

            OnPropertyChanged(nameof(Weapons));
        }

        private string _name;
        private int _currentHitPoints;
        private int _maxHitPoints;
        private int _gold;
        private int _level;

        public ObservableCollection<GameItem> Inventory { get; set; }
        public ObservableCollection<GroupedInventoryItem> GroupedInventory {  get; set; }
        public List<GameItem> Weapons => Inventory.Where(i => i is Weapon).ToList();
        public bool IsDead => CurrentHitPoints <= 0;

        public string Name { 
            get { return _name; } 
            set 
            { 
                _name = value; 
                    OnPropertyChanged();
            }
        }

        public int CurrentHitPoints
        {
            get { return _currentHitPoints; }
            set
            {
                _currentHitPoints = value;
                OnPropertyChanged();
            }
        }

        public int MaxHitPoints
        {
            get { return _maxHitPoints; }
            set
            {
                _maxHitPoints = value;
                OnPropertyChanged();
            }
        }

        public int Gold
        {
            get { return _gold; }
            set
            {
                _gold = value;
                OnPropertyChanged();
            }
        }

        public int Level
        {
            get { return _level; }
            set
            {
                _level = value;
                OnPropertyChanged();
            }
        }

    }
}
