using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTools.Singleton;

public class UserHUDController : MonoSingleton<UserHUDController>
{
    [SerializeField] GameUI gameUI;
    protected override void OnValidate()
    {
        base.OnValidate();
        ValidateFind(ref this.gameUI);
    }

    public void SetHp(int value)
    {
        this.gameUI.SetLifePoints(value);
    }
}
