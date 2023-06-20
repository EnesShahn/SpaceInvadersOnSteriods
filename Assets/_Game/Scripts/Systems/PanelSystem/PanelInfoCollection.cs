using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PanelInfoCollection", menuName = "Game/PanelInfoCollection")]

public class PanelInfoCollection : ScriptableObject
{
    public PanelInfo[] items;
}
