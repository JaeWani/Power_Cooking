using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoodKind
{
    Null =0,
    Bread = 4,
    Carrot = 5,
    Cheese = 6,
    Ham = 7,
    Tomato = 8,
    Stew = 1,
    Cooked_Meat = 2,
    HamBuger = 3
}
public class Food : MonoBehaviour
{
    public FoodKind foodKind;
}
