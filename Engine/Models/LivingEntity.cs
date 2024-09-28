using Engine.Services;

namespace Engine.Models
{
    public abstract class LivingEntity : BaseNotification
    {
        public EventHandler<string> OnActionPerformed;
        public EventHandler OnKilled;
        private Inventory _inventory;
        private void RaiseOnKilledEvent()
        {
            OnKilled?.Invoke(this, new System.EventArgs());
        }

        private void RaiseActionPerformedEvent(object sender, string result)
        {
            OnActionPerformed?.Invoke(this, result);
        }

        protected LivingEntity(string name, int currentHitPoints, int maxHitPoints, int gold)
        {
            Name = name;
            CurrentHitPoints = currentHitPoints;
            MaxHitPoints = maxHitPoints;
            Gold = gold;
            Inventory = new Inventory();
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
            Inventory = Inventory.AddItem(item);
        }

        public void RemoveItemsFromInventory(List<ItemQuantity> items)
        {
            Inventory = Inventory.RemoveItems(items);
        }

        public void RemoveItemFromInventory(GameItem item)
        {
            Inventory = Inventory.RemoveItem(item);
        }

        public void UseCurrentWeaponOn(LivingEntity target)
        {
            CurrentWeapon.PerformAction(this, target);
        }

        public void UseCurrentConsumable()
        {
            CurrentConsumable.PerformAction(this, this);
            RemoveItemFromInventory(CurrentConsumable);
        }

        private string _name;
        private int _currentHitPoints;
        private int _maxHitPoints;
        private int _gold;
        private int _level;
        private GameItem _currentWeapon;
        private GameItem _currentConsumable;

        public bool IsAlive => CurrentHitPoints > 0;
        public bool IsDead => !IsAlive;

        public string Name { 
            get { return _name; } 
            set 
            { 
                _name = value; 
                OnPropertyChanged();
            }
        }

        public Inventory Inventory
        {
            get => _inventory;
            set
            {
                _inventory = value;
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

        public GameItem CurrentWeapon
        {
            get => _currentWeapon;
            set
            {
                if (_currentWeapon != null)
                {
                    _currentWeapon.Action.OnActionPerformed -=
                        RaiseActionPerformedEvent;
                }
                _currentWeapon = value;
                if (_currentWeapon != null)
                {
                    _currentWeapon.Action.OnActionPerformed +=
                        RaiseActionPerformedEvent;
                }
                OnPropertyChanged();
            }
        }

        public GameItem CurrentConsumable
        {
            get => _currentConsumable;
            set
            {
                if (_currentConsumable != null)
                {
                    _currentConsumable.Action.OnActionPerformed -= RaiseActionPerformedEvent;
                }
                _currentConsumable = value;
                if (_currentConsumable != null)
                {
                    _currentConsumable.Action.OnActionPerformed += RaiseActionPerformedEvent;
                }
                OnPropertyChanged();
            }
        }

    }
}
