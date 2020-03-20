using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTools.Helpers;
using MyTools.Singleton;
using DG.Tweening;
using MyTools.Pooling;

public class MatchController : MonoSingleton<MatchController>
{
    [SerializeField] EnemiesSpawner spawner;

    LevelPreset preset;

    protected override void OnValidate()
    {
        base.OnValidate();
        ValidateFind(ref this.spawner);
    }

    public void StartMatch(int levelId)
    {
        this.preset = LevelPresetData.GetPreset(levelId);
        CorouWaiter.Start(Routine());
        IEnumerator Routine()
        {
            yield return CorouWaiter.WaitFor(() => PlayerController.I != null);
            yield return CorouWaiter.WaitFor(() => UserHUDController.I != null);
            PlayerController.I.Player.Health.OnDamage += (_, hp) => UserHUDController.I.SetHp(hp);

            var waitSecond = new WaitForSeconds(1f);
            int counter = 3;
            while (counter --> 0)
            {
                Debug.Log(counter + 1);
                yield return waitSecond;
            }

            Debug.Log("Math started!");

            var waitSpawn = new WaitForSeconds(this.preset.duration / (float)this.preset.count);
            counter = this.preset.count;
            while (counter --> 0)
            {
                var enemy = spawner.SpawnEnemy();
                enemy.OnCollide += Damage;
                void Damage(Enemy e, Collision c)
                {
                    enemy.OnCollide -= Damage;
                    var health = c.collider.GetComponentInParent<PlayerHealth>();
                    if (health == null) return;
                    health.SetDamage(1, enemy.GO);
                }
                yield return waitSpawn;
            }
        }
    }

    public void StopMatch()
    {

    }
}
