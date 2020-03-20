using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTools.Prefs;

[System.Serializable]
public class LevelProgressSaveable
{
    public List<LevelProgress> levels = new List<LevelProgress>();
}
[System.Serializable]
public class LevelProgress
{
    public int id;
    public bool complete = false;
    public bool noDamage = false;
    public bool allEnemies = false;

    public int Stars => (complete ? 1 : 0) + (noDamage ? 1 : 0) + (allEnemies ? 1 : 0);
}



public static class LevelProgressData
{
    const string saveKey = "level_progress_data";
    static LevelProgressSaveable _save;

    public static LevelProgress GetProgress(int id)
    {
        foreach (var level in _save.levels) 
            if (level.id == id) { return level; }
        var newProgress = new LevelProgress { id = id };
        _save.levels.Add(newProgress);
        return newProgress;
    }

    static LevelProgressData()
    {
        _save = MyPlayerPrefs.GetObject(saveKey, new LevelProgressSaveable());
        MyPlayerPrefs.OnSave += () => MyPlayerPrefs.SetObject(saveKey, _save);
    }
}
