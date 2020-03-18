using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTools.Helpers;
using MyTools.Extensions.Vectors;
using MyTools.Extensions.GameObjects;

public class Player : MonoValidate
{
    [SerializeField] PlayerView view;
    [SerializeField] PlayerMotor motor;
    [SerializeField] PlayerCombat combat;
    [SerializeField] new PlayerCamera camera;
    [SerializeField] CharacterStatusBar statusBar;
    [SerializeField] PlayerHealth health;


    public PlayerView View => this.view;
    public PlayerMotor Motor => this.motor;
    public PlayerCombat Combat => this.combat;
    public PlayerCamera Camera => this.camera;
    public PlayerHealth Health => this.health;

    protected override void OnValidate()
    {
        base.OnValidate();
        ValidateGetComponent(ref this.view);
        ValidateGetComponent(ref this.motor);
        ValidateGetComponent(ref this.combat);
        ValidateGetComponent(ref this.camera);
        ValidateGetComponent(ref this.health);
        ValidateGetComponent(ref this.statusBar);
    }

    private void Awake()
    {
        this.camera.SetActiveCamera(false);
        this.health.OnDead += () =>
        {
            motor.SetEnabledCollider(false);
            motor.SetRigidbodyKinematic(true);
        };
    }

    private void Start()
    {
        PlayerController.I.RegisterLocal(this);
    }

    private void OnDestroy()
    {
        PlayerController.I.UnregisterLocal(this);
    }


    public void Refresh()
    {
        view.Refresh();
        motor.Refresh();
        combat.Refresh();
        camera.Refresh();
    }
}
