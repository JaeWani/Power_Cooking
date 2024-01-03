using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    public GameObject foodObject;
    public FoodKind foodkind;
    public bool canPlate = false;

    private void OnMouseDown()
    {
        Plating();
    }
    private void Plating()
    {
        if (canPlate)
        {
            Debug.Log("아이고난");
            if (GameManager.instance.playerinteraction.currentFood == FoodKind.Null)
            {
                if (foodkind != FoodKind.Null)
                {
                    var player = GameManager.instance.playerinteraction;
                    player.currentFood = foodkind;
                    var a = player.foodObject = foodObject;
                    a.transform.SetParent(player.transform);
                    a.transform.localPosition = new Vector3(0,1,0);

                    foodObject = null;
                    foodkind =  FoodKind.Null;
                }
            }
            else
            {
                if (foodkind == FoodKind.Null)
                {
                    var player = GameManager.instance.playerinteraction;
                    foodkind = player.currentFood;
                    foodObject = player.foodObject;
                    player.foodObject = null;
                    player.currentFood = FoodKind.Null;

                    foodObject.transform.SetParent(transform);
                    foodObject.transform.localPosition = new Vector3(0,0,0);
                }
                else
                {
                    Debug.Log("접시 참");
                }
            }
        }
    }
}
