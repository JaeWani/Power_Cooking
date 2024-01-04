using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region  Variable
    public static GameManager instance;

    public Canvas mainCanvas;
    public Canvas keyCanvas;

    public Playerinteraction playerinteraction;

    public List<Sprite> foodSprites = new List<Sprite>();
    public Sprite checkSprite;

    [Header("In Game")]
    public int playerHp = 3;
    public int inGameGold;
    public int score;
    #endregion


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
