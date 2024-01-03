using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoodKind
{
    Null,
    Stew,
    Bread,
    Carrot,
    Cheese,
    Ham,
    Lettuce,
    Onion,
    Potato,
    Steak,
    Tomato,
}
public class Food : MonoBehaviour
{
    public FoodKind foodKind;
}
