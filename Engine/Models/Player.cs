using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Engine.Models
{

    public class Player : LivingEntity
    {
        public EventHandler OnLeveledUp;
        public Player(string name, string characterClass, int xp, int maxHitPoints, int currentHitPoints, int gold) : base(name, currentHitPoints, maxHitPoints, gold)
        {
            CharacterClass = characterClass;
            XP = xp;
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
                OnPropertyChanged();
                SetLevelAndMaxHitPoints();
            }
        }

        private void SetLevelAndMaxHitPoints()
        {
            int originalLevel = Level;
            Level = (XP / 100) + 1;
            
            if (Level != originalLevel)
            {
                MaxHitPoints = Level * 10;
                CurrentHitPoints += 10;
                OnLeveledUp?.Invoke(this, System.EventArgs.Empty);
            }
        }

        public void AddXP(int xp)
        {
            XP += xp;
        }

        #endregion
    }
}
