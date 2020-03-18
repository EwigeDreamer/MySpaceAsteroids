using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTools.Helpers;
using Cinemachine;

public class PlayerCamera : MonoValidate, IRefreshable
{
    [SerializeField] new CinemachineVirtualCameraBase camera;

    protected override void OnValidate()
    {
        base.OnValidate();
        ValidateGetComponentInChildren(ref this.camera);
    }

    public void SetActiveCamera(bool state)
    {
        this.camera.enabled = state;
    }

    public void Refresh()
    {

    }
}
