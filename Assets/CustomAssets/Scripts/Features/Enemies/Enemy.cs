using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTools.Helpers;
using MyTools.Pooling;
using System;

public class Enemy : MonoValidate, IPooledComponent
{
    [SerializeField] Rigidbody rb;
    [SerializeField] PlayerHealth health;

    public event Action<Enemy, Collision> OnCollide = delegate { };
    public event Action<Enemy> OnDestroyEvent = delegate { };

    public Action deactive = null;
    event Action IPooledComponent.Deactive
    {
        add => deactive += value;
        remove => deactive -= value;
    }

    protected override void OnValidate()
    {
        base.OnValidate();
        ValidateGetComponent(ref this.rb);
        ValidateGetComponent(ref this.health);
    }

    private void Awake()
    {
        this.health.OnDead += Remove;
    }

    public void SetVelocity(Vector3 value) => rb.velocity = value;
    public void SetAngularVelocity(Vector3 value) => rb.angularVelocity = value * Mathf.Deg2Rad;

    void IPooledComponent.OnActivation()
    {
        this.health.ResetHealth();
    }

    void IPooledComponent.OnDeactivation()
    {
        SetVelocity(Vector3.zero);
        SetAngularVelocity(Vector3.zero);
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnCollide(this, collision);
        Remove();
    }

    void Remove()
    {
        OnDestroyEvent(this);
        if (deactive != null) deactive();
        else Destroy(GO);
    }
}
