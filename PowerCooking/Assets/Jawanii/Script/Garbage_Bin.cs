using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage_Bin : MonoBehaviour
{
    public bool canInteraction = false;
    private void OnMouseDown()
    {
        if (canInteraction) Drop();
    }
    private void Drop()
    {
        var player = GameManager.instance.playerinteraction;
        player.currentFood = FoodKind.Null;
        Destroy(player.foodObject);
    }
}
