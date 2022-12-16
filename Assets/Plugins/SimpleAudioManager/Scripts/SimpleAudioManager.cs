using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAudioManager : MonoBehaviour
{
    public List<AudioFile> Files = new List<AudioFile>();

    private void Awake()
    {
        GetAudioSources();
    }

    private void Start()
    {
        for (int i = 0; i < Files.Count; i++)
        {
            Files[i].AudioSource.volume = Files[i].AudioVolume / 100f;
            if (Files[i].PlayOnStart)
            {
                if (Files[i].Loop)
                {
                    if (Files[i].InfiniteLoop)
                    {
                        Files[i].AudioSource.loop = true;
                        Files[i].AudioSource.Play();
                    }
                    else
                    {
                        StartCoroutine(PlayWithLoop(Files[i].Name));
                    }
                }
                else
                {
                    Files[i].AudioSource.Play();
                }
            }
        }
    }

    #region Methods
    private void GetAudioSources()
    {
        foreach (AudioFile file in Files) 
        {
            file.AudioSource = this.gameObject.AddComponent<AudioSource>();
            file.AudioSource.clip = file.AudioClip;
        }

    }

    private void ErrorMessageWrongName(string fileName)
    {
        Debug.LogError("Could not find file: " + fileName + ". Please check if File exists.");
    }

    private IEnumerator PlayWithLoop(string fileName)
    {
        for (int i = 0; i < Files.Count; i++)
        {
            if (Files[i].Name == fileName)
            {
                for (int j = 0; j < Files[i].LoopAmount; j++)
                {
                    Files[i].AudioSource.Play();
                    yield return new WaitForSeconds(Files[i].AudioClip.length);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileName: Als Parameter wird der Name der Audio File mitgegeben."></param>
    public void Play(string fileName)
    {
        bool correctName = false;
        for (int i = 0; i < Files.Count; i++)
        {
            if (Files[i].Name == fileName)
            {
                if (Files[i].Loop)
                {
                    if (Files[i].InfiniteLoop)
                    {
                        Files[i].AudioSource.loop = true;
                        Files[i].AudioSource.Play();
                    }
                    else
                    {
                        StartCoroutine(PlayWithLoop(Files[i].Name));
                    }
                }
                else
                {
                    Files[i].AudioSource.Play();
                }
                correctName = true;
                break;
            }
        }
        if (!correctName)
            ErrorMessageWrongName(fileName);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileName: Als Parameter wird der Name der Audio File mitgegeben."></param>
    public void Stop(string fileName)
    {
        bool correctName = false;
        for (int i = 0; i < Files.Count; i++)
        {
            if (Files[i].Name == fileName)
            {
                if (Files[i].AudioSource.isPlaying)
                {
                    Files[i].AudioSource.Stop();
                }
                correctName = true;
                break;
            }
        }
        if (!correctName)
            ErrorMessageWrongName(fileName);
    }

    public void Stop()
    {
        foreach (AudioFile file in Files)
        {
            file.AudioSource.Stop();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileName: Als Parameter wird der Name der Audio File mitgegeben."></param>
    /// <param name="newVolume: Setzt die Lautstaerke in Prozent."></param>
    public void SetVolume(string fileName, float newVolume)
    {
        bool correctName = false;
        for (int i = 0; i < Files.Count; i++)
        {
            if (Files[i].Name == fileName)
            {
                Files[i].AudioSource.volume = newVolume;
                correctName = true;
                break;
            }
        }
        if (!correctName)
            ErrorMessageWrongName(fileName);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileName: Als Parameter wird der Name der Audio File mitgegeben."></param>
    /// <param name="loopState: Setzt den LoopState auf true oder false."></param>
    public void SetLoop(string fileName, bool loopState)
    {
        bool correctName = false;
        for (int i = 0; i < Files.Count; i++)
        {
            if (Files[i].Name == fileName)
            {
                Files[i].Loop = loopState;
                correctName = true;
                break;
            }
        }
        if (!correctName)
            ErrorMessageWrongName(fileName);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileName: Als Parameter wird der Name der Audio File mitgegeben."></param>
    /// <param name="loopAmount: Setzt die Anzahl der Loops. Wird nur beachtet, wenn Loop true und InfiniteLoop false ist."></param>
    public void SetLoopAmount(string fileName, int loopAmount)
    {
        bool correctName = false;
        for (int i = 0; i < Files.Count; i++)
        {
            if (Files[i].Name == fileName)
            {
                Files[i].LoopAmount = loopAmount;
                correctName = true;
                break;
            }
        }
        if (!correctName)
            ErrorMessageWrongName(fileName);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileName: Als Parameter wird der Name der Audio File mitgegeben."></param>
    /// <param name="infiniteLoopState: Setzt den InfiniteLoopState auf true oder false. Ist InfiniteLoop true, wird LoopAmount ignoriert und die Audio File unendlich lange wiederholt."></param>
    public void SetInfiniteLoop(string fileName, bool infiniteLoopState)
    {
        bool correctName = false;
        for (int i = 0; i < Files.Count; i++)
        {
            if (Files[i].Name == fileName)
            {
                Files[i].InfiniteLoop = infiniteLoopState;
                correctName = true;
                break;
            }
        }
        if (!correctName)
            ErrorMessageWrongName(fileName);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileName: Als Parameter wird der Name der Audio File mitgegeben."></param>
    /// <returns>Gibt den InfiniteLoopState als bool zurueck.</returns>
    public bool GetInfiniteLoopState(string fileName)
    {
        for (int i = 0; i < Files.Count; i++)
        {
            if (Files[i].Name == fileName)
            {
                return Files[i].InfiniteLoop;
            }
        }
        ErrorMessageWrongName(fileName);
        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileName: Als Parameter wird der Name der Audio File mitgegeben."></param>
    /// <returns>Gibt die Lautstaerke als float in Prozent zurueck.</returns>
    public float GetVolume(string fileName)
    {
        for (int i = 0; i < Files.Count; i++)
        {
            if (Files[i].Name == fileName)
            {
                return Files[i].AudioSource.volume;
            }
        }
        ErrorMessageWrongName(fileName);
        return 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileName: Als Parameter wird der Name der Audio File mitgegeben."></param>
    /// <returns>Gibt den LoopState als bool zurueck.</returns>
    public bool GetLoopState(string fileName)
    {
        for (int i = 0; i < Files.Count; i++)
        {
            if (Files[i].Name == fileName)
            {
                return Files[i].Loop;
            }
        }
        ErrorMessageWrongName(fileName);
        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileName: Als Parameter wird der Name der Audio File mitgegeben."></param>
    /// <returns>Gibt Anzahl der Loops als int zurueck.</returns>
    public int GetLoopAmount(string fileName)
    {
        for (int i = 0; i < Files.Count; i++)
        {
            if (Files[i].Name == fileName)
            {
                return Files[i].LoopAmount;
            }
        }
        ErrorMessageWrongName(fileName);
        return 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileName: Als Parameter wird der Name der Audio File mitgegeben."></param>
    /// <returns>Gibt als bool zurueck, ob die Audio File abgespielt wird oder nicht.</returns>
    public bool GetPlayingState(string fileName)
    {
        for (int i = 0; i < Files.Count; i++)
        {
            if (Files[i].Name == fileName)
            {
                if (Files[i].AudioSource.isPlaying)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        ErrorMessageWrongName(fileName);
        return false;
    }
    #endregion
}
