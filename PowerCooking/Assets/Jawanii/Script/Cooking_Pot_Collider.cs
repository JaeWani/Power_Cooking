using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooking_Pot_Collider : MonoBehaviour
{
    private BoxCollider boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    [SerializeField] private Cooking_Pot cooking_Pot;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) cooking_Pot.isPlayer = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) cooking_Pot.isPlayer = false;
    }
}
