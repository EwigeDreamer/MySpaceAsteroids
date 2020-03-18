using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTools.Singleton;
using System;

public class PlayerController : MonoSingleton<PlayerController>
{
    public event Action<Player> OnRegister = delegate { };
    public event Action<Player> OnUnregister = delegate { };
    public event Action<WeaponKind> OnChangeWeapon = delegate { };

    List<Player> allPlayers = new List<Player>();
    Player localPlayer = null;

    public Player LocalPlayer => localPlayer;

    public void Register(Player player)
    {
        allPlayers.Add(player);
        OnRegister(player);
    }
    public void Unregister(Player player)
    {
        allPlayers.Remove(player);
        OnUnregister(player);
    }

    public void RegisterLocal(Player player)
    {
        if (this.localPlayer == player) return;
        if (this.localPlayer != null) UnregisterLocal(this.localPlayer);
        this.localPlayer = player;
        player.Combat.OnSetWeapon -= OnWeapoChangeEvent;
        player.Combat.OnSetWeapon += OnWeapoChangeEvent;
    }

    public void UnregisterLocal(Player player)
    {
        if (this.localPlayer == null) return;
        if (this.localPlayer != player) return;
        this.localPlayer = null;
        player.Combat.OnSetWeapon -= OnWeapoChangeEvent;
    }

    void OnWeapoChangeEvent(WeaponKind kind) => OnChangeWeapon(kind);

    public Player GetClosest(Player player)
    {
        if (this.allPlayers.Count < 1) return null;
        var min = float.PositiveInfinity;
        Player closest = null;
        foreach (var pl in this.allPlayers)
        {
            if (pl == player) continue;
            if (!pl.View.IsVisible) continue;
            var dist = (pl.Motor.Position - player.Motor.Position).sqrMagnitude;
            if (dist > min) continue;
            min = dist;
            closest = pl;
        }
        return closest;
    }
}
