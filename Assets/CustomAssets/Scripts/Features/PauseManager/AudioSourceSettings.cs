using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTools.Helpers;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceSettings : MonoBehaviour
{
    [SerializeField] bool m_IgnoreListenerPause = false;
    [SerializeField] bool m_IgnoreListenerVolume = false;

    void Awake()
    {
        var audio = GetComponent<AudioSource>();
        if (audio != null)
        {
            audio.ignoreListenerPause = m_IgnoreListenerPause;
            audio.ignoreListenerVolume = m_IgnoreListenerVolume;
        }
    }
}
