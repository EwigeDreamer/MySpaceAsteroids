using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTools.Singleton;
using System;

public class PlayerController : MonoSingleton<PlayerController>
{
    public event Action<WeaponKind> OnChangeWeapon = delegate { };

    [SerializeField] Player player;

    protected override void OnValidate()
    {
        base.OnValidate();
        ValidateFind(ref this.player);
    }

    public Player Player => this.player;

    private void Start()
    {
        this.player.Combat.SetWeapon(WeaponKind.Gun_0);
    }
}
