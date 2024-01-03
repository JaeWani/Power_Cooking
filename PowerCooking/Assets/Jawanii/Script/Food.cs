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
    Lettuce,
    Onion,
    Potato,
    Steak,
    Tomato,
    Meat,
    Stew,
    HamBuger,
}
public class Food : MonoBehaviour
{
    public FoodKind foodKind;
}
