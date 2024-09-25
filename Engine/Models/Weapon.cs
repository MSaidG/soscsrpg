namespace Engine.Models
{
    public class Weapon : GameItem
    {
        public int MinDamage { get; }
        public int MaxDamage { get; }
        public Weapon(int id, string name, string description, int price, int minDamage, int maxDamage) 
            : base(id, name, description, price, true)
        {
            MinDamage = minDamage;
            MaxDamage = maxDamage;
        }

        public new Weapon Clone()
        {
            return new Weapon(Id, Name, Description, 
                Price, MinDamage, MaxDamage);
        }
    }
}
