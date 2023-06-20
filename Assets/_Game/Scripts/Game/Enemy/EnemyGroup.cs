using System.Collections;
using System.Collections.Generic;
using System.Drawing;

using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    [SerializeField] private float _swayAmount = 0.2f;
    [SerializeField] private float _swayInterval = 1;
    [SerializeField] private float _advanceAmount = 0.4f;

    private float _maxXPos;
    private int _currentGroupSwayDirection = 0;
    private float _swayTimer;

    private void Awake()
    {
        _currentGroupSwayDirection = Random.Range(0, 2) == 0 ? 1 : -1;
    }

    private void Update()
    {
        DoSway();
    }

    public void SetMaxXPosition(float maxXPos)
    {
        _maxXPos = maxXPos;
    }

    private void DoSway()
    {
        _swayTimer += Time.deltaTime;
        if (_swayTimer >= _swayInterval)
        {
            _swayTimer = 0f;
            Vector3 toPos = transform.position;
            toPos.x = _currentGroupSwayDirection * _maxXPos;
            transform.position = Vector3.MoveTowards(transform.position, toPos, _swayAmount);

            float delta = Mathf.Abs(transform.position.x - toPos.x);
            if (delta < 0.1f)
            {
                //  Switch direction and advance downwards
                _currentGroupSwayDirection *= -1;
                StartCoroutine(AdvanceDelayed());
            }
        }
    }
    private IEnumerator AdvanceDelayed()
    {
        yield return new WaitForSeconds(0.2f);
        transform.position -= Vector3.up * _advanceAmount;
    }
}
