using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTools.Helpers;
using MyTools.Extensions.Vectors;
using MyTools.Extensions.GameObjects;

public class Player : MonoValidate
{
#pragma warning disable 649
    [SerializeField] PlayerView view;
    [SerializeField] PlayerMotor motor;
    [SerializeField] PlayerCombat combat;
    [SerializeField] PlayerHealth health;
#pragma warning restore 649

    List<IRefreshable> refreshables = new List<IRefreshable>();

    public PlayerView View => this.view;
    public PlayerMotor Motor => this.motor;
    public PlayerCombat Combat => this.combat;
    public PlayerHealth Health => this.health;

    protected override void OnValidate()
    {
        base.OnValidate();
        ValidateGetComponent(ref this.view);
        ValidateGetComponent(ref this.motor);
        ValidateGetComponent(ref this.combat);
        ValidateGetComponent(ref this.health);
    }

    private void Awake()
    {
        GetComponentsInChildren(this.refreshables);
        this.health.OnDead += () =>
        {
            this.motor.SetEnabledCollider(false);
            this.motor.SetRigidbodyKinematic(true);
            this.view.SetVisible(false);
            this.combat.CanShooting = false;
        };
    }

    public void Refresh()
    {
        foreach (var refreshable in this.refreshables) refreshable.Refresh();
    }
}
