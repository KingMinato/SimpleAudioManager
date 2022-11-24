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

    #region Methods

    private void GetAudioSources()
    {
        foreach (AudioFile file in Files) 
        {
            file.AudioSource = this.gameObject.AddComponent<AudioSource>();
            file.AudioSource.clip = file.AudioClip;
        }

    }

    public void Play(string fileName)
    {
        for (int i = 0; i < Files.Count; i++)
        {
            if (Files[i].Name == fileName)
            {
                Files[i].AudioSource.Play();
                if (Files[i].Loop)
                {
                    Files[i].AudioSource.loop = true;
                }
                break;
            }
        }
    }

    public void Stop(string fileName)
    {
        for (int i = 0; i < Files.Count; i++)
        {
            if (Files[i].Name == fileName)
            {
                if (Files[i].AudioSource.isPlaying)
                {
                    Files[i].AudioSource.Stop();
                }
            }
        }
    }

    #endregion
}
