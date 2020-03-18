using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTools.Extensions.GameObjects;
using DG.Tweening;
using MyTools.Extensions.Vectors;
using MyTools.ValueInfo;
using MyTools.Helpers;

public class PlayerStatusBar : MonoValidate
{
    [SerializeField] CharacterStatusBar bar;
    [SerializeField] PlayerHealth health;
    [SerializeField] PlayerView view;

    [SerializeField] Color myColor = Color.green;
    [SerializeField] Color enemyColor = Color.red;

    protected override void OnValidate()
    {
        base.OnValidate();
        ValidateGetComponent(ref this.health);
        ValidateGetComponent(ref this.view);
        ValidateGetComponentInChildren(ref this.bar);
    }

    private void Awake()
    {
        this.view.OnChangeVisible += state => this.bar.SetActive(state);
        this.health.OnDamage += (_, hp) => RefreshHp(hp);
        this.health.OnHeal += (_, hp) => RefreshHp(hp);
        this.health.OnReset += () => RefreshHp();
    }

    private void Start()
    {
        RefreshHp();
    }

    void RefreshHp()
    {
        var hp = this.health.Hp;
        RefreshHp(hp);
    }
    void RefreshHp(IntInfo hp)
    {
        this.bar.SetHpValue(hp.Normalize);
    }

    public void Refresh()
    {
        RefreshHp();
        this.bar.SetActive(this.view.IsVisible);
    }
}
