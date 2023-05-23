using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class HealthController : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 5;
    [SerializeField] private GameObject[] _bonusBullets;
    public static Action<string> OnDead;
    private int currentHealth;

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void OnEnable()
    {
        SetMaxHealth();
    }

    private void SetMaxHealth()
    {
        currentHealth = _maxHealth;
    }

    private void Die()
    {
        if (_bonusBullets.Length > 0)
        {
            SpawnBonus();
        }

        OnDead?.Invoke(gameObject.tag);
        gameObject.SetActive(false);
    }

    private void SpawnBonus()
    {
        var range = Random.Range(0, 10);
        if (range < _bonusBullets.Length)
        {
            var bulletControllerPrefab = _bonusBullets[range];
            var bullet = Instantiate(bulletControllerPrefab, transform.position, Quaternion.identity);
            var bulletController = bullet.GetComponent<BulletController>();
            bulletController.Bonus = true;
            bulletController.Prefab = bulletControllerPrefab;
            bullet.layer = 6; //Player
        }
    }
}