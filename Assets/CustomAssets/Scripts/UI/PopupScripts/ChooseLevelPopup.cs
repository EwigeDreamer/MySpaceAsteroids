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
            int id = i;
            var preset = LevelPresetData.GetPreset(id);
            var progress = LevelProgressData.GetProgress(id);
            var cell = Instantiate(this.cellReference, points[id]);
            cell.GO.SetActive(true);
            cell.Name.text = $"Level {id + 1}";
            cell.SetStars(progress.Stars);
            cell.StartBtn.onClick.AddListener(() => 
            {
                var popup = PopupManager.OpenPopup<StartLevelPopup>();
                popup.SetDescription($"Level {id + 1}\nasteroids: {preset.count}\ntime: {preset.duration}");
                popup.SetStars(progress.Stars);
                popup.OnStart += () =>
                {
                    Hide(null);
                    GameManager.StartLevel(id);
                };
            });
        }
    }
}
