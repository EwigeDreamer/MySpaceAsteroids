using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTools.Helpers;
using OneLine;
using MyTools.Pooling;
using System.Collections.ObjectModel;
using MyTools.Singleton;

public class ProjectileVisualFXController : MonoSingleton<ProjectileVisualFXController>
{
#pragma warning disable 649
    [SerializeField] ProjectileController projectileCtrl;

    [SerializeField, OneLine(Header = LineHeader.Short)]
    ProjectileEffectInfo[] infoList;
    IVisualEffectPointFactory factory;
#pragma warning restore 649

    Dictionary<ProjectileKind, ProjectileEffectInfo> dict = new Dictionary<ProjectileKind, ProjectileEffectInfo>();

    protected override void OnValidate()
    {
        base.OnValidate();
        ValidateFind(ref this.projectileCtrl);
    }
    protected override void Awake()
    {
        base.Awake();
        ValidateGetComponent(ref this.factory);
        this.dict = new Dictionary<ProjectileKind, ProjectileEffectInfo>();
        foreach (var item in this.infoList) this.dict[item.kind] = item;
        this.projectileCtrl.OnShoot += (proj, point) => OnShoot(proj.kind, point);
        this.projectileCtrl.OnHit += (_, proj, point) => OnHit(proj.kind, point);
    }

    void OnShoot(ProjectileKind kind, PointInfo point)
    {
        if (!this.dict.TryGetValue(kind, out var info)) return;
        Debug.LogWarning($"SHOOT EFFECT! kind: {kind}, point: {point.point}");

        var effect = factory.GetObject(info.shoot);
        effect.TR.position = point.point;
        effect.TR.rotation = Quaternion.LookRotation(point.direction);
    }
    void OnHit(ProjectileKind kind, PointInfo point)
    {
        if (!this.dict.TryGetValue(kind, out var info)) return;
        Debug.LogWarning($"IMPACT EFFECT! kind: {kind}, point: {point.point}");

        var effect = factory.GetObject(info.impact);
        effect.TR.position = point.point;
        effect.TR.rotation = Quaternion.LookRotation(point.normal);
    }

    [System.Serializable]
    public class ProjectileEffectInfo
    {
        public ProjectileKind kind;
        [PoolKey] public string shoot;
        [PoolKey] public string impact;
    }
}

