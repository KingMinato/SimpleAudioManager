using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioFile", menuName = "Simple Audio Manager/AudioFile")]
public class AudioFile : ScriptableObject
{
    public string Name = "";
    [HideInInspector]public AudioSource AudioSource;
    public AudioClip AudioClip;
    public float AudioVolume = 50f;
    public bool Loop = false;
    public int LoopAmount = 1;
}
