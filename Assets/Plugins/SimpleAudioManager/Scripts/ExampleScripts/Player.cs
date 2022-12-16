using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private SimpleAudioManager audioManager;
    private RaycastHit hit;
    NavMeshAgent agent;

    private void Awake()
    {
        audioManager = GetComponent<SimpleAudioManager>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 150))
            {
                agent.SetDestination(hit.point);
            }
        }
        if (agent.velocity != Vector3.zero)
        {
            if (!audioManager.GetPlayingState("Run"))
            {
                audioManager.Play("Run");
            }
        }
        else if (audioManager.GetPlayingState("Run"))
        {
            audioManager.Stop("Run");
        }
    }
}
