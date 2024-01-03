using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Canvas mainCanvas;
    public Canvas keyCanvas;

    public Playerinteraction playerinteraction;

    public List<Sprite> foodSprites = new List<Sprite>();

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
