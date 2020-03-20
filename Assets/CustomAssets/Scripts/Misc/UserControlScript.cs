using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTools.Helpers;
using System;
using MyTools.ValueInfo;

public class UserControlScript : MonoValidate
{
    public event Action<Vector2> OnMove = delegate { };
    public event Action OnStartShoot = delegate { };
    public event Action OnStopShoot = delegate { };

    [SerializeField] GameUI gameUI;

    protected override void OnValidate()
    {
        base.OnValidate();
        ValidateFind(ref this.gameUI);
    }

    private void Awake()
    {
        this.gameUI.OnPauseMenuPressed += () => PauseManager.Pause = true;
        this.gameUI.OnFireTriggerOn += () => OnStartShoot();
        this.gameUI.OnFireTriggerOff += () => OnStopShoot();
    }

    private void FixedUpdate()
    {
        OnMove(this.gameUI.MovementJoystick.Direction);
    }
}
