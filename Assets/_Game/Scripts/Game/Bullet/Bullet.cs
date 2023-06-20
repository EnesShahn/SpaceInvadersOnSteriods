using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Bullet : MonoBehaviour
{
    private TeamType _teamType;
    private Rigidbody2D _rb;
    private int _bulletDamage;
    private float _bulletSpeed;
    private float _bulletTimer;
    public TeamType TeamType => _teamType;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void DOUpdate()
    {
        _rb.velocity = transform.up * _bulletSpeed;

        _bulletTimer += Time.deltaTime;
        if (_bulletTimer > 10)
        {
            BulletManager.Instance.DestroyBullet(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ship"))
        {
            var otherShipController = other.gameObject.GetComponent<ShipController>();
            if (otherShipController.TeamType == _teamType) return;

            otherShipController.DODamage(_bulletDamage);
            BulletManager.Instance.DestroyBullet(this);
        }
        else if (other.gameObject.CompareTag("Bullet"))
        {
            var otherBullet = other.gameObject.GetComponent<Bullet>();
            if (otherBullet.TeamType == _teamType) return;

            BulletManager.Instance.DestroyBullet(this);
        }
    }


    public void InitBullet(TeamType teamType, int bulletDamage, float bulletSpeed)
    {
        _bulletTimer = 0;
        _teamType = teamType;
        _bulletDamage = bulletDamage;
        _bulletSpeed = bulletSpeed;
    }

}
