using System;
using System.Collections;
using System.Collections.Generic;

using DG.Tweening;

using UnityEngine;
using UnityEngine.UI;

public class StartPanelData : BasePanelData
{
    private Action _onStartButtonClicked;

    public Action OnStartButtonClicked => _onStartButtonClicked;

    public StartPanelData(Action onStartButtonClicked)
    {
        _onStartButtonClicked = onStartButtonClicked;
    }
}

public class StartPanel : BasePanel
{
    [SerializeField] private GameObject _parent;
    [SerializeField] private Button _startButton;

    public override PanelType PanelType => PanelType.StartPanel;

    StartPanelData _panelData;

    public override void ShowPanel(BasePanelData data)
    {
        _panelData = data as StartPanelData;
        gameObject.SetActive(true);


        _parent.SetActive(true);
        _parent.transform.DOScale(Vector3.one, 0.2f).From(Vector3.one * 0.2f);

        _startButton.onClick.AddListener(OnStartButtonClicked);
    }

    public override void HidePanel()
    {
        gameObject.SetActive(false);
        _startButton.onClick.RemoveListener(OnStartButtonClicked);
    }

    private void OnStartButtonClicked()
    {
        _panelData.OnStartButtonClicked?.Invoke();
    }
}
