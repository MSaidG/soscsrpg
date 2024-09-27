using Engine.Models;

namespace Engine.Actions
{
    public class Heal : BaseAction, IAction
    {

        public readonly GameItem _item;
        private readonly int _hitPoints;
        public Heal(GameItem itemInUse, int hitPoints) : base(itemInUse)
        {
            if (itemInUse.Type != GameItem.ItemType.Consumable)
            {
                throw new ArgumentException($"{itemInUse} is not consumable.");
            }
            _hitPoints = hitPoints;
        }

        public void Execute(LivingEntity actor, LivingEntity target)
        {
            string actorName = (actor is Player) ? "You" :
                $"The {actor.Name.ToLower()}";
            string targetName = (target is Player) ? "yourself" :
                $"the {target.Name.ToLower()}";

            ReportResult($"{actorName} heal {targetName} for {_hitPoints}.");
            target.Heal(_hitPoints);
        }

    }
}
