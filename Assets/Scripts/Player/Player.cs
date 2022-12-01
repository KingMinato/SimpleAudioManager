using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SimpleAudioManager audioManager;

    private void Awake()
    {
        if (audioManager == null)
            audioManager = GetComponent<SimpleAudioManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            audioManager.Play("Hello");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioManager.Play("Jump");
        }
    }
}
