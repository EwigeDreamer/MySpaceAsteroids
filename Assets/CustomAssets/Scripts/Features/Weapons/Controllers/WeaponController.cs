﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTools;
using System;
using MyTools.Singleton;

[Serializable]
public struct WeaponInfo
{
    public GameObject owner;
    public WeaponKind kind;
    public LayerMask mask;
    public AudioSource audio;
}

public class WeaponController : MonoSingleton<WeaponController>
{
    public event Action<WeaponInfo, Vector3, Vector3> OnShoot = delegate { };

#pragma warning disable 649
    [SerializeField] WeaponModels models;
    [SerializeField] LayerMask hitMask;
#pragma warning restore 649

    public LayerMask HitMask => this.hitMask;

    public WeaponModel GetWeaponModel(WeaponKind kind)
    {
        return Instantiate(models.WeaponModelDict[kind]);
    }

    public void Subscribe(Weapon weapon)
    {
        if (weapon == null) return;
        weapon.OnShoot -= OnShootEvent;
        weapon.OnShoot += OnShootEvent;
    }

    public void Unsubscribe(Weapon weapon)
    {
        if (weapon == null) return;
        weapon.OnShoot -= OnShootEvent;
    }

    private void OnShootEvent(WeaponInfo info, Vector3 pos, Vector3 dir)
    { OnShoot(info, pos, dir); }
}
