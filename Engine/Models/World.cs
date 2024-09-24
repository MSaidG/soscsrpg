namespace Engine.Models
{
    public class World
    {
        private List<Location> _locations = new List<Location>();

        internal void AddLocation(int xCoordinate,
            int yCoordinate, string name,
            string description, string imagePath)
        {
            Location loc = new Location();
            loc.XCoordinate = xCoordinate;
            loc.YCoordinate = yCoordinate;
            loc.Name = name;
            loc.Description = description;  
            loc.ImagePath = $"/soscsrpg;component/Images/Locations/{imagePath}";
            _locations.Add(loc);
        }


        public Location LocationAt(int xCoordinate, int yCoordinate)
        {
            foreach (Location loc in _locations)
            {
                if (loc.XCoordinate == xCoordinate &&
                    loc.YCoordinate == yCoordinate) return loc;
            }
            return null;
        }

        public Location LocationAt(string name)
        {
            foreach (Location loc in _locations)
            {
                if (loc.Name.Equals(name)) return loc;
            }
            return null;
        }
    }
}
