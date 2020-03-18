using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MyTools.Helpers;
using MyTools.Singleton;
using DG.Tweening;

public enum ProjectileEventType { Shoot, Hit }

public struct ProjectileInfo
{
    public Projectile instance;
    public WeaponInfo weapon;
    public ProjectileKind kind;
}
public struct PointInfo
{
    public Vector3 point;
    public Vector3 direction;
    public Vector3 normal;
}

public class ProjectileController : MonoSingleton<ProjectileController>
{
    public event Action<ProjectileInfo, PointInfo> OnShoot = delegate { };
    public event Action<GameObject, ProjectileInfo, PointInfo> OnHit = delegate { };

#pragma warning disable 649
    [SerializeField] WeaponController weaponCtrl;
    IProjectileFactory factory;
#pragma warning restore 649

    protected override void OnValidate()
    {
        base.OnValidate();
        ValidateFind(ref this.weaponCtrl);
    }

    protected override void Awake()
    {
        base.Awake();
        ValidateGetComponent(ref this.factory);
        this.weaponCtrl.OnShoot += CreateProjectile;
    }

    private void CreateProjectile(WeaponInfo wInfo, Vector3 pos, Vector3 dir)
    {
        var pKind = WeaponStaticData.WeaponProjectileBindData[wInfo.kind];
        var proj = this.factory.GetObject(pKind);
        proj.TR.position = pos;
        proj.TR.rotation = Quaternion.LookRotation(dir);
        Subscribe(proj);
        proj.Init(wInfo, pKind, pos, dir);
        OnShoot(proj.Info, new PointInfo { point = pos, direction = dir, normal = dir });
    }

    void Subscribe(Projectile proj)
    {
        proj.OnHit += OnHitEvent;
        proj.OnFinish += DestroyProjectile;
    }

    public void RegisterAlienProjectile(Projectile proj, ProjectileKind kind, WeaponInfo wInfo, Vector3 pos, Vector3 dir)
    {
        //if (!NetworkServer.active) return;
        if (proj == null) return;
        //NetworkServer.Spawn(proj.gameObject);
        Subscribe(proj);
        proj.Init(wInfo, kind, pos, dir);
        OnShoot(proj.Info, new PointInfo { point = pos, direction = dir, normal = dir });
    }

    private void DestroyProjectile(Projectile proj)
    {
        DOVirtual.DelayedCall(1f, () =>
        {
            proj.OnHit -= OnHitEvent;
            proj.OnFinish -= DestroyProjectile;
            Destroy(proj.gameObject);
            //NetworkServer.Destroy(proj.gameObject);
        });
    }

    private void OnHitEvent(GameObject obj, ProjectileInfo info, PointInfo hit) => OnHit(obj, info, hit);
}


