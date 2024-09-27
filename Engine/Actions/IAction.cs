using Engine.Models;

namespace Engine.Actions
{
    public interface IAction
    {
        public event EventHandler<string> OnActionPerformed;
        public void Execute(LivingEntity actor, LivingEntity target);
    }
}
