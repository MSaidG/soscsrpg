using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Engine.Models
{
    public class Player : BaseNotification
    {
        private string? _name { get; set; }
        private string? _characterClass { get; set; }
        private int _hitPoints { get; set; }
        private int _experiencePoints { get; set; }
        private int _level {  get; set; }
        private int _gold {  get; set; }

        public ObservableCollection<GameItem> Inventory { get; set; }
        public ObservableCollection<QuestStatus> Quests { get; set; }
        public Player()
        {
            Inventory = new ObservableCollection<GameItem>();
            Quests = new ObservableCollection<QuestStatus>();
        }

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

        public int ExperiencePoints
        {
            get { return _experiencePoints; }
            set
            {
                _experiencePoints = value;
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

    }
}
