using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

using WK = WeaponKind;
using PK = ProjectileKind;
using IE = ImpactEffect;
using DictWKPK = System.Collections.Generic.Dictionary<WeaponKind, ProjectileKind>;
using DictPKIE = System.Collections.Generic.Dictionary<ProjectileKind, System.Collections.ObjectModel.ReadOnlyCollection<ImpactEffect>>;
using RODictWKPK = System.Collections.ObjectModel.ReadOnlyDictionary<WeaponKind, ProjectileKind>;
using RODictPKIE = System.Collections.ObjectModel.ReadOnlyDictionary<ProjectileKind, System.Collections.ObjectModel.ReadOnlyCollection<ImpactEffect>>;

public enum WeaponKind
{
    Unknown = -1,
    Gun_0,
}
public enum ProjectileKind
{
    Unknown = -1,
    Projectile_0,
}
public static class WeaponStaticData
{
    public static RODictWKPK WeaponProjectileBindData { get; } = new RODictWKPK(new DictWKPK
        {
            { WK.Gun_0, PK.Projectile_0 },
            { WK.Unknown, PK.Unknown },
        });

    public static RODictPKIE ProjectileEffectBindData { get; } = new RODictPKIE(new DictPKIE
        {
            { PK.Projectile_0, new List<IE> {
                new DamageImpactEffect(100),
            }.AsReadOnly() },
        });
}
