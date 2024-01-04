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
        get { return playerHp; }
        set
        {
            playerHp = value;

            if (playerHp == 2) Shake_HP(Hp_Img[0]);
            if (playerHp == 1) Shake_HP(Hp_Img[1]);
            if (playerHp == 0) Shake_HP(Hp_Img[2]);
        }
    }
    public int inGameGold;
    public int score;

    [Header("UI")]
    [SerializeField] private List<Image> Hp_Img;
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private RectTransform roundRect;
    [SerializeField] private TextMeshProUGUI roundText;
    [SerializeField] private TextMeshProUGUI difficultyText;
    [SerializeField] private TextMeshProUGUI guestText;

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
        if (Input.GetKeyDown(KeyCode.T)) PlayerHP--;
        scoreText.text = score.ToString();

    }
    public void Success(GuestState guestState, float upScore, Vector3 position)
    {
        switch (guestState)
        {
            case GuestState.VeryGood: EffectManager.SpawnEffect("Very_Good", position, new Vector3(0.4f, 0.4f, 0.4f)); break;
            case GuestState.Good: EffectManager.SpawnEffect("Good", position, new Vector3(0.4f, 0.4f, 0.4f)); break;
            case GuestState.Normal: EffectManager.SpawnEffect("SoSo", position, new Vector3(0.4f, 0.4f, 0.4f)); break;
            case GuestState.Bad: EffectManager.SpawnEffect("Bad", position, new Vector3(0.4f, 0.4f, 0.4f)); break;
            case GuestState.VeryBad: EffectManager.SpawnEffect("Terrible", position, new Vector3(0.4f, 0.4f, 0.4f)); break;
        }
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
            currentRoundIndex = i + 1;
            yield return StartCoroutine(RoundStart(rounds[i]));
        }
    }
    public IEnumerator RoundStart(Round round)
    {
        roundRect.DOAnchorPos(new Vector2(-2000, 100), 0);

        string difficulty = "";
        roundText.text = "Round " + currentRoundIndex;
        switch (round.currentDifficulty)
        {
            case Round.Difficulty.veryEasy: difficulty = "Very Easy"; break;
            case Round.Difficulty.easy: difficulty = "Easy"; break;
            case Round.Difficulty.normal: difficulty = "normal"; break;
            case Round.Difficulty.hard: difficulty = "Hard"; break;
            case Round.Difficulty.veryHard: difficulty = "Very Hard"; break;
        }
        difficultyText.text = "Difficulty : " + difficulty;
        guestText.text = "Customer : " + round.roundGuestAmount;

        roundRect.DOAnchorPos(new Vector2(0, 100), 0.5f).SetEase(Ease.OutQuad);
        yield return new WaitForSeconds(3);
        roundRect.DOAnchorPos(new Vector2(2000, 100), 0.5f).SetEase(Ease.OutQuad);


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
        image.rectTransform.DOShakeAnchorPos(1, 30, 10, 90, false).OnComplete(() => image.gameObject.SetActive(false));
    }

}
