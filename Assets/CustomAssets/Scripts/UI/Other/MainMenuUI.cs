using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTools.Menu;
using MyTools.Tween;
using MyTools.Extensions.Common;
using UnityEngine.UI;
using System;
using MyTools.Helpers;

public class MainMenuUI : UIBase
{
    public event Action OnPlayPressed = delegate { };
    public event Action OnJoinPressed = delegate { };
    public event Action OnQuitPressed = delegate { };

#pragma warning disable 649
    [SerializeField] Button playBtn;
    [SerializeField] Button joinBtn;
    [SerializeField] Button quitBtn;
#pragma warning restore 649

    void Awake()
    {
        playBtn.onClick.AddListener(() => OnPlayPressed());
        joinBtn.onClick.AddListener(() => OnJoinPressed());
        quitBtn.onClick.AddListener(() => OnQuitPressed());
    }
}