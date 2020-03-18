    using UnityEngine;
    using System;
    using MyTools.Pooling;
    using MyTools.Helpers;
using Object = UnityEngine.Object;

public class Weapon : IDisposable
{
    Transform point;
    WeaponInfo info;
    public Transform Point => this.point;
    public WeaponInfo Info => this.info;

    public event Action<WeaponInfo, Vector3, Vector3> OnShoot = delegate { };


    public Weapon(Transform point, WeaponInfo info)
    {
        this.point = point;
        this.info = info;
    }

    public void Dispose()
    {
        WeaponController.I.Unsubscribe(this);
    }

    public void Shoot()
    {
        OnShoot(this.info, this.point.position, this.point.forward);
    }
}

