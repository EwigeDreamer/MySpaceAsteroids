using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTools.Helpers;

public static class GameManager
{
    static int _currentLevelId = -1;

    public static void StartLevel(int id)
    {
        _currentLevelId = id;
        Debug.LogWarning($"START LEVEL: {_currentLevelId}");
        CorouWaiter.Start(Routine());
        IEnumerator Routine()
        {
            PauseManager.PauseEnabled = true;
            yield return SceneLoadingManager.LoadGame();
            MatchController.I.StartMatch(id);
        }
    }

    public static void StopLevel()
    {
        Debug.LogWarning($"STOP LEVEL: {_currentLevelId}");
        PauseManager.PauseEnabled = false;
        MatchController.I.StopMatch();
        SceneLoadingManager.LoadMenu();
    }
}
