namespace Engine.Models
{
    public class Recipe
    {
        public Recipe(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddIngredient(int itemID, int count)
        {
            if (!Ingredients.Any(x => x.ItemID == itemID))
            {
                Ingredients.Add(new ItemQuantity(itemID, count));
            }
        }

        public void AddOutputItem(int itemID, int count)
        {
            if (!OutputItems.Any(x => x.ItemID == itemID))
            {
                OutputItems.Add(new ItemQuantity(itemID, count));
            }
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public List<ItemQuantity> Ingredients { get; set; } = new List<ItemQuantity>();
        public List<ItemQuantity> OutputItems { get; set; } = new List<ItemQuantity>();

        public string ToolTipContents =>
                "Ingredients" + Environment.NewLine +
                "===========" + Environment.NewLine +
                string.Join(Environment.NewLine, Ingredients.Select(i => i.QuantityItemDescription)) +
                Environment.NewLine + Environment.NewLine +
                "Creates" + Environment.NewLine +
                "===========" + Environment.NewLine +
                string.Join(Environment.NewLine, OutputItems.Select(i => i.QuantityItemDescription));

    }
}
