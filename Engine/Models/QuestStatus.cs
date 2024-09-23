namespace Engine.Models
{
    public class QuestStatus
    {
        public QuestStatus(Quest quest)
        {
            Quest = quest;
            IsDone = false;
        }

        public Quest Quest { get; set; }
        public bool IsDone { get; set; }


    }
}
