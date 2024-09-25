namespace Engine.Models
{
    public class QuestStatus : BaseNotification
    {
        private bool _isDone;
        public QuestStatus(Quest quest)
        {
            PlayerQuest = quest;
            IsDone = false;
        }

        public Quest PlayerQuest { get; set; }
        public bool IsDone
        {
            get { return _isDone; }
            set
            {
                _isDone = value;
                OnPropertyChanged();
            }
        }
    }
}
