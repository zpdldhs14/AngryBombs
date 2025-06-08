using UnityEngine;

public class Projectile : MonoBehaviour
{
    [System.Serializable]
    public class ProjectileData
    {
        public GameObject projectile;
        public float mass = 10.0f;
        public int damage = 10;
        public float explosionRadius = 3f;
        public float lifetime = 3.0f;
        public LayerMask damageable;
    }
    
    [SerializeField] private ProjectileData projectileData;

    public GameObject CreateProjectile(Vector3 position, Quaternion rotation, Vector3 force)
    {
        GameObject projectile = Instantiate(projectileData.projectile, position, rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.mass = projectileData.mass;
            rb.AddForce(force, ForceMode.Impulse);
        }

        Bomb bomb = projectile.GetComponent<Bomb>();
        if (bomb != null)
        {
            bomb.explosionDamage = projectileData.damage;
            bomb.explosionRadius = projectileData.explosionRadius;
            bomb.damageableLayers = projectileData.damageable;
        }
        Destroy(projectile,projectileData.lifetime);
        return projectile;
    }
}