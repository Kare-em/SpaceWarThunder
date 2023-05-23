using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private int _damage = 1;
    public GameObject Prefab;
    public bool Bonus;

    public void Shoot()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * _bulletSpeed;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Bonus)
        {
            if (other.TryGetComponent(out WeaponController weapon))
                weapon.BulletPrefab = Prefab;
            gameObject.SetActive(false);
        }
        else
        {
            DealDamage(other);
        }
    }

    private void DealDamage(Collider2D other)
    {
        HealthController vehicle = other.GetComponent<HealthController>();
        if (vehicle != null)
        {
            vehicle.TakeDamage(_damage);
            gameObject.SetActive(false);
        }
    }
}