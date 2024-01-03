using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guest : MonoBehaviour
{
    [SerializeField] private GameObject orderObject;

    public int maxOrderAmount;

    public List<FoodKind> foodKinds;
    public List<GameObject> orders;

  


    void Start()
    {
        RandomOrder();
    }

    private void RandomOrder()
    {
        foodKinds = new List<FoodKind>(new FoodKind[Random.Range(1, maxOrderAmount + 1)]);
        List<GameObject> order = new List<GameObject>();
        for (int i = 0; i < foodKinds.Count; i++)
        {
            var kind = Random.Range(1, System.Enum.GetValues(typeof(FoodKind)).Length);
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
            obj.transform.localPosition = new Vector3(x,0.3f,0);
            x++;
        }
        orders = order;
    }
}
