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

        CorouWaiter.WaitFor(() => PlayerController.I != null, Subscribe, () => this == null);
        void Subscribe()
        {
            this.userControl.OnMove += dir =>
            {
                if (!isActive) return;
                PlayerController.I.Player.Motor.Move(dir);
            };
            this.userControl.OnStartShoot += () =>
            {
                if (!isActive) return;
                Debug.Log($"Start shooting");
                PlayerController.I.Player.Combat.StartShooting();
            };
            this.userControl.OnStopShoot += () =>
            {
                if (!isActive) return;
                Debug.Log($"Stop shooting");
                PlayerController.I.Player.Combat.StopShooting();
            };
        }
    }
}
