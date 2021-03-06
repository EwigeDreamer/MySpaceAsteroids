﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTools.Extensions.Vectors;
using System;
using MyTools.Extensions.GameObjects;
using MyTools.Helpers;
using DG.Tweening;

public class PlayerCombat : MonoValidate, IRefreshable
{
#pragma warning disable 649
    [SerializeField] int fireRate = 12;
    [SerializeField] PlayerView view;
    [SerializeField] Transform weaponPoint;
    [SerializeField] LayerMask hitMask;
    [SerializeField] new AudioSource audio;
#pragma warning restore 649

    public event Action<WeaponKind> OnSetWeapon = delegate { };

    WeaponKind currentKind = WeaponKind.Unknown;

    Weapon weapon = null;

    Tween shooting = null;

    bool canShooting = true;

    public bool CanShooting
    {
        get => this.canShooting;
        set
        {
            this.canShooting = value;
            if (!value) StopShooting();
        }
    }

    public WeaponKind CurrentWeapon => currentKind;

    protected override void OnValidate()
    {
        base.OnValidate();
        ValidateGetComponent(ref this.view);
    }

    public void Refresh()
    {
        if (this.currentKind == WeaponKind.Unknown) return;
        SetWeapon(this.currentKind);
    }

    public void SetWeapon(WeaponKind kind)
    {
        if (this.weapon != null && this.weapon.Info.kind == kind) return;
        RemoveWeapon();
        WeaponInfo info = new WeaponInfo
        {
            owner = gameObject,
            kind = kind,
            mask = this.hitMask,
            audio = this.audio,
        };
        this.weapon = new Weapon(this.weaponPoint, info);
        OnSetWeapon(kind);
    }

    public void RemoveWeapon()
    {
        this.weapon?.Dispose();
        this.weapon = null;
    }

    public void StartShooting()
    {
        if (!CanShooting) return;
        this.shooting?.Kill();
        Shoot();
        this.shooting = DOVirtual.DelayedCall(1f / this.fireRate, Shoot).SetLoops(-1);
    }
    public void StopShooting()
    {
        this.shooting?.Kill();
        this.shooting = null;
    }

    public void Shoot()
    {
        this.weapon?.Shoot();
    }
}
