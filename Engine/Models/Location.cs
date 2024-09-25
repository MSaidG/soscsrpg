using Engine.Factories;

namespace Engine.Models
{
    public class Location
    {

        public Location(int xCoordinate, int yCoordinate, string name, 
            string description, string imagePath)
        {
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
            Name = name;
            Description = description;
            ImagePath = imagePath;
        }

        public int XCoordinate { get; }
        public int YCoordinate { get; }
        public string? Name { get; }
        public string? Description { get; }
        public string? ImagePath { get; }
        public List<Quest> QuestAvailable { get; } 
            = new List<Quest>();
        public List<MonsterEncounter> MonsterAvailable { get; }
            = new List<MonsterEncounter>();

        public Trader Trader { get; set; }

        public void AddMonster(int monsterID, int chanceOfEncountering)
        {
            if (MonsterAvailable.Exists(
                m => m.MonsterID == monsterID))
            {
                MonsterAvailable.First(
                    m => m.MonsterID == monsterID).
                    ChanceOfEncountering = chanceOfEncountering;
            }
            else
            {
                MonsterAvailable.Add(
                    new MonsterEncounter(monsterID, chanceOfEncountering));
            }
        }

        public Monster? GetMonster()
        {
            if (!MonsterAvailable.Any())
            {
                return null;
            }

            int totalChances = MonsterAvailable.
                Sum(m => m.ChanceOfEncountering);

            int randomNumber = RandomNumberGenerator.
                NumberBetween(1, totalChances);

            int runningTotal = 0;
            foreach (MonsterEncounter monsterEncounter in MonsterAvailable)
            {
                runningTotal += monsterEncounter.ChanceOfEncountering;
                if (randomNumber <= runningTotal)
                {
                    return MonsterFactory.
                        GetMonster(monsterEncounter.MonsterID);
                }
            }

            return MonsterFactory.GetMonster(
                MonsterAvailable.Last().MonsterID);
        }

    }
}
