using System.Collections;
using System.Collections.Generic;

using DG.Tweening;

using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private ShipController _shipController;

    public ShipController ShipController => _shipController;


    private void Awake()
    {
        _shipController.OnShipDestroyed += OnShipDestroyed;
    }
    private void Update()
    {
        if (transform.position.y < EnemyManager.Instance.EnemyCrossLine.position.y)
        {
            EnemyManager.Instance.CrossedLine();
        }
    }

    private void OnEnable()
    {
        DOTween.Kill(transform);
    }
    private void OnShipDestroyed()
    {
        _shipController.Collider.enabled = false;
        transform.DOScale(Vector3.zero, 0.1f).OnComplete(() =>
        {
            EnemyManager.Instance.DestroyEnemy(this);
            _shipController.Collider.enabled = true;
            transform.localScale = Vector3.one;
        });
    }
}
