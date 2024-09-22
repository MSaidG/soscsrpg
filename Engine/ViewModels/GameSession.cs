using Engine.Factories;
using Engine.Models;
using System.ComponentModel;
using System.Diagnostics;

namespace Engine.ViewModels
{
    public class GameSession : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public Player player {  get; set; }
        public World world { get; set; }
        private Location location { get; set; }
        public Location Location
        {
            get { return location; }
            set
            {
                location = value;

                OnPropertyChanged("Location");
                OnPropertyChanged("HasLocationToNorth");
                OnPropertyChanged("HasLocationToEast");
                OnPropertyChanged("HasLocationToWest");
                OnPropertyChanged("HasLocationToSouth");

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
            player = new Player();
            player.Name = "Said";
            player.CharacterClass = "Fighter";
            player.HitPoints = 10;
            player.Gold = 1000;
            player.ExperiencePoints = 0;
            player.Level = 1;

            WorldFactory factory = new WorldFactory();
            world = factory.CreateWorld();
            Location = world.LocationAt("Home");
        }


        public void MoveNorth()
        {
            Location = world.LocationAt(Location.XCoordinate,
                                        Location.YCoordinate + 1);
        }

        public void MoveSouth() 
        {
            Location = world.LocationAt(Location.XCoordinate,
                                        Location.YCoordinate - 1);
        }

        public void MoveWest()
        {
            Location = world.LocationAt(Location.XCoordinate - 1,
                                        Location.YCoordinate);
        }

        public void MoveEast()
        {
            Location = world.LocationAt(Location.XCoordinate + 1,
                                        Location.YCoordinate);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
