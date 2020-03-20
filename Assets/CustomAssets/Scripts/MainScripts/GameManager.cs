using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    static int _currentLevelId = -1;
    public static void StartLevel(int id)
    {
        _currentLevelId = id;
        Debug.LogWarning($"START LEVEL: {_currentLevelId}");
        SceneLoadingManager.LoadGame();
    }

    public static void StopLevel()
    {
        Debug.LogWarning($"STOP LEVEL: {_currentLevelId}");
        SceneLoadingManager.LoadMenu();
    }
}
