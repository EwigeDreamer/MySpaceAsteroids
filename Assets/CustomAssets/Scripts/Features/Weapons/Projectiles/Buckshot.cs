using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buckshot : Projectile
{
#pragma warning disable 649
    [SerializeField] Projectile bulletPrefab;
    [SerializeField] ProjectileKind subKind;
    [SerializeField] int bulletCount = 10;
    [SerializeField] float scatterAngle = 10f;
#pragma warning restore 649

    protected override void Go() => SpawnBuckshot();

    protected override void Stop() { }

    void SpawnBuckshot()
    {
        var pos = transform.position;
        var dir = transform.forward;
        for (int i = 0; i < this.bulletCount; ++i)
        {
            var randomDir = dir + Random.insideUnitSphere * Mathf.Tan(this.scatterAngle * Mathf.Deg2Rad);
            var proj = Instantiate(this.bulletPrefab, pos, Quaternion.LookRotation(randomDir));
            ProjectileController.I.RegisterAlienProjectile(proj, subKind, Info.weapon, pos, randomDir);
        }
        Finish();
    }
}
