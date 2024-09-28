using Engine.Models;
using Engine.Factories;

namespace Engine.Actions
{
    public class AttackWithWeapon : BaseAction, IAction
    {

        private readonly GameItem _weapon;
        private readonly int _minDamage;
        private readonly int _maxDamage;

        public AttackWithWeapon(GameItem itemInUse, int minDamage, int maxDamage) : base(itemInUse)
        {
            if (itemInUse.Type != GameItem.ItemType.Weapon)
            {
                throw new ArgumentException($"{itemInUse.Name} is not a itemInUse");
            }
            if (minDamage < 0)
            {
                throw new ArgumentException("minimumDamage must be 0 or larger");
            }
            if (maxDamage < minDamage)
            {
                throw new ArgumentException("maximumDamage must be >= minimumDamage");
            }
            _minDamage = minDamage;
            _maxDamage = maxDamage;
        }

        public void Execute(LivingEntity actor, LivingEntity target)
        {
            string actorName = (actor is Player) ? "You" :
                $"The {actor.Name.ToLower()}";
            string targetName = (target is Player) ? "you" :
                $"the {target.Name.ToLower()}";

            int damage = RandomNumberGenerator.NumberBetween(_minDamage, _maxDamage);
            if (damage == 0)
            {
                ReportResult($"{actorName} missed the {targetName}.");
            }
            else
            {
                ReportResult($"{actorName} hit the {targetName} for {damage} points.");
                target.TakeDamage(damage);
            }
        }

    }
}
