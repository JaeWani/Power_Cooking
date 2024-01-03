using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public FoodKind foodKind;
    [SerializeField] private GameObject foodPrefab; 


    public int price;
    public bool canBuy;

    private void OnMouseDown()
    {
        Buy();
    }
    private void Buy()
    {
        if (canBuy)
        {
            if (GameManager.instance.playerinteraction.currentFood == FoodKind.Null)
            {
                if (GameManager.instance.inGameGold > price)
                {
                    GameManager.instance.inGameGold -= price;
                  var player = GameManager.instance.playerinteraction;
                    player.AddFood(foodKind);
                    var food = Instantiate(foodPrefab, player.transform);
                    food.transform.localPosition = new Vector3(0, 1, 0);
                    player.foodObject = food;
                }
                else
                {
                    Debug.Log("너 돈 없다?");
                }
            }
            else
            {
                Debug.Log("너 손에 음식 있다?");
            }
        }
    }
}