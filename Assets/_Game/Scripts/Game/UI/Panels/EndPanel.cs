using System;
using System.Collections;
using System.Collections.Generic;

using DG.Tweening;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class EndPanelData : BasePanelData
{
    private int _finalScore;
    private Action _onStartButtonClicked;

    public int FinalScore => _finalScore;
    public Action OnRestartButtonClicked => _onStartButtonClicked;

    public EndPanelData(int finalScore, Action onStartButtonClicked)
    {
        _finalScore = finalScore;
        _onStartButtonClicked = onStartButtonClicked;
    }
}

public class EndPanel : BasePanel
{
    [SerializeField] private GameObject _parent;
    [SerializeField] private Button _restartButton;
    [SerializeField] private TMP_Text _scoreText;

    public override PanelType PanelType => PanelType.EndPanel;

    EndPanelData _panelData;

    public override void ShowPanel(BasePanelData data)
    {
        _panelData = data as EndPanelData;
        gameObject.SetActive(true);

        _parent.SetActive(true);
        _parent.transform.DOScale(Vector3.one, 0.2f).From(Vector3.one * 0.2f);

        _restartButton.onClick.AddListener(OnRestartButtonClicked);
        _scoreText.text = _panelData.FinalScore.ToString();
    }

    public override void HidePanel()
    {
        gameObject.SetActive(false);
        _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
    }

    private void OnRestartButtonClicked()
    {
        _panelData.OnRestartButtonClicked?.Invoke();
    }
}
