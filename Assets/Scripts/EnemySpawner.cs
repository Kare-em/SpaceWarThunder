using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _spawnAnchor;
    [SerializeField] private int _xCount = 10;
    [SerializeField] private int _yCount = 10;
    [SerializeField] private float _xStep = 1f;
    [SerializeField] private float _yStep = 1f;
    private int _currentEnemies;
    private List<GameObject> _enemies;
    private bool _spawned;

    private void Start()
    {
        SpawnEnemies();
        HealthController.OnDead += OnDead;
    }

    private void OnDestroy()
    {
        HealthController.OnDead -= OnDead;
    }

    private void OnDead(string obj)
    {
        if (obj == "Enemy")
            DecreaseEnemies();
    }

    private void SpawnEnemies()
    {
        if (_spawned)
        {
            foreach (var enemy in _enemies)
            {
                enemy.SetActive(true);
            }

            return;
        }

        _currentEnemies = 0;
        _enemies = new List<GameObject>();
        for (int i = 0; i < _yCount; i++)
        {
            for (int j = 0; j < _xCount; j++)
            {
                var position = _spawnAnchor.position + new Vector3(_xStep * j, _yStep * i, 0f);
                var enemy = Instantiate(_enemyPrefab, position,
                    Quaternion.identity, _spawnAnchor);
                _enemies.Add(enemy);
                _currentEnemies++;
            }
        }

        _spawned = true;
    }

    private void DecreaseEnemies()
    {
        _currentEnemies--;
        if (_currentEnemies == 0)
            SpawnEnemies();
    }
}