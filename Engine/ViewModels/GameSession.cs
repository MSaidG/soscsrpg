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
        private Monster? currentMonster;
        private Trader _currentTrader;
        private Location location;
        public Player player {  get; set; }
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

        public Monster? CurrentMonster
        {
            get { return currentMonster; }
            set
            {
                currentMonster = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasMonster));
                if (currentMonster != null)
                {
                    RaiseMessage("");
                    RaiseMessage($"You see a " +
                        $"{currentMonster.Name} here!");
                }
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
            player = new Player()
            {
                Name = "Said",
                CharacterClass = "Fighter",
                CurrentHitPoints = 10,
                MaxHitPoints = 10,
                Gold = 1000,
                XP = 0,
                Level = 1
            };

            if (!player.Weapons.Any())
            {
                player.AddItemToInventory(
                    ItemFactory.CreateGameItem(1001));
            }

            world = WorldFactory.CreateWorld();
            Location = world.LocationAt("Home");

            //player.Inventory.Add(ItemFactory.CreateGameItem(1001));
            //player.Inventory.Add(ItemFactory.CreateGameItem(1002));
            //player.Inventory.Add(ItemFactory.CreateGameItem(1001));
            //player.Inventory.Add(ItemFactory.CreateGameItem(1000));
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
                if (!player.Quests.
                    Any(q => q.PlayerQuest.Id == quest.Id))
                {
                    player.Quests.Add(new QuestStatus(quest));
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
                    player.Quests.FirstOrDefault(
                        q => q.PlayerQuest.Id == quest.Id &&
                        !q.IsDone);

                if (questToComplete != null)
                {
                    if (player.HasAllTheseItems(quest.ItemsToComplete))
                    {

                        foreach (ItemQuantity itemQuantity in quest.ItemsToComplete)
                        {
                            for (int i = 0; i < itemQuantity.Quantity; i++)
                            {
                                player.RemoveItemFromInventory(
                                    player.Inventory.First
                                    (item => item.Id == itemQuantity.ItemID));
                            }
                        }

                        RaiseMessage("");
                        RaiseMessage($"You completed the '{quest.Name}' quest.");

                        player.XP += quest.RewardXP;
                        RaiseMessage($"You receive {quest.RewardXP} XP.");

                        player.Gold += quest.RewardGold;
                        RaiseMessage($"You receive {quest.RewardGold} gold.");

                        foreach (ItemQuantity itemQuantity in quest.RewardItems)
                        {
                            GameItem? rewardItem = ItemFactory.
                                CreateGameItem(itemQuantity.ItemID);
                            if (rewardItem != null)
                            {
                                player.AddItemToInventory(rewardItem);
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
                CurrentMonster.CurrentHitPoints -= damageToMonster;
                RaiseMessage($"You hit the {CurrentMonster.Name} " +
                    $"dor {damageToMonster} points.");
            }

            if (CurrentMonster.CurrentHitPoints <= 0)
            {
                RaiseMessage("");
                RaiseMessage($"You defeated the {CurrentMonster.Name}!");

                player.XP += currentMonster.RewardXP;
                RaiseMessage($"You gain {currentMonster.RewardXP} XP.");

                player.Gold += currentMonster.Gold;
                RaiseMessage($"You gain {currentMonster.Gold} gold.");

                foreach (GameItem item in CurrentMonster.Inventory)
                {
                    player.AddItemToInventory(item);
                    RaiseMessage($"You found one {item.Name}.");
                }

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
                    player.CurrentHitPoints -= damageToPlayer;
                    RaiseMessage($"The {CurrentMonster.Name} " +
                        $"hit you for {damageToPlayer} hit points.");
                }

                if (player.CurrentHitPoints <= 0)
                {
                    RaiseMessage("");
                    RaiseMessage($"The {CurrentMonster.Name} " +
                        $"killed you.");


                    Location = world.LocationAt(0, -1);
                    player.CurrentHitPoints = player.Level * 10;
                    RaiseMessage("");
                    RaiseMessage($"You revived at your home.");
                }
            }
        }
    }
}
