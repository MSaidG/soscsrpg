using System.Windows;
using Engine.ViewModels;

namespace soscsrpg
{
    public partial class MainWindow : Window
    {
        private GameSession _gameSession;
        public MainWindow()
        {
            InitializeComponent();
            _gameSession = new GameSession();
            DataContext = _gameSession;
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
            _gameSession.player.ExperiencePoints += 10;
        }
    }
}