namespace Engine.Models
{
    internal class Weapon : GameItem
    {
        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public Weapon(int id, string name, string description, int price, int minDamage, int maxDamage) 
            : base(id, name, description, price)
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
