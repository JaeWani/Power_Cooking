using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooking_Pot : MonoBehaviour
{
    #region  Variable;

    [Header ("Input")]
    public int keyAmount = 4;

    [Header ("Beat")]
    public int beatAmount = 30;

    [Header ("Prefab")]

    [SerializeField] private GameObject keyPrefab;
    [SerializeField] private GameObject beatPrefab;

    protected enum Pot_Type
    {
        Input, Beat
    }

    protected Pot_Type currentType;

    #endregion

    private void Start()
    {

    }

    private void Update()

    {
    }

    private void OnMouseDown()
    {
        KeyBeat();
    }

    protected virtual void Cook()
    {
        KeyInput();
    }

    protected void KeyInput()
    {
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
                        Debug.Log("성공");
                        Destroy(obj.gameObject);
                        break;
                    }
                    else if (Input.GetKeyDown(KeyCode.W) && !Input.GetKeyDown(obj.keyCode))
                    {
                        Debug.Log("실패");
                        Destroy(obj.gameObject);
                        fail++;
                        break;
                    }
                    else if (Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(obj.keyCode))
                    {
                        Debug.Log("실패");
                        Destroy(obj.gameObject);
                        fail++;
                        break;
                    }
                    else if (Input.GetKeyDown(KeyCode.S) && !Input.GetKeyDown(obj.keyCode))
                    {
                        Debug.Log("실패");
                        Destroy(obj.gameObject);
                        fail++;
                        break;
                    }
                    else if (Input.GetKeyDown(KeyCode.D) && !Input.GetKeyDown(obj.keyCode))
                    {
                        Debug.Log("실패");
                        Destroy(obj.gameObject);
                        fail++;
                        break;
                    }
                }
                if (fail > 0) break;
            }
        }
    }
    protected void KeyBeat()
    {
        StartCoroutine(func());
        IEnumerator func()
        {
            var obj = Instantiate(beatPrefab,transform.position + new Vector3(0,1,0), Quaternion.identity).GetComponent<TextMesh>();
            while (true)
            {
                obj.text = beatAmount.ToString();
                yield return null;
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))beatAmount--;

                if(beatAmount < 0) break;
            }
            Destroy(obj.gameObject);
        }
    }
}
