namespace Engine.Models
{
    public class GameItem
    {
        public int Id {  get; }
        public string Name {  get; }
        public string Description { get; }
        public int Price { get; }
        public bool IsUnique { get; }

        public GameItem(int id, string name, string description, int price, bool isUnique=false)
        {
            Id = id;
            Name = name;    
            Description = description;
            Price = price;
            IsUnique = isUnique;
        }

        public GameItem Clone()
        {
            return new GameItem(Id, Name, Description, Price, IsUnique);
        }

    }
}
