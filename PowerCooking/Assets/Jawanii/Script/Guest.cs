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

    public List<FoodKind> foodKinds;
    public List<GameObject> orders;

    public bool canInteraction = false;

    public GuestState currentGuestState = GuestState.VeryGood;

    public GameObject Fillng_Object;

    void Start()
    {
        RandomOrder();
        var filling = Instantiate(Fillng_Object,GameManager.instance.keyCanvas.transform).GetComponent<Filling_UI>();
        filling.StartBar(time,currentGuestState);
        filling.guest = this;
    }

    private void OnMouseDown()
    {
        Interaction();
    }

    private void RandomOrder()
    {
        foodKinds = new List<FoodKind>(new FoodKind[Random.Range(1, maxOrderAmount + 1)]);
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
            Debug.Log("asd");
            for (int i = 0; i < foodKinds.Count; i++)
            {
                if (GameManager.instance.playerinteraction.currentFood == foodKinds[i])
                {
                    Debug.Log("어익호난");
                    Destroy(orders[i]);
                    GameManager.instance.playerinteraction.currentFood = FoodKind.Null;
                    Destroy(GameManager.instance.playerinteraction.foodObject);
                    foodKinds[i] = FoodKind.Null;
                    break;
                }
                else Debug.Log("엄");
            }
        }
    }
}
