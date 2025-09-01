using UnityEngine;

namespace Application.ShootSystem
{
    public class Projectile : PoolEntity
    {
        [field: SerializeField] public ProjectileTypes ProjectileType { get; private set; }

        private void Update()
        {
            transform.position += Vector3.up * Time.deltaTime;
        }
    }
}
