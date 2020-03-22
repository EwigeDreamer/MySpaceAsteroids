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

    int killCount = 0;
    int destroyCount = 0;

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
            PlayerController.I.Player.Health.OnDead += StopMatch;

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
                    if (health != null) health.SetDamage(1, enemy.GO);
                }
                enemy.OnDeadByPlayer += Kill;
                void Kill(Enemy e)
                {
                    enemy.OnDeadByPlayer -= Kill;
                    ++this.killCount;
                }
                enemy.OnDestroyEvent += Destroy;
                void Destroy(Enemy e)
                {
                    enemy.OnDestroyEvent -= Destroy;
                    ++this.destroyCount;
                    if (this.destroyCount == this.preset.count) StopMatch();
                }
                yield return waitSpawn;
            }
        }
    }

    public void StopMatch()
    {
        bool isWin = !PlayerController.I.Player.Health.Hp.IsZero;
        bool allEnemies = this.killCount == this.preset.count;
        bool noDamage = PlayerController.I.Player.Health.Hp.IsMax;

        var progress = LevelProgressData.GetProgress(this.preset.id);
        progress.complete |= isWin;
        progress.allEnemies |= allEnemies;
        progress.noDamage |= noDamage;

        var popup = PopupManager.OpenPopup<EndLevelPopup>();
        popup.SetWindow(isWin);
        if (isWin) popup.SetStars(1 + (allEnemies ? 1 : 0) + (noDamage ? 1 : 0));
        popup.OnRemoving += GameManager.StopLevel;
    }
}
