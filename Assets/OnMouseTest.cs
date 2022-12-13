using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseTest : MonoBehaviour
{
    private void OnMouseEnter()
    {
        Debug.Log("ENTER");
    }

    private void OnMouseExit()
    {
        Debug.Log("EXIT");
    }
}
