using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BulletManager : Singleton<BulletManager>
{
    [SerializeField] private Bullet _bulletPrefab;
    private List<Bullet> _activeBullets = new List<Bullet>();
    private ComponentPool<Bullet> _bulletPool;


    private void Awake()
    {
        _bulletPool = new ComponentPool<Bullet>(_bulletPrefab, 100, 20);
    }
    private void Update()
    {
        for (int i = 0; i < _activeBullets.Count; i++)
        {
            _activeBullets[i].DOUpdate();
        }
    }

    public Bullet CreateBullet()
    {
        var bullet = _bulletPool.RequestObject();
        _activeBullets.Add(bullet);
        return bullet;
    }

    public void DestroyBullet(Bullet bullet)
    {
        bool removed = _activeBullets.Remove(bullet);
        if (removed)
            _bulletPool.ReturnObject(bullet);
    }

    public void DestroyAllBullets()
    {
        foreach (var bullet in _activeBullets)
        {
            _bulletPool.ReturnObject(bullet);
        }
        _activeBullets.Clear();
    }
}
