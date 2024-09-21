using System.Windows;
using Engine.ViewModels;

namespace soscsrpg
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameSession _gameSession;
        public MainWindow()
        {
            InitializeComponent();
            _gameSession = new GameSession();
            DataContext = _gameSession;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _gameSession.player.ExperiencePoints += 10;
        }
    }
}