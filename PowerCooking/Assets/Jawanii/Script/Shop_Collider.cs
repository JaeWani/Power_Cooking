using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_Collider : MonoBehaviour
{
    [SerializeField] private Shop shop;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) shop.canBuy = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) shop.canBuy = false;
    }
}
