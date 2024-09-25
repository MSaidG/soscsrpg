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
            if (((FrameworkElement)sender).DataContext is GameItem item)
            {
                Session.player.Gold += item.Price;
                Session.CurrentTrader.AddItemToInventory(item);
                Session.player.RemoveItemFromInventory(item);
            }
        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is GameItem item)
            {
                if (Session.player.Gold >= item.Price)
                {
                    Session.player.Gold -= item.Price;
                    Session.CurrentTrader.RemoveItemFromInventory(item);
                    Session.player.AddItemToInventory(item);
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
