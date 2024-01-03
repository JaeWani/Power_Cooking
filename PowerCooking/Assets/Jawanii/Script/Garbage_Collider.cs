using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage_Collider : MonoBehaviour
{
    public Garbage_Bin garbage_Bin;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            garbage_Bin.canInteraction = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            garbage_Bin.canInteraction = false;
        }
    }
}
