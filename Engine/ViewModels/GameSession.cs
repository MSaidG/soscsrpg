using Engine.Factories;
using Engine.Models;
using Engine.Services;
using System.Diagnostics;

namespace Engine.ViewModels
{
    public class GameSession : BaseNotification
    {
        private readonly MessageBroker _messageBroker = MessageBroker.GetInstance();
        #region Properties
        private Monster? _currentMonster;
        private Trader _currentTrader;
        private Location location;
        private Player _player;
        private Battle _currentBattle;
        public Player Player 
        {   
            get { return _player; } 
            set
            {
                if (_player != null)
                {
                    _player.OnKilled -= OnPlayerKilled;
                    _player.OnLeveledUp -= OnPlayerLeveledUp;
                }
                _player = value;
                if (_player != null)
                {
                    _player.OnKilled += OnPlayerKilled;
                    _player.OnLeveledUp += OnPlayerLeveledUp;
                }
            }
        }

        public Monster? CurrentMonster
        {
            get => _currentMonster;
            set
            {
                if (_currentBattle != null)
                {
                    _currentBattle.OnCombatVictory -= OnCurrentMonsterKilled;
                    _currentBattle.Dispose();
                }

                _currentMonster = value;
                if (_currentMonster != null)
                {
                    _currentBattle = new Battle(Player, CurrentMonster);
                    _currentBattle.OnCombatVictory += OnCurrentMonsterKilled;
                }
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasMonster));
            }
        }

        private void OnCurrentMonsterPerformedAction(object? sender, string result)
        {
            _messageBroker.RaiseMessage(result);
        }

        private void OnPlayerLeveledUp(object? sender, System.EventArgs e)
        {
            _messageBroker.RaiseMessage($"You are now level {Player.Level}.");
        }

        private void OnPlayerKilled(object sender, System.EventArgs eventArgs)
        {   
            _messageBroker.RaiseMessage("");
            //_messageBroker.RaiseMessage($"The {CurrentMonster.Name} killed you.");
            _messageBroker.RaiseMessage("You have been killed.");

            Location = world.LocationAt(0, -1);
            Player.HealFull();
        }
        private void OnCurrentMonsterKilled(object sender, System.EventArgs eventArgs)
        {
            CurrentMonster = Location.GetMonster();
        }

        public World world { get; set; }
        public Location Location
        {
            get { return location; }
            set
            {
                location = value;

                OnPropertyChanged(nameof(Location));
                OnPropertyChanged(nameof(HasLocationToEast));
                OnPropertyChanged(nameof(HasLocationToNorth));
                OnPropertyChanged(nameof(HasLocationToSouth));
                OnPropertyChanged(nameof(HasLocationToWest));
                CompleteQuestAtLocation();
                GivePlayerQuestAtLocation();
                CurrentMonster = Location.GetMonster();

                CurrentTrader = location.Trader;

                Debug.WriteLine(Location.XCoordinate.ToString() + 
                    Location.YCoordinate.ToString() + 
                    Location.Name);
                
            }
        }

        public Trader CurrentTrader
        {
            get { return _currentTrader; }
            set
            {
                _currentTrader = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(HasTrader));
            }
        }

        public bool HasTrader => CurrentTrader != null;
        public bool HasMonster => CurrentMonster != null;
        public bool HasLocationToNorth =>
                world.LocationAt(Location.XCoordinate, Location.YCoordinate + 1) != null;
        public bool HasLocationToEast =>
                world.LocationAt(Location.XCoordinate + 1, Location.YCoordinate) != null;
        public bool HasLocationToSouth =>
                world.LocationAt(Location.XCoordinate, Location.YCoordinate - 1) != null;
        public bool HasLocationToWest =>
                world.LocationAt(Location.XCoordinate - 1, Location.YCoordinate) != null;
        #endregion

        public GameSession()
        {
            Player = new Player("Said", "Fighter", 0, 10, 10, 9000);

            if (!Player.Inventory.Weapons.Any())
            {
                Player.AddItemToInventory(
                    ItemFactory.CreateGameItem(1001));
            }

            Player.AddItemToInventory(ItemFactory.CreateGameItem(2001));
            Player.LearnRecipe(RecipeFactory.GetRecipe(1));
            Player.AddItemToInventory(ItemFactory.CreateGameItem(3001));
            Player.AddItemToInventory(ItemFactory.CreateGameItem(3002));
            Player.AddItemToInventory(ItemFactory.CreateGameItem(3003));
            world = WorldFactory.CreateWorld();
            Location = world.LocationAt("Home");
        }


        public void MoveNorth()
        {
            if (HasLocationToNorth) 
                Location = world.LocationAt(Location.XCoordinate,
                                        Location.YCoordinate + 1);
        }

        public void MoveSouth() 
        {
            if (HasLocationToSouth)
                Location = world.LocationAt(Location.XCoordinate,
                                        Location.YCoordinate - 1);
        }

        public void MoveWest()
        {
            if (HasLocationToWest)
                Location = world.LocationAt(Location.XCoordinate - 1,
                                        Location.YCoordinate);
        }

        public void MoveEast()
        {
            if (HasLocationToEast)
                Location = world.LocationAt(Location.XCoordinate + 1,
                                        Location.YCoordinate);
        }

        private void GivePlayerQuestAtLocation()
        {
            foreach (Quest quest in location.QuestAvailable)
            {
                if (!Player.Quests.
                    Any(q => q.PlayerQuest.Id == quest.Id))
                {
                    Player.Quests.Add(new QuestStatus(quest));
                    _messageBroker.RaiseMessage("");

                    _messageBroker.RaiseMessage($"You receive the '{quest.Name}' quest.");
                    _messageBroker.RaiseMessage(quest.Description);
                    _messageBroker.RaiseMessage("Return with: ");

                    foreach (ItemQuantity itemQuantity in quest.ItemsToComplete)
                    {
                        _messageBroker.RaiseMessage($" {itemQuantity.Quantity} " +
                            $"{ItemFactory.CreateGameItem(itemQuantity.ItemID).Name}. ");
                    }

                    _messageBroker.RaiseMessage("And you will receive: ");
                    _messageBroker.RaiseMessage($" {quest.RewardXP} XP.");
                    _messageBroker.RaiseMessage($" {quest.RewardGold} gold.");

                    foreach (ItemQuantity itemQuantity in quest.RewardItems)
                    {
                        _messageBroker.RaiseMessage($" {itemQuantity.Quantity} " +
                            $"{ItemFactory.CreateGameItem(itemQuantity.ItemID).Name}. ");
                    }

                }
            }
        }

        private void CompleteQuestAtLocation()
        {
            foreach (Quest quest in location.QuestAvailable)
            {
                QuestStatus? questToComplete =
                    Player.Quests.FirstOrDefault(
                        q => q.PlayerQuest.Id == quest.Id &&
                        !q.IsDone);

                if (questToComplete != null)
                {
                    if (Player.Inventory.HasAllTheseItems(quest.ItemsToComplete))
                    {

                        Player.RemoveItemsFromInventory(quest.ItemsToComplete);

                        _messageBroker.RaiseMessage("");
                        _messageBroker.RaiseMessage($"You completed the '{quest.Name}' quest.");

                        Player.AddXP(quest.RewardXP);
                        _messageBroker.RaiseMessage($"You receive {quest.RewardXP} XP.");

                        Player.Gold += quest.RewardGold;
                        _messageBroker.RaiseMessage($"You receive {quest.RewardGold} gold.");

                        foreach (ItemQuantity itemQuantity in quest.RewardItems)
                        {
                            GameItem? rewardItem = ItemFactory.
                                CreateGameItem(itemQuantity.ItemID);
                            if (rewardItem != null)
                            {
                                Player.AddItemToInventory(rewardItem);
                                _messageBroker.RaiseMessage($"You receive a {rewardItem.Name}.");
                            }
                        }

                        questToComplete.IsDone = true;
                    }
                }
            }
        }

        public void AttackCurrentMonster()
        {
            _currentBattle.AttackOpponent();
        }

        public void CraftItemUsing(Recipe recipe)
        {
            if (Player.Inventory.HasAllTheseItems(recipe.Ingredients))
            {
                Player.RemoveItemsFromInventory(recipe.Ingredients);
                foreach (ItemQuantity itemQuantity in recipe.OutputItems)
                {
                    for (int i = 0; i < itemQuantity.Quantity; i++)
                    {
                        GameItem outputItem = ItemFactory.CreateGameItem(itemQuantity.ItemID);
                        Player.AddItemToInventory(outputItem);
                        _messageBroker.RaiseMessage($"You craft 1 {outputItem.Name}");
                    }
                }
            }
            else
            {
                _messageBroker.RaiseMessage("You do not have the required ingredients:");
                foreach (ItemQuantity itemQuantity in recipe.Ingredients)
                {
                    _messageBroker.RaiseMessage($"  {itemQuantity.Quantity} {ItemFactory.ItemName(itemQuantity.ItemID)}");
                }
            }
        }

        public void UseCurrentConsumbale()
        {
            if (Player.CurrentConsumable != null)
            {
                Player.UseCurrentConsumable();
            }
        }

    }
}
