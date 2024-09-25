using System.Windows;
using Engine.Models;
using Engine.ViewModels;


namespace soscsrpg
{
    public partial class TradeScreen : Window
    {
        public GameSession Session => (GameSession)DataContext;
        public TradeScreen()
        {
            InitializeComponent();
        }

        private void Sell_Click(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is GroupedInventoryItem groupInventory)
            {
                Session.Player.ReceiveGold(groupInventory.Item.Price);
                Session.CurrentTrader.AddItemToInventory(groupInventory.Item);
                Session.Player.RemoveItemFromInventory(groupInventory.Item);
            }
        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is GroupedInventoryItem groupInventory)
            {
                if (Session.Player.Gold >= groupInventory.Item.Price)
                {
                    Session.Player.SpendGold(groupInventory.Item.Price);
                    Session.CurrentTrader.RemoveItemFromInventory(groupInventory.Item);
                    Session.Player.AddItemToInventory(groupInventory.Item);
                }
                else
                {
                    MessageBox.Show("You do not have enough gold.");
                }
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
