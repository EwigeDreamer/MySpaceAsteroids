using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTools.Menu;
using MyTools.Tween;
using MyTools.Extensions.Common;
using UnityEngine.UI;
using System;
using MyTools.Helpers;
using TMPro;
using UnityEngine.EventSystems;

public class GameUI : UIBase
{
    public event Action OnMenuPressed = delegate { };



#pragma warning disable 649
    [SerializeField] Button menuBtn;
    [SerializeField] Joystick movement;
    [SerializeField] EventTrigger fireTrigger;

    [SerializeField] GameObject[] lifePoints;
#pragma warning restore 649

    public Joystick MovementJoystick => movement;
    public EventTrigger FireTrigger => fireTrigger;

    void Awake()
    {
        menuBtn.onClick.AddListener(() => OnMenuPressed());
    }

    public void SetLifePoints(int value)
    {
        var points = this.lifePoints;
        var count = points.Length;
        for (int i = 0; i < count; ++i) points[i].SetActive(i < value);
    }
}