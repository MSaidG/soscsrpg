using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Engine.Models
{

    public class Player : LivingEntity
    {
        public Player()
        {
            Quests = new ObservableCollection<QuestStatus>();
        }

        public bool HasAllTheseItems(List<ItemQuantity> items)
        {
            foreach (ItemQuantity item in items)
            {
                if (Inventory.Count(i => i.Id == item.ItemID) < item.Quantity)
                {
                    return false;
                }
            }

            return true;
        }

        #region Properties

        public ObservableCollection<QuestStatus> Quests { get; set; }

        private string? _characterClass { get; set; }
        private int _xp { get; set; }
        private int _level {  get; set; }

        public string? CharacterClass
        {
            get { return _characterClass; }
            set
            {
                _characterClass = value;
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

        #endregion
    }
}
