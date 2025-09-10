using Application.GameEntities.Properties;

namespace Application.GameEntitiesComponents.ShootSystem.Projectiles
{
    public class Bullet : Projectile
    {
        protected override void DealDamage(ICanTakeDamage damageTaker)
        {
            base.DealDamage(damageTaker);
            ReturnToPool();
        }
    }
}
