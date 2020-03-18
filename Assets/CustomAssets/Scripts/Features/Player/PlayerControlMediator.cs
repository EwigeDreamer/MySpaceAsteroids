using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTools.Helpers;
using MyTools.Extensions.Vectors;
using MyTools.Singleton;

public class PlayerControlMediator : MonoSingleton<PlayerControlMediator>
{
    [SerializeField] UserControlScript userControl;

    bool isActive = true;
    public bool IsActive => this.isActive;
    public void SetActive(bool state) => this.isActive = state;

    protected override void OnValidate()
    {
        base.OnValidate();
        ValidateFind(ref this.userControl);
    }

    protected override void Awake()
    {
        base.Awake();
        this.userControl.OnMove += dir =>
        {
            if (!isActive) return;
            PlayerController.I.Player.Motor.Move(dir);
        };
        this.userControl.OnDirectionalShoot += dir =>
        {
            if (!isActive) return;
            Debug.LogWarning($"DirectionalShoot {dir}");
            PlayerController.I.Player.Combat.Shoot(dir);
            PlayerController.I.Player.View.SetAim(false, false);
            PlayerController.I.Player.Motor.SetAimRotation(false);
        };
        this.userControl.OnDirectionalAim += dir =>
        {
            if (!isActive) return;
            PlayerController.I.Player.View.SetAim(true, false);
            PlayerController.I.Player.Motor.SetAimRotation(dir);
        };
        //this.userControl.OnShoot += () =>
            //if (!isActive) return;
            //Debug.LogWarning($"Try Shoot");
            //var player = PlayerController.I.Player;
            //var closest = PlayerController.I.GetClosest(player);
            //if (closest == null) return;
            //var dir = (closest.Motor.Position - player.Motor.Position).ToV2_xz().normalized;
            //Debug.LogWarning($"Shoot {dir}");
            //PlayerController.I.Player.View.SetAim(true, true);
            //PlayerController.I.Player.Motor.SetAimRotation(dir);
            //PlayerController.I.Player.Combat.Shoot(dir);
            //PlayerController.I.Player.View.SetAim(false, false);
            //PlayerController.I.Player.Motor.SetAimRotation(false);
        //};
    }
}
