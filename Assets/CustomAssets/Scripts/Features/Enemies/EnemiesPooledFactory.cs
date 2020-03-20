using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTools.Pooling;

public class EnemiesPooledFactory : MonoBehaviour, IEnemyFactory
{
#pragma warning disable 649
    [SerializeField] [PoolKey] string enemyKey;
#pragma warning restore 649

    public Enemy GetObject()
    {
        ObjectPool.I.TrySpawn(enemyKey, out var obj);
        return obj?.GetComponent<Enemy>();
    }

    public bool TryGetObject(out Enemy obj)
    {
        obj = GetObject();
        return obj != null;
    }
}
