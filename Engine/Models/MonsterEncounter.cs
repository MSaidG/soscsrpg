﻿namespace Engine.Models
{
    public class MonsterEncounter
    {
        public MonsterEncounter(int monsterID, int chanceOfEncountering)
        {
            MonsterID = monsterID;
            ChanceOfEncountering = chanceOfEncountering;
        }

        public int MonsterID { get; set; }
        public int ChanceOfEncountering { get; set; }
    }
}
