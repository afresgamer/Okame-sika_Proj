using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundSetting")]
public class SoundSetting : ScriptableObject
{
    public float BGM = 1.0f;
    public float SE = 1.0f;
    public bool Mute = false;
    public AudioClip[] se_AudioClip;

    public void Init()
    {
        BGM = 1.0f;
        SE = 1.0f;
        Mute = false;
    }

}
