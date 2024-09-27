using System.Collections.ObjectModel;

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
            Recipes = new ObservableCollection<Recipe>();
        }

        #region Properties

        public ObservableCollection<QuestStatus> Quests { get; set; }
        public ObservableCollection<Recipe> Recipes { get; set; }

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
                if (MaxHitPoints != 10)
                { 
                    CurrentHitPoints += 10;
                }
                OnLeveledUp?.Invoke(this, System.EventArgs.Empty);
            }
        }

        public void LearnRecipe(Recipe recipe)
        {
            if (!Recipes.Any(x => x.Id == recipe.Id))
            {
                Recipes.Add(recipe);
            }
        }

        public void AddXP(int xp)
        {
            XP += xp;
        }

        #endregion
    }
}
