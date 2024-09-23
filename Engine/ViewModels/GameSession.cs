using Engine.Factories;
using Engine.Models;
using System.ComponentModel;
using System.Diagnostics;

namespace Engine.ViewModels
{
    public class GameSession : BaseNotification
    {
        public Player player {  get; set; }
        public World world { get; set; }
        private Location location { get; set; }
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
                GivePlayerQuestAtLocation();

                Debug.WriteLine(Location.XCoordinate.ToString() + 
                    Location.YCoordinate.ToString() + 
                    Location.Name);
                
            }
        }
        public bool HasLocationToNorth
        {
            get
            {
                return world.LocationAt(Location.XCoordinate, Location.YCoordinate + 1) != null;
            }
        }
        public bool HasLocationToEast
        {
            get
            {
                return world.LocationAt(Location.XCoordinate + 1, Location.YCoordinate) != null;
            }
        }
        public bool HasLocationToSouth
        {
            get
            {
                return world.LocationAt(Location.XCoordinate, Location.YCoordinate - 1) != null;
            }
        }
        public bool HasLocationToWest
        {
            get
            {
                return world.LocationAt(Location.XCoordinate - 1, Location.YCoordinate) != null;
            }
        }

        public GameSession()
        {
            player = new Player()
            {
                Name = "Said",
                CharacterClass = "Fighter",
                HitPoints = 10,
                Gold = 1000,
                ExperiencePoints = 0,
                Level = 1
            };

            world = WorldFactory.CreateWorld();
            Location = world.LocationAt("Home");

            player.Inventory.Add(ItemFactory.CreateGameItem(1001));
            player.Inventory.Add(ItemFactory.CreateGameItem(1002));
            player.Inventory.Add(ItemFactory.CreateGameItem(1001));
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
                if (player.Quests.
                    Any(q => q.Quest.Id == quest.Id))
                {
                    player.Quests.Add(new QuestStatus(quest));
                }
            }
        }

    }
}
