using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Cooking_Pot : MonoBehaviour
{
    #region  Variable;

    [SerializeField] protected GameObject foodPrefab;

    public FoodKind currentKind;

    public float coolTime = 0;

    private float curCoolTime = 0;

    private bool canCook;

    public bool isPlayer;
    public bool isPlaying;

    private RaycastHit raycastHit;
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

        if (Physics.BoxCast(transform.position + Vector3.right / 4, transform.lossyScale / 2, Vector3.right / 2, out raycastHit, transform.rotation, maxDistance))
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
        Cook();
    }

    protected virtual void Cook()
    {
        if (isPlayer)
        {
            if (!isPlaying)
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
            int fail = 0;
            for (int i = 0; i < keyAmount; i++)
            {
                yield return new WaitForSeconds(0.5f);
                var obj = Instantiate(keyPrefab, GameManager.instance.keyCanvas.transform).GetComponent<KeyObject>();
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

                if (fail > 0)
                {
                    var player = raycastHit.transform.GetComponent<Playerinteraction>();
                    player.AddFood(currentKind);
                    Instantiate(foodPrefab, player.transform);
                    break;
                }

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
            var obj = Instantiate(beatPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity).GetComponent<TextMesh>();
            int count = beatAmount;
            while (true)
            {
                obj.text = count.ToString();
                yield return null;
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)) count--;

                if (count <= 0)
                {
                    var player = raycastHit.transform.GetComponent<Playerinteraction>();
                    player.AddFood(currentKind);
                    var food = Instantiate(foodPrefab, player.transform);
                    food.transform.localPosition = new Vector3(0,1,0);
                    break;
                }
            }
            Destroy(obj.gameObject);
            isPlaying = false;
        }
    }
}
