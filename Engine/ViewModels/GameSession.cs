using Engine.EventArgs;
using Engine.Factories;
using Engine.Models;
using System.Diagnostics;

namespace Engine.ViewModels
{
    public class GameSession : BaseNotification
    {
        public event EventHandler<GameMessageEventArgs> OnMessageRaised;

        #region Properties
        private Monster? _currentMonster;
        private Trader _currentTrader;
        private Location location;
        private Player _player;
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
            get { return _currentMonster; }
            set
            {
                if (_currentMonster != null)
                {
                    _currentMonster.OnKilled -= OnCurrentMonsterKilled;
                }

                _currentMonster = value;
                if (_currentMonster != null)
                {
                    _currentMonster.OnKilled += OnCurrentMonsterKilled;
                    RaiseMessage("");
                    RaiseMessage($"You see a {CurrentMonster.Name} here!");
                }
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasMonster));
            }
        }

        private void OnPlayerLeveledUp(object? sender, System.EventArgs e)
        {
            RaiseMessage($"You are now level {Player.Level}.");
        }

        private void OnPlayerKilled(object sender, System.EventArgs eventArgs)
        {   
            RaiseMessage("");
            RaiseMessage($"The {CurrentMonster.Name} killed you.");

            Location = world.LocationAt(0, -1);
            Player.HealFull();
        }
        private void OnCurrentMonsterKilled(object sender, System.EventArgs eventArgs)
        {
            RaiseMessage("");
            RaiseMessage($"You defeated the {CurrentMonster.Name}!");

            RaiseMessage($"You receive {CurrentMonster.RewardXP} experience points.");
            Player.AddXP(CurrentMonster.RewardXP);

            RaiseMessage($"You receive {CurrentMonster.Gold} gold.");
            Player.ReceiveGold(CurrentMonster.Gold);

            foreach (GameItem gameItem in CurrentMonster.Inventory)
            {
                RaiseMessage($"You receive one {gameItem.Name}.");
                Player.AddItemToInventory(gameItem);
            }
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
                GetMonsterAtLocation();

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

        public Weapon CurrentWeapon { get; set; }

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

            if (!Player.Weapons.Any())
            {
                Player.AddItemToInventory(
                    ItemFactory.CreateGameItem(1001));
            }

            world = WorldFactory.CreateWorld();
            Location = world.LocationAt("Home");

            //Player.Inventory.Add(ItemFactory.CreateGameItem(1001));
            //Player.Inventory.Add(ItemFactory.CreateGameItem(1002));
            //Player.Inventory.Add(ItemFactory.CreateGameItem(1001));
            //Player.Inventory.Add(ItemFactory.CreateGameItem(1000));
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
                    RaiseMessage("");

                    RaiseMessage($"You receive the '{quest.Name}' quest.");
                    RaiseMessage(quest.Description);
                    RaiseMessage("Return with: ");

                    foreach (ItemQuantity itemQuantity in quest.ItemsToComplete)
                    {
                        RaiseMessage($" {itemQuantity.Quantity} " +
                            $"{ItemFactory.CreateGameItem(itemQuantity.ItemID).Name}. ");
                    }

                    RaiseMessage("And you will receive: ");
                    RaiseMessage($" {quest.RewardXP} XP.");
                    RaiseMessage($" {quest.RewardGold} gold.");

                    foreach (ItemQuantity itemQuantity in quest.RewardItems)
                    {
                        RaiseMessage($" {itemQuantity.Quantity} " +
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
                    if (Player.HasAllTheseItems(quest.ItemsToComplete))
                    {

                        foreach (ItemQuantity itemQuantity in quest.ItemsToComplete)
                        {
                            for (int i = 0; i < itemQuantity.Quantity; i++)
                            {
                                Player.RemoveItemFromInventory(
                                    Player.Inventory.First
                                    (item => item.Id == itemQuantity.ItemID));
                            }
                        }

                        RaiseMessage("");
                        RaiseMessage($"You completed the '{quest.Name}' quest.");

                        Player.AddXP(quest.RewardXP);
                        RaiseMessage($"You receive {quest.RewardXP} XP.");

                        Player.Gold += quest.RewardGold;
                        RaiseMessage($"You receive {quest.RewardGold} gold.");

                        foreach (ItemQuantity itemQuantity in quest.RewardItems)
                        {
                            GameItem? rewardItem = ItemFactory.
                                CreateGameItem(itemQuantity.ItemID);
                            if (rewardItem != null)
                            {
                                Player.AddItemToInventory(rewardItem);
                                RaiseMessage($"You receive a {rewardItem.Name}.");
                            }
                        }

                        questToComplete.IsDone = true;
                    }
                }
            }
        }

        private void GetMonsterAtLocation()
        {
            CurrentMonster = location.GetMonster();
        }

        private void RaiseMessage(string message)
        {
            OnMessageRaised?.Invoke(this, new GameMessageEventArgs(message));
        }

        public void AttackCurrentMonster()
        {
            if (CurrentWeapon == null)
            {
                RaiseMessage("You must select a weapon to attack.");
                return;
            }

            int damageToMonster = RandomNumberGenerator.NumberBetween(
                CurrentWeapon.MinDamage, CurrentWeapon.MaxDamage );

            if (damageToMonster == 0)
            {
                RaiseMessage($"You missed the {CurrentMonster.Name}.");
            }
            else
            {
                RaiseMessage($"You hit the {CurrentMonster.Name} " +
                    $"dor {damageToMonster} points.");
                CurrentMonster.TakeDamage(damageToMonster);
            }

            if (CurrentMonster.IsDead)
            {
                GetMonsterAtLocation();
            }
            else
            {
                int damageToPlayer = RandomNumberGenerator.NumberBetween(
                    CurrentMonster.MinDamage, CurrentMonster.MaxDamage );

                if (damageToPlayer == 0)
                {
                    RaiseMessage("The monster attacks, but misses you.");
                }
                else
                {
                    RaiseMessage($"The {CurrentMonster.Name} " +
                        $"hit you for {damageToPlayer} hit points.");
                    Player.TakeDamage(damageToPlayer);
                }
            }
        }
    }
}
