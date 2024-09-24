using System.Windows;
using System.Windows.Documents;
using Engine.EventArgs;
using Engine.ViewModels;

namespace soscsrpg
{
    public partial class MainWindow : Window
    {
        private readonly GameSession _gameSession = new GameSession();
        public MainWindow()
        {
            InitializeComponent();

            _gameSession.OnMessageRaised += OnGameMessageRaised;
            DataContext = _gameSession;
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
            _gameSession.player.XP += 10;
        }

        private void AttackMonster_Click(object sender, RoutedEventArgs e)
        {
            _gameSession.AttackCurrentMonster();
        }
    }
}