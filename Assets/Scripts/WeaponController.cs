using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform _muzzle;
    [SerializeField] private float _reloadingTime;
    [SerializeField] private bool _autoShooting;
    [SerializeField] private float _minReloadTime;
    [SerializeField] private float _maxReloadTime;
    public GameObject BulletPrefab;
    private bool _ready;
    private float _currentReloadingTime;

    private void Start()
    {
        RandomizeReloading();
        StartReloading();
    }

    private void Update()
    {
        if (_ready)
        {
            if (_autoShooting || Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }
        }
        else
        {
            if (_currentReloadingTime < 0f)
            {
                _ready = true;
            }
            else
            {
                _currentReloadingTime -= Time.deltaTime;
            }
        }
    }


    private void Shoot()
    {
        GameObject bullet = Instantiate(BulletPrefab, _muzzle.position, _muzzle.rotation);
        bullet.GetComponent<BulletController>().Shoot();
        _ready = false;
        RandomizeReloading();
        StartReloading();
    }

    private void RandomizeReloading()
    {
        if (_autoShooting)
        {
            _reloadingTime =
                Random.Range(_minReloadTime, _maxReloadTime);
        }
    }

    private void StartReloading()
    {
        _ready = false;
        _currentReloadingTime = _reloadingTime;
    }
}