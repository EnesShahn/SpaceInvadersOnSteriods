using System;

using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private int _startPlayerLives = 3;
    [SerializeField] private Transform _playerStartPosition;
    [SerializeField] private PlayerController _playerPrefab;

    private bool _initialized;
    private int _currentPlayerLives;
    private PlayerController _currentPlayerController;

    public int CurrentPlayerLives => _currentPlayerLives;

    public static event Action OnPlayerDestroyed;
    public static event Action OnPlayerLivesUpdated;
    public static event Action OnPlayerLivesDepleted;

    public void Init()
    {
        if (_initialized) return;
        _initialized = true;

        _currentPlayerLives = _startPlayerLives;
    }
    public void ResetStates()
    {
        _currentPlayerLives = _startPlayerLives;
    }

    public void SpawnPlayer()
    {
        if (_currentPlayerLives == 0) return;

        var newPlayer = GameObject.Instantiate(_playerPrefab);
        newPlayer.transform.position = _playerStartPosition.position;
        _currentPlayerController = newPlayer;
        _currentPlayerLives--;
        OnPlayerLivesUpdated?.Invoke();
    }
    public void DestroyPlayer(PlayerController player)
    {
        Destroy(player.gameObject);
        _currentPlayerController = null;

        OnPlayerDestroyed?.Invoke();

        if (_currentPlayerLives == 0 && _currentPlayerController == null)
            OnPlayerLivesDepleted?.Invoke();
    }
}
