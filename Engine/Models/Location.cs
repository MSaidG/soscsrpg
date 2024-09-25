using Engine.Factories;

namespace Engine.Models
{
    public class Location
    {
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImagePath { get; set; }
        public List<Quest> QuestAvailable { get; set; } 
            = new List<Quest>();
        public List<MonsterEncounter> MonsterAvailable { get; set; }
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
