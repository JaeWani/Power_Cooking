using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoodKind
{
    Stew

}
public class Food : MonoBehaviour
{
    [SerializeField] protected GameObject foodPrefab;
    
    public FoodKind currentKind;

}
