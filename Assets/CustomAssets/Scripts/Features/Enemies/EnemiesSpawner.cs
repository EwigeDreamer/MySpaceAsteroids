using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTools.Helpers;
using MyTools.Extensions.Vectors;

public class EnemiesSpawner : MonoValidate
{
#pragma warning disable 649
    [SerializeField] BoxCollider spawnBox;
    [SerializeField] float minVelocity = 10f;
    [SerializeField] float maxVelocity = 10f;
    [SerializeField] float maxAngularVelocity = 90f;
#pragma warning restore 649

    IEnemyFactory factory;

    private void Awake()
    {
        ValidateGetComponent(ref this.factory);
    }

    public Enemy SpawnEnemy()
    {
        var enemy = factory.GetObject();
        enemy.TR.position = GetRandomPointInsideBox(this.spawnBox.bounds).SetZ(0f);
        enemy.SetVelocity(Vector3.down * Random.Range(this.minVelocity, this.maxVelocity));
        enemy.SetAngularVelocity(Random.insideUnitSphere * maxAngularVelocity);
        return enemy;
    }

    Vector3 GetRandomPointInsideBox(Bounds bounds)
    {
        Vector3 v;
        v.x = Random.Range(bounds.min.x, bounds.max.x);
        v.y = Random.Range(bounds.min.y, bounds.max.y);
        v.z = Random.Range(bounds.min.z, bounds.max.z);
        return v;
    }
}
