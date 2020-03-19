using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChooseLevelPopup : PopupBase
{
#pragma warning disable 649
    [SerializeField] LevelCell cellReference;
    [SerializeField] Button[] returnBtns;
    [SerializeField] RectTransform[] levelPoints;
#pragma warning restore 649

    protected override int SortDelta => 0;

    protected override void OnInit()
    {
        foreach (var btn in this.returnBtns) btn.onClick.AddListener(() => Hide(null));
        this.cellReference.GO.SetActive(false);

        var points = this.levelPoints;
        var count = points.Length;
        for (int i = 0; i < count; ++i)
        {
            var preset = LevelPresetData.GetPreset(i);
            var progress = LevelProgressData.GetProgress(i);
            var cell = Instantiate(this.cellReference, points[i]);
            cell.GO.SetActive(true);
            cell.Name.text = $"Level {i}";
            int stars = 0;
            stars += progress.complete ? 1 : 0;
            stars += progress.allEnemies ? 1 : 0;
            stars += progress.noDamage ? 1 : 0;
            cell.SetStars(stars);
            cell.StartBtn.onClick.AddListener(() => StartLevel(i));
        }

        void StartLevel(int id)
        {
            Debug.LogError($"START LEVEL: {id}");
        }
    }
}
