using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using DG.Tweening;

[System.Serializable]
public class Round
{
    public enum Difficulty
    {
        veryEasy,
        easy,
        normal,
        hard,
        veryHard
    }

    public Difficulty currentDifficulty;

    public int roundGuestAmount;

    [HideInInspector] public float time;
}

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
    [SerializeField] private int playerHp = 3;
    public int PlayerHP
    {
        get{ return playerHp;}
        set
        {
            playerHp = value;
            
            if(playerHp == 2) Shake_HP(Hp_Img[0]);
            if(playerHp == 1) Shake_HP(Hp_Img[1]);
            if(playerHp == 0) Shake_HP(Hp_Img[2]);
        }
    }
    public int inGameGold;
    public int score;

    [Header("UI")]
    [SerializeField] private List<Image> Hp_Img;
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Guest")]
    public List<Round> rounds = new List<Round>();
    public int currentRoundIndex = 0;
    public int currentRoundGuestAmount;
    #endregion


    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(GameStart());
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T)) PlayerHP--;
    }
    public void Success(GuestState guestState, float upScore)
    {
        GuestManager.instance.DequeuePlayer();
        int scr = Mathf.RoundToInt(upScore / ((int)guestState + 1));
        score += scr;
        currentRoundGuestAmount--;
    }
    public void Fail()
    {
        playerHp--;
        currentRoundGuestAmount--;
        GuestManager.instance.DequeuePlayer();
    }
    public IEnumerator GameStart()
    {
        for (int i = 0; i < rounds.Count; i++)
        {
            Debug.Log(i);
            yield return StartCoroutine(RoundStart(rounds[i]));
            yield return new WaitForSeconds(3);
        }
    }
    public IEnumerator RoundStart(Round round)
    {
        float time = 50 - (int)round.currentDifficulty * 5;
        currentRoundGuestAmount = 0;
        for (int i = 0; i < round.roundGuestAmount; i++)
        {
            currentRoundGuestAmount++;
            yield return new WaitForSeconds(0.5f);
            var guest = GuestManager.instance.EnqueuePlayer().GetComponent<Guest>();
            guest.time = time;
        }
        while (currentRoundGuestAmount > 0)
        {
            yield return null;
        }
    }
    private void Shake_HP(Image image)
    {
        image.rectTransform.DOShakeAnchorPos(1,30,10,90,false).OnComplete(() => image.gameObject.SetActive(false));
    }
}
