using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChooseLevelPopup : PopupBase
{
#pragma warning disable 649
    [SerializeField] ChooseLevelPopupCell cellReference;
    [SerializeField] Button[] returnBtns;
#pragma warning restore 649

    protected override int SortDelta => 0;

    protected override void OnInit()
    {
        foreach (var btn in this.returnBtns) btn.onClick.AddListener(() => Hide(null));
    }
}
