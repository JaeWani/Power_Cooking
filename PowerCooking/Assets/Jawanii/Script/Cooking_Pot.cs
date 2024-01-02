using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooking_Pot : MonoBehaviour
{

    public int keyAmount;

    public bool isKeyInput;

    [SerializeField] private GameObject keyPrefab;

    [SerializeField] private List<KeyObject> keyObjects = null;



    private void Start()
    {
        keyObjects = new List<KeyObject>(keyAmount);
    }

    private void OnMouseDown()
    {
        Cook();
    }

    protected virtual void Cook()
    {

    }

    protected void KeyInput()
    {
        for (int i = 0; i < keyAmount; i++) keyObjects.Add(Instantiate(keyPrefab, transform).GetComponent<KeyObject>());
    }
}
