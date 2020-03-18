using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTools.Factory;
using MyTools.Pooling;
using System;
using MyTools.Extensions.Rects;
using OneLine;

public class ProjectilePooledFactory : MonoBehaviour, IProjectileFactory
{
#pragma warning disable 649
    [SerializeField, OneLine(Header = LineHeader.Short)]
    ProjectileKindKeyPair[] projectilePoolKeys;
    Dictionary<ProjectileKind, string> projectilePoolKeyDict;
#pragma warning restore 649

    public Projectile GetObject(ProjectileKind info)
    {
        if (!this.projectilePoolKeyDict.TryGetValue(info, out var key))
        {
            MyLogger.ObjectErrorFormat<ProjectilePooledFactory>("don't contain \"{0}\" kind!", info);
            return null;
        }
        if (!ObjectPool.I.TrySpawn(key, out var obj))
        {
            MyLogger.ObjectErrorFormat<ProjectilePooledFactory>("\"{0}\" kan't be spawned!", key);
            return null;
        }
        return obj.GetComponent<Projectile>();
    }

    public bool TryGetObject(ProjectileKind info, out Projectile obj)
    {
        obj = GetObject(info);
        return obj != null;
    }

    private void Awake()
    {
        var keys = projectilePoolKeys;
        var count = keys.Length;
        if (count < 1) return;
        var dict = new Dictionary<ProjectileKind, string>(count);
        for (int i = 0; i < count; ++i)
            dict[keys[i].kind] = keys[i].key;
        this.projectilePoolKeyDict = dict;
    }

    [System.Serializable]
    public class ProjectileKindKeyPair
    {
        public ProjectileKind kind;
        [PoolKey] public string key;
    }
}
