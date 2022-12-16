using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    private SimpleAudioManager simpleAudioManager;

    private void Awake()
    {
        simpleAudioManager = GetComponent<SimpleAudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            simpleAudioManager.Play("Hello");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            simpleAudioManager.Play("Bye");
        }
    }
}