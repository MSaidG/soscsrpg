using Engine.Services;

namespace Engine.Models
{
    public class Inventory
    {
        private readonly List<GameItem> _backingInventory = new List<GameItem>();
        private readonly List<GroupedInventoryItem> _backingGroupedInventoryItems = new List<GroupedInventoryItem>();

        public IReadOnlyCollection<GameItem> Items => _backingInventory.AsReadOnly();
        public IReadOnlyList<GroupedInventoryItem> GroupedInventory => _backingGroupedInventoryItems.AsReadOnly();
        public IReadOnlyList<GameItem> Weapons => _backingInventory.ItemsThatAre(GameItem.ItemType.Weapon).AsReadOnly();
        public IReadOnlyList<GameItem> Consumables => _backingInventory.ItemsThatAre(GameItem.ItemType.Consumable).AsReadOnly();
        public bool HasConsumable => Consumables.Any();

        public Inventory(IEnumerable<GameItem> items = null)
        {
            if (items == null) return;

            foreach (GameItem item in items)
            {
                _backingInventory.Add(item);
                AddItemToGroupedInventory(item);
            }
        }

        public bool HasAllTheseItems(IEnumerable<ItemQuantity> items)
        {
            return items.All(item => Items.Count(i => i.Id == item.ItemID) >= item.Quantity);
        }

        private void AddItemToGroupedInventory(GameItem item)
        {
            if (item.IsUnique)
            {
                _backingGroupedInventoryItems.Add(new GroupedInventoryItem(item, 1));
            }
            else
            {
                if (_backingGroupedInventoryItems.All(gi => gi.Item.Id != item.Id))
                {
                    _backingGroupedInventoryItems.Add(new GroupedInventoryItem(item, 0));
                }
                _backingGroupedInventoryItems.First(gi => gi.Item.Id == item.Id).Count++;
            }
        }
    }
}
