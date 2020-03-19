using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseEnabler : MonoBehaviour
{
    private void OnEnable()
    {
        PauseManager.PauseEnabled = true;
    }

    private void OnDisable()
    {
        PauseManager.PauseEnabled = false;
    }
}
