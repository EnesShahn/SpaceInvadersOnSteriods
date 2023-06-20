using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] private TeamType _teamType;
    [SerializeField] private DataContainer _data;
    [SerializeField] private GunController _gunController;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private BoxCollider2D _collider;

    private int _currentHealth;

    public TeamType TeamType => _teamType;
    public SpriteRenderer SpriteRenderer => _spriteRenderer;
    public BoxCollider2D Collider => _collider;

    public event Action<int> OnShipDamaged;
    public event Action OnShipDestroyed;

    private void Awake()
    {
        _gunController.SetAttribute(_data.GetAttribute<GunAttribute>());
        _gunController.SetCanShoot(true);
        _gunController.SetTeam(_teamType);
    }
    private void OnEnable()
    {
        _currentHealth = _data.GetAttribute<ShipAttribute>().Health;
    }

    public void DODamage(int damage)
    {
        if (_currentHealth <= 0) return;

        _currentHealth -= damage;
        OnShipDamaged?.Invoke(damage);
        CheckHealth();
    }

    private void CheckHealth()
    {
        if (_currentHealth <= 0)
        {
            OnShipDestroyed?.Invoke();
        }
    }

}

public enum TeamType
{
    Undefined = -1,
    Player,
    Enemy
}