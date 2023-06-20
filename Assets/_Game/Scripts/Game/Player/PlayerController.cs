using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ShipController _shipController;
    [SerializeField] private float _moveSpeed = 4;

    private Vector2 _shipSize;
    private bool _canMove;

    private void Awake()
    {
        _shipSize = _shipController.SpriteRenderer.bounds.size;
        _shipController.OnShipDestroyed += OnShipDestroyed;
    }
    private void OnEnable()
    {
        _canMove = false;
    }
    private void Update()
    {
        if (!_canMove && CPInput.IsPointerMoved())
        {
            _canMove = true;
        }
        DOMove();
    }

    private void DOMove()
    {
        if (_canMove)
        {
            Vector3 movePosition = Camera.main.ScreenToWorldPoint(CPInput.GetPosition());
            movePosition.x = Mathf.Clamp(movePosition.x, -WorldHelper.WorldSize.x * 0.5f + _shipSize.x, WorldHelper.WorldSize.x * 0.5f - _shipSize.x);
            Vector3 newPos = new Vector3(movePosition.x, transform.position.y, 0);
            transform.position = Vector3.MoveTowards(transform.position, newPos, _moveSpeed * Time.deltaTime);
        }
    }
    private void OnShipDestroyed()
    {
        PlayerManager.Instance.DestroyPlayer(this);
    }

}
