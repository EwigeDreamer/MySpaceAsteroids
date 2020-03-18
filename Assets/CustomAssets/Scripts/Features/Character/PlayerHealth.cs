using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTools.ValueInfo;
using System;
using MyTools.Extensions.GameObjects;
using MyTools.Helpers;

public class PlayerHealth : MonoValidate, IRefreshable
{
    public event Action OnDead = delegate { };
    public event Action<GameObject, Player> OnDeadByKiller = delegate { };
    public event Action<int, IntInfo> OnDamage = delegate { };
    public event Action<int, IntInfo> OnHeal = delegate { };
    public event Action OnReset = delegate { };

    [SerializeField] Player player;

    [SerializeField] IntInfo hp = new IntInfo { Min = 0, Max = 100, Value = 100 };

    public IntInfo Hp => hp;

    protected override void OnValidate()
    {
        base.OnValidate();
        ValidateGetComponent(ref this.player);
    }

    public void SetDamage(int damage, GameObject killer)
    {
        if (this.hp.IsMin) return;
        var newHp = this.hp;
        newHp.Value -= damage;
        SetNewHpValue(newHp);
        Debug.Log($"SET DAMAGE! damage: {damage}, isDead: {newHp.IsZero}");
        if (newHp.IsZero) OnDeadByKiller(killer, this.player);
    }

    public void SetHeal(int heal)
    {
        if (this.hp.IsMax) return;
        var newHp = this.hp;
        newHp.Value += heal;
        SetNewHpValue(newHp);
    }

    void SetNewHpValue(IntInfo hp)
    {
        int diff = hp.value - this.hp.value;
        if (diff == 0) return;
        this.hp = hp;
        if (diff > 0) OnHeal(diff, hp);
        if (diff < 0) OnDamage(-diff, hp);
        if (hp.IsZero) OnDead();
    }

    public void ResetHealth()
    {
        if (this.hp.IsMax) return;
        this.hp.Value = this.hp.Max;
        OnReset();
    }

    public void Refresh()
    {

    }
}
