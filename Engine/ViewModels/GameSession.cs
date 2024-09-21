using Engine.Models;

namespace Engine.ViewModels
{
    public class GameSession
    {
        public Player player {  get; set; }

        public GameSession()
        {
            player = new Player();
            player.Name = "Said";
            player.CharacterClass = "Fighter";
            player.HitPoints = 10;
            player.Gold = 1000;
            player.ExperiencePoints = 0;
            player.Level = 1;
        }
    }
}
