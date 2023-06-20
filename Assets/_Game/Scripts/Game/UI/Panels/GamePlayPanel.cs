using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class GamePlayPanel : BasePanel
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private GameObject[] _playerLives;

    public override PanelType PanelType => PanelType.GameplayPanel;

    public override void ShowPanel(BasePanelData data)
    {
        gameObject.SetActive(true);

        GameManager.OnScoreUpdated += UpdateUI;
        PlayerManager.OnPlayerLivesUpdated += UpdateUI;

        UpdateUI();
    }

    public override void HidePanel()
    {
        gameObject.SetActive(false);

        GameManager.OnScoreUpdated -= UpdateUI;
        PlayerManager.OnPlayerLivesUpdated -= UpdateUI;
    }

    private void UpdateUI()
    {
        _scoreText.text = GameManager.Instance.CurrentScore.ToString();

        for (int i = 0; i < _playerLives.Length; i++)
        {
            _playerLives[i].SetActive(i + 1 <= PlayerManager.Instance.CurrentPlayerLives);
        }

    }

}
