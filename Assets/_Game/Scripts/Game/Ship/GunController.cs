using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GunController : MonoBehaviour
{
    private TeamType _teamType;
    private GunAttribute _gunAttribute;
    private bool _canShoot;
    private float _nextFireInterval;
    private float _fireTimer;

    private void OnDisable()
    {
        _fireTimer = 0;
    }
    private void Update()
    {
        _fireTimer += Time.deltaTime;

        if (_canShoot && _fireTimer >= _nextFireInterval)
        {
            _fireTimer = 0;
            UpdateNextFireInterval();
            Shoot();
        }
    }

    private void Shoot()
    {
        var bullet = BulletManager.Instance.CreateBullet();
        bullet.transform.rotation = transform.rotation;
        bullet.transform.position = transform.position;
        bullet.InitBullet(_teamType, _gunAttribute.BulletDamage, _gunAttribute.BulletSpeed);
    }

    public void SetAttribute(GunAttribute gunAttribute)
    {
        _gunAttribute = gunAttribute;
        UpdateNextFireInterval();
    }

    private void UpdateNextFireInterval() => _nextFireInterval = Random.Range(_gunAttribute.FireIntervalMin, _gunAttribute.FireIntervalMax);

    public void SetCanShoot(bool canShoot)
    {
        this._canShoot = canShoot;
    }
    public void SetTeam(TeamType teamType)
    {
        _teamType = teamType;
    }
}
