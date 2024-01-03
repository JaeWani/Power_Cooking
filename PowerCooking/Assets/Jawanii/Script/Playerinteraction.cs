using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerinteraction : MonoBehaviour
{
    public FoodKind currentFood = FoodKind.Null;

    public void AddFood(FoodKind foodKind) => currentFood = foodKind;
    public void DropFood() => currentFood = FoodKind.Null;
}
