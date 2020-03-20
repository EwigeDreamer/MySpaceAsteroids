using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;
using OneLine;
using MyTools.Prefs;

[System.Serializable]
public class LevelPresetSaveable
{
    public List<LevelPreset> levels = new List<LevelPreset>();
}
[System.Serializable]
public class LevelPreset
{
    public int id;
    public int count;
    public int duration;
}

public static class LevelPresetData
{
    const string saveKey = "level_preset_data";
    static LevelPresetSaveable _save;

    public static LevelPreset GetPreset(int id)
    {
        foreach (var level in _save.levels)
            if (level.id == id) { return level; }
        var newLevel = GetRandomLevel(id);
        _save.levels.Add(newLevel);
        return newLevel;
    }

    public static LevelPreset GetRandomLevel(int id)
    {
        return new LevelPreset
        {
            id = id,
            count = Random.Range(10, 41),
            duration = Random.Range(10, 21)
        };
    }

    static LevelPresetData()
    {
        _save = MyPlayerPrefs.GetObject(saveKey, new LevelPresetSaveable());
        MyPlayerPrefs.OnSave += () => MyPlayerPrefs.SetObject(saveKey, _save);
    }
}
