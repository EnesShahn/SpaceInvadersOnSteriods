using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PanelCanvas : MonoBehaviour
{
    public static PanelCanvas Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

}
