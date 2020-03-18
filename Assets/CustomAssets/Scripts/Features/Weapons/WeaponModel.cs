﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTools.Helpers;

public class WeaponModel : MonoValidate
{
#pragma warning disable 649
    [SerializeField] Renderer[] renderers;
    [SerializeField] Transform projectilePoint;
#pragma warning restore 649

    public (Vector3, Vector3) ProjectilePointAndDir => (projectilePoint.position, projectilePoint.forward);

    [ContextMenu("Get renderers")]
    void GetRenderers()
    {
        this.renderers = GetComponentsInChildren<Renderer>();
    }

    public void SetVisible(bool state)
    {
        foreach (var r in this.renderers) r.enabled = state;
    }
}
