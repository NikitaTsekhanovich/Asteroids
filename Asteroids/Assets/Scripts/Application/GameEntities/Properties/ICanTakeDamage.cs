namespace Application.GameEntities.Properties
{
    public interface ICanTakeDamage
    {
        public GameEntityTypes GameEntityType { get; }
        public void TakeDamage(int damage);
    }
}
