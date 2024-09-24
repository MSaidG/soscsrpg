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
        public List<MonsterEncounter> MonsterEncounterAvailable { get; set; }
            = new List<MonsterEncounter>();

        public void AddMonster(int monsterID, int chanceOfEncountering)
        {
            if (MonsterEncounterAvailable.Exists(
                m => m.MonsterID == monsterID))
            {
                MonsterEncounterAvailable.First(
                    m => m.MonsterID == monsterID).
                    ChanceOfEncountering = chanceOfEncountering;
            }
            else
            {
                MonsterEncounterAvailable.Add(
                    new MonsterEncounter(monsterID, chanceOfEncountering));
            }
        }

        public Monster? GetMonster()
        {
            if (!MonsterEncounterAvailable.Any())
            {
                return null;
            }

            int totalChances = MonsterEncounterAvailable.
                Sum(m => m.ChanceOfEncountering);

            int randomNumber = RandomNumberGenerator.
                NumberBetween(1, totalChances);

            int runningTotal = 0;
            foreach (MonsterEncounter monsterEncounter in MonsterEncounterAvailable)
            {
                runningTotal += monsterEncounter.ChanceOfEncountering;
                if (randomNumber <= runningTotal)
                {
                    return MonsterFactory.
                        GetMonster(monsterEncounter.MonsterID);
                }
            }

            return MonsterFactory.GetMonster(
                MonsterEncounterAvailable.Last().MonsterID);
        }

    }
}
