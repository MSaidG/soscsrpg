using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Engine.Models
{

    public class Player : BaseNotification
    {
        public Player()
        {
            Inventory = new ObservableCollection<GameItem>();
            Quests = new ObservableCollection<QuestStatus>();
        }

        public void AddItemToInventory(GameItem item)
        {
            Inventory.Add(item);
            OnPropertyChanged(nameof(Weapons));
        }

        public void RemoveItemFromInventory(GameItem item)
        {
            Inventory.Remove(item);
            OnPropertyChanged(nameof(Weapons));
        }

        public bool HasAllTheseItems(List<ItemQuantity> items)
        {
            foreach (ItemQuantity item in items)
            {
                if (Inventory.Count(i => i.Id == item.ItemID) > item.Quantity)
                {
                    return false;
                }
            }

            return true;
        }

        #region Properties

        public ObservableCollection<GameItem> Inventory { get; set; }
        public ObservableCollection<QuestStatus> Quests { get; set; }
        public List<GameItem> Weapons =>
            Inventory.Where(i => i is Weapon).ToList();

        private string? _name { get; set; }
        private string? _characterClass { get; set; }
        private int _hitPoints { get; set; }
        private int _xp { get; set; }
        private int _level {  get; set; }
        private int _gold {  get; set; }


        public string? Name
        { 
            get { return _name; } 
            set 
            { 
                _name = value;
                OnPropertyChanged();
            } 
        }

        public string? CharacterClass
        {
            get { return _characterClass; }
            set
            {
                _characterClass = value;
                OnPropertyChanged();
            }
        }

        public int HitPoints
        {
            get { return _hitPoints; }
            set
            {
                _hitPoints = value;
                OnPropertyChanged();
            }
        }

        public int XP
        {
            get { return _xp; }
            set
            {
                _xp = value;
                Debug.WriteLine("FREE XP LOL");
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

        public int Gold
        {
            get { return _gold; }
            set
            {
                _gold = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}
