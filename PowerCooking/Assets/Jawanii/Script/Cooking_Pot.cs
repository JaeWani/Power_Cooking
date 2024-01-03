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

    public List<FoodKind> needResource = new List<FoodKind>();
    public List<SpriteRenderer> resources = new List<SpriteRenderer>();

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

    }
    private void Update()
    {
        if (Physics.BoxCast(transform.position, transform.lossyScale * 3, transform.forward, out raycastHit, transform.rotation, maxDistance))
        {
            if (raycastHit.transform.CompareTag("Player"))
            {
                isPlayer = true;
            }
        }
        else isPlayer = false;
    }

    private void OnMouseDown()
    {
        Debug.Log("Asd");
        Cook();
    }

    protected virtual void Cook()
    {
        if (isPlayer)
        {
            Debug.Log("1");
            if (!isPlaying)
            {
                Debug.Log("2");
                if (GameManager.instance.playerinteraction.currentFood == FoodKind.Null)
                {
                    Debug.Log("3");
                    switch (currentType)
                    {
                        case Pot_Type.Input: KeyInput(); break;
                        case Pot_Type.Beat: KeyBeat(); break;
                    }
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
            int fail = 0;
            for (int i = 0; i < keyAmount; i++)
            {
                yield return new WaitForSeconds(0.01f);
                var obj = Instantiate(keyPrefab, GameManager.instance.keyCanvas.transform).GetComponent<KeyObject>();
                obj.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2

                );
                while (true)
                {
                    yield return null;
                    if (Input.GetKeyDown(obj.keyCode))
                    {
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

                if (fail > 0) break;
            }
            if (fail <= 0)
            {
                var player = GameManager.instance.playerinteraction;
                player.AddFood(currentKind);
                var food = Instantiate(foodPrefab, player.transform);
                food.transform.localPosition = new Vector3(0, 1, 0);
                player.foodObject = food;
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
            var obj = Instantiate(beatPrefab, transform.position + Vector3.up * 2, Quaternion.identity).GetComponent<TextMesh>();
            int count = beatAmount;
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
            Destroy(obj.gameObject);
            isPlaying = false;
        }
    }
    public void AddResource(FoodKind foodKind)
    {
        if (foodKind == FoodKind.Null) 
        { 
            
        }
        else
        {
            foreach (var item in needResource)
            {
                if (item == foodKind) { }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(transform.position + transform.forward * raycastHit.distance, transform.lossyScale * 3);
    }
}
