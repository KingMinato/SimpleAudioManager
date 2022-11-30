using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
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
            if (Files[i].PlayOnStart)
            {
                Files[i].AudioSource.Play();
            }
            if (Files[i].Loop)
            {
                Files[i].AudioSource.loop = true;
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

    IEnumerator PlayWithLoop(string fileName)
    {
        for (int i = 0; i < Files.Count; i++)
        {
            if (Files[i].name == fileName)
            {
                for (int j = 0; j < Files[i].LoopAmount; j++)
                {
                    Files[i].AudioSource.Play();
                    yield return new WaitForSeconds(Files[i].AudioClip.length);
                }
            }
        }
    }

    public void Play(string fileName)
    {
        bool correctName = false;
        for (int i = 0; i < Files.Count; i++)
        {
            if (Files[i].Name == fileName)
            {
                if (Files[i].Loop)
                {
                    StartCoroutine(PlayWithLoop(fileName));
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
