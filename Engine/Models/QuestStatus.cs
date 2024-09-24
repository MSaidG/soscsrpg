namespace Engine.Models
{
    public class QuestStatus
    {
        public QuestStatus(Quest quest)
        {
            PlayerQuest = quest;
            IsDone = false;
        }

        public Quest PlayerQuest { get; set; }
        public bool IsDone { get; set; }


    }
}
