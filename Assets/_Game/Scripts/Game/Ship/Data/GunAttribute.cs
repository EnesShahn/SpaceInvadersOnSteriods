using UnityEngine;

public class GunAttribute : BaseAttribute
{
    [SerializeField] private int _bulletDamage;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _fireIntervalMin;
    [SerializeField] private float _fireIntervalMax;

    public int BulletDamage => _bulletDamage;
    public float BulletSpeed => _bulletSpeed;
    public float FireIntervalMin => _fireIntervalMin;
    public float FireIntervalMax => _fireIntervalMax;
}