using UnityEngine;
using System;
using MyTools.Pooling;
using MyTools.Helpers;

public abstract class Projectile : MonoValidate
{
    public event Action<GameObject, ProjectileInfo, PointInfo> OnHit = delegate { };
    public event Action<Projectile> OnFinish = delegate { };

#pragma warning disable 649
    [SerializeField] new AudioSource audio;
    ProjectileInfo info = default;
#pragma warning restore 649

    public ProjectileInfo Info => info;
    public AudioSource Audio => audio;


    public void Init(WeaponInfo weapon, ProjectileKind kind, Vector3 position, Vector3 direction)
    {
        this.info.weapon = weapon;
        this.info.instance = this;
        this.info.kind = kind;
        transform.position = position;
        transform.rotation = Quaternion.LookRotation(direction);
        Go();
    }

    protected void Hit(Collider col, PointInfo hit)
    {
        GameObject obj;
        Rigidbody rb = col.attachedRigidbody;
        if (rb != null)
            obj = rb.gameObject;
        else
            obj = col.gameObject;
        OnHit(obj, info, hit);
        Stop();
    }

    protected void Finish()
    {
        OnFinish(this);
    }

    protected abstract void Go();
    protected abstract void Stop();
}

