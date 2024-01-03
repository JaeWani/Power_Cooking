using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public bool isExisting;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Guest"))
        {
            if (other.GetComponent<GuestMovement>().isReached) 
            {
                isExisting = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Guest"))
        {
            isExisting = false;
        }
    }
}
