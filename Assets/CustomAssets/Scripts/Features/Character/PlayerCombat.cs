using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTools.Extensions.Vectors;
using System;
using MyTools.Extensions.GameObjects;
using MyTools.Helpers;

public class PlayerCombat : MonoValidate, IRefreshable
{
#pragma warning disable 649
    [SerializeField] PlayerView view;
    [SerializeField] Transform weaponPoint;
    [SerializeField] LayerMask hitMask;
    [SerializeField] new AudioSource audio;
#pragma warning restore 649

    public event Action<WeaponKind> OnSetWeapon = delegate { };

    WeaponKind currentKind = WeaponKind.Unknown;

    Weapon weapon = null;

    public WeaponKind CurrentWeapon => currentKind;

    protected override void OnValidate()
    {
        base.OnValidate();
        ValidateGetComponent(ref this.view);
    }

    private void Awake()
    {
        this.view.OnChangeVisible += state => weapon?.Model.SetVisible(state);
    }

    public void Refresh()
    {
        if (this.currentKind == WeaponKind.Unknown) return;
        SetWeapon(this.currentKind);
        this.weapon?.Model.SetVisible(this.view.IsVisible);
    }

    public void SetWeapon(WeaponKind kind)
    {
        if (this.weapon != null && this.weapon.Info.kind == kind) return;
        this.weapon?.Dispose();
        WeaponInfo info = new WeaponInfo
        {
            owner = gameObject,
            kind = kind,
            mask = this.hitMask,
            audio = this.audio,
        };
        this.weapon = new Weapon(this.weaponPoint, this.weaponPoint, info);
        this.weapon.Model.SetVisible(this.view.IsVisible);
        OnSetWeapon(kind);
    }

    public void Shoot(Vector2 dir)
    {
        this.weapon?.Shoot(dir.ToV3_x0y());
    }
}
