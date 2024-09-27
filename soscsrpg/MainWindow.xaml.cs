using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Engine.EventArgs;
using Engine.Models;
using Engine.ViewModels;

namespace soscsrpg
{
    public partial class MainWindow : Window
    {
        private readonly GameSession _gameSession = new GameSession();
        private readonly Dictionary<Key, Action> _userInputActions =
            new Dictionary<Key, Action>();
        public MainWindow()
        {
            InitializeComponent();
            InitializeUserInputActions();

            _gameSession.OnMessageRaised += OnGameMessageRaised;
            DataContext = _gameSession;
        }

        private void InitializeUserInputActions()
        {
            _userInputActions.Add(Key.W, () => _gameSession.MoveNorth());
            _userInputActions.Add(Key.D, () => _gameSession.MoveEast());
            _userInputActions.Add(Key.A, () => _gameSession.MoveWest());
            _userInputActions.Add(Key.S, () => _gameSession.MoveSouth());
            _userInputActions.Add(Key.Z, () => _gameSession.AttackCurrentMonster());
            _userInputActions.Add(Key.C, () => _gameSession.UseCurrentConsumbale());
            _userInputActions.Add(Key.I, () => SetTabFocusTo("InventoryTabItem"));
            _userInputActions.Add(Key.Q, () => SetTabFocusTo("QuestsTabItem"));
            _userInputActions.Add(Key.R, () => SetTabFocusTo("RecipesTabItem"));
            _userInputActions.Add(Key.T, () => Trade_Click(this, 
                new RoutedEventArgs()));
        }

        private void SetTabFocusTo(string tabName)
        {
            foreach (object item in PlayerDataTabControl.Items)
            {
                if (item is TabItem tabItem)
                {
                    if (tabItem.Name == tabName)
                    {
                        tabItem.IsSelected = true;
                        return;
                    }
                }
            }
        }

        private void OnGameMessageRaised(object? sender, GameMessageEventArgs e)
        {
            GameMessages.Document.Blocks.Add(
                new Paragraph(
                    new Run(e.Message)));
            GameMessages.ScrollToEnd();
        }

        private void MoveNorth_Click(object sender, RoutedEventArgs e)
        {
            _gameSession.MoveNorth();
        }

        private void MoveWest_Click(object sender, RoutedEventArgs e)
        {
            _gameSession.MoveWest();
        }

        private void MoveEast_Click(object sender, RoutedEventArgs e)
        {
            _gameSession.MoveEast();
        }

        private void MoveSouth_Click(object sender, RoutedEventArgs e)
        {
            _gameSession.MoveSouth();
        }

        private void AddXP_Click(object sender, RoutedEventArgs e)
        {
            _gameSession.Player.AddXP(100);
        }

        private void AttackMonster_Click(object sender, RoutedEventArgs e)
        {
            _gameSession.AttackCurrentMonster();
        }

        private void Trade_Click(object sender, RoutedEventArgs e)
        {
            TradeScreen tradeScreen = new TradeScreen();
            tradeScreen.Owner = this;
            tradeScreen.DataContext = _gameSession;
            tradeScreen.ShowDialog();
        }

        private void UseConsumable_Click(object sender, RoutedEventArgs e)
        {
            _gameSession.UseCurrentConsumbale();
        }

        private void Craft_Click(object sender, RoutedEventArgs e)
        {
            Recipe recipe = ((FrameworkElement)sender).DataContext as Recipe;
            _gameSession.CraftItemUsing(recipe);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (_userInputActions.ContainsKey(e.Key))
            {
                _userInputActions[e.Key].Invoke();
            }
        }
    }
}