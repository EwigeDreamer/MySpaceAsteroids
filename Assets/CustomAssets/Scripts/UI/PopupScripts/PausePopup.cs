using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PausePopup : PopupBase
{
    public event System.Action OnConfirm = delegate { };

#pragma warning disable 649
    [SerializeField] Button[] returnBtns;
#pragma warning restore 649

    protected override int SortDelta => 0;

    protected override void OnInit()
    {
        base.OnInit();
        foreach (var btn in returnBtns) btn.onClick.AddListener(() => Hide(null));
    }

    protected override void OnRemove()
    {
        base.OnRemove();
        PauseManager.Pause = false;
    }
}
