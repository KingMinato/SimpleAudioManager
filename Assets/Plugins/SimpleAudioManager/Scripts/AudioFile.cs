using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioFile", menuName = "Simple Audio Manager/AudioFile")]
public class AudioFile : ScriptableObject
{
    /// <summary>
    /// Der Name wird gebraucht, um die bestimmte AudioFile anzusteuern.
    /// </summary>
    public string Name = "";
    /// <summary>
    /// Die AudioSource wird automatisch erstellt.
    /// </summary>
    [HideInInspector]public AudioSource AudioSource;
    /// <summary>
    /// Der AudioClip ist die Audio Datei, die abgespielt wird.
    /// </summary>
    public AudioClip AudioClip;
    /// <summary>
    /// Die Lautstaerke wird in Prozent berechnet.
    /// </summary>
    public float AudioVolume = 50f;
    /// <summary>
    /// Wenn Loop true ist, wird die Audio beim Abspielen wiederholt.
    /// </summary>
    public bool Loop = false;
    /// <summary>
    /// Wenn Loop true ist, wird die Audio so oft wiederholt wie die LoopAmount. (Ist InfiniteLoop true, wird LoopAmount ignoriert.)
    /// </summary>
    public int LoopAmount = 1;
    /// <summary>
    /// Wenn Loop true ist, wird InfiniteLoop beachtet. Ist InfiniteLoop true, wird die Audio so lange wiederholt, bis sie ausgeschaltet wird.
    /// </summary>
    public bool InfiniteLoop = false;
    /// <summary>
    /// Wenn PlayOnStart true ist, wird die Audio abgespielt, sobald das Object instantiiert wird.
    /// </summary>
    public bool PlayOnStart = false;
}
