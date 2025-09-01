using Application.GameEntities.Properties;

namespace Application.ShootSystem.Projectiles
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
