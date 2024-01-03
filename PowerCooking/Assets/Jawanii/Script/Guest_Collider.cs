using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guest_Collider : MonoBehaviour
{
    [SerializeField] private Guest guest;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            guest.canInteraction = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
          if (other.CompareTag("Player"))
        {
            guest.canInteraction = false;
        }
    }
}
