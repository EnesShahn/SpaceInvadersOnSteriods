using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField] private EnemyController _enemyPrefab;
    [SerializeField] private Transform _enemyGroupSpawnPosition;
    [SerializeField] private Vector2 _spacing;
    [SerializeField] private EnemyGroup _enemyGroup;
    [SerializeField] private Transform _enemyCrossLine;
    [SerializeField, Range(0.1f, 0.9f)] private float _screenToXAreaMultiplier = 0.5f;

    private bool _initialized;
    private ComponentPool<EnemyController> _enemyPool;
    private List<EnemyController> _activeEnemies = new List<EnemyController>();
    private bool _crossed;
    public Transform EnemyCrossLine => _enemyCrossLine;

    public static event Action OnAllEnemiedDestroyed;
    public static event Action OnWinLineCrossed;
    public static event Action<EnemyController> OnEnemyDestroyed;

    public void Init()
    {
        if (_initialized) return;
        _initialized = true;

        _enemyPool = new ComponentPool<EnemyController>(_enemyPrefab, 10, 2);
    }
    public void ResetStates()
    {
        foreach (var enemy in _activeEnemies)
        {
            _enemyPool.ReturnObject(enemy);
        }
        _activeEnemies.Clear();
        _crossed = false;
    }
    public void SpawnEnemies(Vector2Int gridSize)
    {
        if (!_initialized)
        {
            Debug.LogError("Enemy Manager not initialized");
            return;
        }

        if (gridSize.x == 0 || gridSize.y == 0)
        {
            Debug.LogError("Check passed grid size, x or/and y == 0");
            return;
        }

        Vector3 halfGridSize = new Vector2(gridSize.x * 0.5f, gridSize.y * 0.5f);

        // We want to spawn enemies in a way where the enemies in a group will take _screenToXAreaMultiplier % of the screen
        Vector3 maxSizeAllocation = WorldHelper.WorldSize * _screenToXAreaMultiplier;
        float uniformSize = maxSizeAllocation.x / (gridSize.x * _spacing.x);
        Vector3 enemySize = Vector3.one * uniformSize;
        Vector3 halfEnemySize = enemySize * 0.5f;

        _enemyGroup.transform.position = _enemyGroupSpawnPosition.position;
        _enemyGroup.SetMaxXPosition(WorldHelper.WorldSize.x * 0.5f - halfGridSize.x * enemySize.x * _spacing.x);

        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                Vector3 pos = new Vector3(i * enemySize.x * _spacing.x, j * enemySize.y * _spacing.y);

                var enemy = _enemyPool.RequestObject();
                enemy.transform.localScale = enemySize;
                enemy.transform.SetParent(_enemyGroup.transform);
                enemy.transform.localPosition = pos - new Vector3(halfGridSize.x * enemySize.x * _spacing.x, halfGridSize.y * enemySize.y * _spacing.y, 0) + halfEnemySize;
                _activeEnemies.Add(enemy.GetComponent<EnemyController>());
            }
        }
    }
    public void DestroyEnemy(EnemyController enemy)
    {
        bool removed = _activeEnemies.Remove(enemy);
        if (removed)
        {
            _enemyPool.ReturnObject(enemy);
            OnEnemyDestroyed?.Invoke(enemy);
            if (_activeEnemies.Count == 0)
                OnAllEnemiedDestroyed?.Invoke();
        }
    }

    public void CrossedLine()
    {
        if (_crossed) return;
        _crossed = true;
        OnWinLineCrossed?.Invoke();
    }
}
