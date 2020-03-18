using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTools.Extensions.GameObjects;
using DG.Tweening;
using MyTools.Extensions.Vectors;
using System;
using MyTools.Helpers;

public class PlayerView : MonoValidate, IRefreshable
{
    public event Action<bool> OnChangeVisible = delegate { };

    [SerializeField] Renderer[] renderers;
    [SerializeField] PlayerMotor motor;

    bool isVisible = true;
    bool isFounded = false;


    public bool IsVisible => this.isVisible || this.isFounded;

    protected override void OnValidate()
    {
        base.OnValidate();
        ValidateGetComponent(ref this.motor);
    }


    [ContextMenu("Get renderers")]
    void GetRenderers() => this.renderers = gameObject.GetComponentsInChildren<Renderer>();


    public void SetVisible(bool state)
    {
        foreach (var r in this.renderers) r.enabled = state;
    }


    public void Refresh()
    {

    }
}
