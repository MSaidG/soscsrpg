namespace Engine.Models
{
    public class GroupedInventoryItem : BaseNotification
    {
        private GameItem _item;
        private int _count;

        public GroupedInventoryItem(GameItem item, int count)
        {
            Item = item;
            Count = count;
        }

        public GameItem Item
        {
            get { return _item; }
            set
            {
                _item = value;
                OnPropertyChanged();
            }
        }

        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;
                OnPropertyChanged();
            }
        }


    }
}
