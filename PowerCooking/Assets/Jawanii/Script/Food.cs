using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoodKind
{
    Null,
    Bread,
    Carrot,
    Cheese,
    Ham,
    Tomato,
    Stew,
    HamBuger,
    Cooked_Meat
}
public class Food : MonoBehaviour
{
    public FoodKind foodKind;
}
