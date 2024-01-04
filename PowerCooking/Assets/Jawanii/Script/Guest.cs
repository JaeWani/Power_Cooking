using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GuestState
{
    VeryGood,
    Good,
    Normal,
    Bad,
    VeryBad
}

public class Guest : MonoBehaviour
{
    public GameObject orderObject;

    public float time = 100;

    public int maxOrderAmount;
    public int orderAmount;
    public int currentOrderAmount = 0;

    public List<FoodKind> foodKinds;
    public List<GameObject> orders;

    public bool canInteraction = false;

    public GuestState currentGuestState = GuestState.VeryGood;

    public GameObject Fillng_Object;
    private Filling_UI filling;

    void Start()
    {
        RandomOrder();
        filling = Instantiate(Fillng_Object, GameManager.instance.keyCanvas.transform).GetComponent<Filling_UI>();
        filling.guest = this;
        filling.StartBar(time);
    }

    private void OnMouseDown()
    {
        Interaction();
    }

    private void RandomOrder()
    {
        orderAmount = Random.Range(1, maxOrderAmount + 1);
        foodKinds = new List<FoodKind>(new FoodKind[orderAmount]);
        List<GameObject> order = new List<GameObject>();
        for (int i = 0; i < foodKinds.Count; i++)
        {
            var kind = Random.Range(1, 4);
            foodKinds[i] = (FoodKind)kind;
        }
        int x = -1;
        for (int i = 0; i < foodKinds.Count; i++)
        {
            var obj = new GameObject("order").AddComponent<SpriteRenderer>();
            order.Add(obj.gameObject);
            obj.sprite = GameManager.instance.foodSprites[(int)foodKinds[i]];
            obj.transform.SetParent(orderObject.transform);
            obj.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            obj.transform.localPosition = new Vector3(x, 0.3f, 0);
            x++;
        }
        orders = order;
    }
    public void Interaction()
    {
        if (canInteraction)
        {
            for (int i = 0; i < foodKinds.Count; i++)
            {
                if (GameManager.instance.playerinteraction.currentFood == foodKinds[i])
                {
                    Destroy(orders[i]);
                    GameManager.instance.playerinteraction.currentFood = FoodKind.Null;
                    Destroy(GameManager.instance.playerinteraction.foodObject);
                    foodKinds[i] = FoodKind.Null;
                    currentOrderAmount++;
                    break;
                }
            }
            if (currentOrderAmount == maxOrderAmount)
            {

                Debug.Log("S");
                filling.StopAllCoroutines();
            }
        }
    }
}
