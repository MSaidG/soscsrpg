namespace Engine.Models
{
    public class GameItem
    {
        public int Id {  get; set; }
        public string Name {  get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public GameItem(int id, string name, string description, int price)
        {
            Id = id;
            Name = name;    
            Description = description;
            Price = price;
        }

        public GameItem Clone()
        {
            return new GameItem(Id, Name, Description, Price);
        }

    }
}
