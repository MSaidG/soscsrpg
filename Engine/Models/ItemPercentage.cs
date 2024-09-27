namespace Engine.Models
{
    public class ItemPercentage
    {
        public ItemPercentage(int iD, int percentage)
        {
            ID = iD;
            Percentage = percentage;
        }

        public int ID{ get; }
        public int Percentage { get; }
    }
}
