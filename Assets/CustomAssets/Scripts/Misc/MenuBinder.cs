using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTools.Helpers;

public class MenuBinder : MonoValidate
{
    [SerializeField] MainMenuUI main;

    protected override void OnValidate()
    {
        base.OnValidate();
        ValidateFind(ref this.main, true);
    }

    private void Awake()
    {
        this.main.OnPlayPressed += () => PopupManager.OpenPopup<ChooseLevelPopup>();
        this.main.OnInfoPressed += () => PopupManager.OpenPopup<MessagePopup>().SetText("Created by Paul Sammler");
        this.main.OnQuitPressed += () => PopupManager.OpenPopup<QuittingPopup>();
    }
}