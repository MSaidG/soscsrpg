using Engine.Actions;

namespace Engine.Models
{
    public class GameItem
    {
        public enum ItemType
        {
            Miscellaneous, 
            Weapon,
            Consumable,
        }

        public ItemType Type { get; }
        public IAction Action { get; set; }
        public int Id {  get; }
        public string Name {  get; }
        public string Description { get; }
        public int Price { get; }
        public bool IsUnique { get; }
        public int MaxDamage { get; }
        public int MinDamage { get; }

        public GameItem(ItemType type, int id, string name, string description, int price, 
            bool isUnique=false, IAction action=null)
        {
            Type = type;
            Id = id;
            Name = name;    
            Description = description;
            Price = price;
            IsUnique = isUnique;
            Action = action;
        }

        public void PerformAction(LivingEntity actor, LivingEntity target)
        {
            Action?.Execute(actor, target);
        }

        public GameItem Clone()
        {
            return new GameItem(Type, Id, Name, Description, Price, 
                IsUnique, Action);
        }

    }
}
