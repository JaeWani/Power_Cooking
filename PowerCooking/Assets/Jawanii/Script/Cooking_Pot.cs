using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Cooking_Pot : MonoBehaviour
{
    #region  Variable;

    [SerializeField] protected GameObject foodPrefab;
    public GameObject resourceObject;

    public FoodKind currentKind;

    public bool isPlayer;
    public bool isPlaying;
    public bool isSatisfaction = false;
    public bool isPocus = false;

    public int needResourceAmount = 0;
    public int currnetNeedResourceAmount = 0;
    public List<FoodKind> needResource = new List<FoodKind>();
    public List<FoodKind> currentNeedResource = new List<FoodKind>();
    public List<SpriteRenderer> resources = new List<SpriteRenderer>();
    public List<Sprite> sprites = new List<Sprite>();

    public GameObject foodView;

    [SerializeField] private RaycastHit raycastHit;
    private float maxDistance = 3;

    [Header("Input")]
    public int keyAmount = 4;

    [Header("Beat")]
    public int beatAmount = 30;

    [Header("Prefab")]

    [SerializeField] private GameObject keyPrefab;
    [SerializeField] private GameObject beatPrefab;

    protected enum Pot_Type
    {
        Input, Beat
    }

    [SerializeField] protected Pot_Type currentType;

    #endregion
    private void Start()
    {
        if(foodView != null) foodView.SetActive(false);
        needResourceAmount = needResource.Count;
        resourceObject.SetActive(false);
        currentNeedResource = new List<FoodKind>(needResource);
    }


    private void OnMouseDown()
    {
        if (!isPocus) CheckFood();
    }

    protected virtual void Cook()
    {

        if (!isPlaying)
        {
            if (GameManager.instance.playerinteraction.currentFood == FoodKind.Null)
            {
                switch (currentType)
                {
                    case Pot_Type.Input: KeyInput(); break;
                    case Pot_Type.Beat: KeyBeat(); break;
                }
            }
        }
    }

    protected void KeyInput()
    {
        isPlaying = true;
        StartCoroutine(func());
        IEnumerator func()
        {
            if(foodView != null) foodView.SetActive(true);
            int fail = 0;
            for (int i = 0; i < keyAmount; i++)
            {
                yield return new WaitForSeconds(0.01f);
                var obj = Instantiate(keyPrefab, GameManager.instance.keyCanvas.transform).GetComponent<KeyObject>();
                obj.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2);
                while (true)
                {
                    yield return null;
                    if (Input.GetKeyDown(obj.keyCode))
                    {
                        SoundManager.PlaySound("Key");
                        Destroy(obj.gameObject);
                        break;
                    }
                    else if (Input.GetKeyDown(KeyCode.W) && !Input.GetKeyDown(obj.keyCode))
                    {
                        Destroy(obj.gameObject);
                        fail++;
                        break;
                    }
                    else if (Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(obj.keyCode))
                    {
                        Destroy(obj.gameObject);
                        fail++;
                        break;
                    }
                    else if (Input.GetKeyDown(KeyCode.S) && !Input.GetKeyDown(obj.keyCode))
                    {
                        Destroy(obj.gameObject);
                        fail++;
                        break;
                    }
                    else if (Input.GetKeyDown(KeyCode.D) && !Input.GetKeyDown(obj.keyCode))
                    {
                        Destroy(obj.gameObject);
                        fail++;
                        break;
                    }
                }

                if (fail > 0) 
                {
                    if(foodView != null) foodView.SetActive(false);
                    break;
                }
            }
            if (fail <= 0)
            {
                var player = GameManager.instance.playerinteraction;
                player.AddFood(currentKind);
                var food = Instantiate(foodPrefab, player.transform);
                food.transform.localPosition = new Vector3(0, 1, 0);
                player.foodObject = food;
                if(foodView != null) foodView.SetActive(false);
            }
            isPlaying = false;
        }
    }
    protected void KeyBeat()
    {
        isPlaying = true;
        StartCoroutine(func());
        IEnumerator func()
        {
            if(foodView != null) foodView.SetActive(true);

            var p = GameManager.instance.playerinteraction;
            p.isInteraction = true;
            var obj = Instantiate(beatPrefab, transform).GetComponent<TextMesh>();
            obj.transform.localPosition = new Vector3(1,3,0);
            obj.transform.rotation = Quaternion.Euler(0,0,0);
            obj.transform.localScale = new Vector3(0.16f,0.16f,0.16f);
            int count = beatAmount;

            var effect = EffectManager.SpawnEffect("Smoke", transform.position, transform);
            while (true)
            {
                obj.text = count.ToString();
                yield return null;
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)) count--;

                if (count <= 0)
                {
                    var player = GameManager.instance.playerinteraction;
                    player.AddFood(currentKind);
                    var food = Instantiate(foodPrefab, player.transform);
                    food.transform.localPosition = new Vector3(0, 1, 0);
                    player.foodObject = food;
                    break;
                }
            }
            if(foodView != null) foodView.SetActive(false);
            Destroy(effect);
            Destroy(obj.gameObject);
            p.isInteraction = false;
            isPlaying = false;
        }
    }

    private void CheckFood()
    {
        if (isPlayer)
        {
            var player = GameManager.instance.playerinteraction;
            if (player.currentFood == FoodKind.Null)
            {
                if (resourceObject.active) resourceObject.SetActive(false);
                else resourceObject.SetActive(true);
            }
            else
            {
                resourceObject.SetActive(true);
                for (int i = 0; i < currentNeedResource.Count; i++)
                {
                    if (player.currentFood == currentNeedResource[i])
                    {
                        player.currentFood = FoodKind.Null;
                        Destroy(player.foodObject);


                        resources[i].sprite = GameManager.instance.checkSprite;
                        currentNeedResource[i] = FoodKind.Null;

                        currnetNeedResourceAmount++;
                        Debug.Log(currnetNeedResourceAmount);
                        break;
                    }
                    else continue;
                }
            }
            if (currnetNeedResourceAmount >= needResourceAmount)
            {
                Cook();
                for (int i = 0; i < resources.Count; i++)
                {
                    resources[i].sprite = sprites[i];
                    currnetNeedResourceAmount = 0;
                    currentNeedResource = new List<FoodKind>(needResource); ;
                }
            }
        }
    }
}
