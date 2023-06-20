using System;

using UnityEngine;


//  The Coordinator
public class GameManager : Singleton<GameManager>
{
    private int _currentScore;

    public int CurrentScore => _currentScore;

    public static event Action OnScoreUpdated;

    private void Awake()
    {
        Application.targetFrameRate = 120;

        EnemyManager.Instance.Init();
        PlayerManager.Instance.Init();
    }

    private void Start()
    {
        EnemyManager.OnAllEnemiedDestroyed += StartNextWave;
        EnemyManager.OnEnemyDestroyed += OnEnemyDestroyed;
        EnemyManager.OnWinLineCrossed += ProcessGameOver;
        PlayerManager.OnPlayerLivesDepleted += ProcessGameOver;
        PlayerManager.OnPlayerDestroyed += OnPlayerDestroyed;

        PanelManager.Show(PanelType.StartPanel, new StartPanelData(StartGame));
    }

    public void StartGame()
    {
        _currentScore = 0;

        PanelManager.Hide(PanelType.StartPanel);
        PanelManager.Hide(PanelType.EndPanel);

        PanelManager.Show(PanelType.GameplayPanel, null);

        PlayerManager.Instance.SpawnPlayer();
        StartNextWave();
    }

    private void OnEnemyDestroyed(EnemyController enemyController)
    {
        _currentScore++;
        OnScoreUpdated?.Invoke();
    }
    private void OnPlayerDestroyed()
    {
        PlayerManager.Instance.SpawnPlayer();
    }

    private void StartNextWave()
    {
        EnemyManager.Instance.SpawnEnemies(new Vector2Int(15, 10));
    }
    private void ProcessGameOver()
    {
        PanelManager.Show(PanelType.EndPanel, new EndPanelData(_currentScore, StartGame));
        PanelManager.Hide(PanelType.GameplayPanel);

        PlayerManager.Instance.ResetStates();
        EnemyManager.Instance.ResetStates();
        BulletManager.Instance.DestroyAllBullets();
    }
}
