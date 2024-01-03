using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate_Collider : MonoBehaviour
{
    public Plate plate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) plate.canPlate = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) plate.canPlate = false;
    }
}
