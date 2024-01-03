using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerinteraction : MonoBehaviour
{
    public FoodKind currentFood = FoodKind.Null;
    public GameObject foodObject = null;

    public bool isInteraction = false;

    public void AddFood(FoodKind foodKind) => currentFood = foodKind;
    public void DropFood() => currentFood = FoodKind.Null;
}
