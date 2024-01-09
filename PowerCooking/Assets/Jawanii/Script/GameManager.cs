using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Unity.Collections;
using UnityEngine.SceneManagement;

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

    public Camera mainCamera;
    public Vector3 cameraStartPos;

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
            if (playerHp == 0)
            {
                Shake_HP(Hp_Img[2]);
                GameOver();
            }
        }
    }
    public int inGameGold;
    public int score;
    public string playerName;

    [Header("UI")]
    [SerializeField] private List<Image> Hp_Img;
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private RectTransform roundRect;
    [SerializeField] private TextMeshProUGUI roundText;
    [SerializeField] private TextMeshProUGUI difficultyText;
    [SerializeField] private TextMeshProUGUI guestText;
    [SerializeField] private TextMeshProUGUI errorText;
    [SerializeField] private RectTransform inputField_Panel;

    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Button enterButton;

    [SerializeField] private RectTransform gameOverPanel;
    [SerializeField] private Button gameOverTitleButton;
    [SerializeField] private Button titleButton;

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
        mainCamera = Camera.main;
        cameraStartPos = mainCamera.transform.position;

        SoundManager.PlaySound("InGame_Background_Music", 0.8f, true);
        enterButton.onClick.AddListener(() => Enter());
        gameOverTitleButton.onClick.AddListener(() =>
        {

            Transitioner.Instance.TransitionToScene("Title");
        });
        titleButton.onClick.AddListener(() =>
        {
            DataManager.instance.AddUserData(playerName, score);
            DataManager.instance.Save();
            UIManager.SetActiveUI(0, false);
            Transitioner.Instance.TransitionToScene("Title");
        });
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12)) PlayerHP--;

        scoreText.text = score.ToString();
    }

    private void Enter()
    {
        if (!string.IsNullOrWhiteSpace(nameInputField.text))
        {

            Debug.Log(nameInputField.text);
            playerName = nameInputField.text;
            inputField_Panel.DOAnchorPosX(-3000, 1).OnComplete(() =>
            {
                StartCoroutine(GameStart());
                inputField_Panel.gameObject.SetActive(false);
            });
        }
        else
        {
            Debug.Log("Null or Empty");
            UIManager.StopAllCoroutine();
            UIManager.SetActiveSelectUI(errorText.gameObject, 0, true);
            UIManager.SetActiveSelectUI(errorText.gameObject, 1.5f, false);

        }
    }
    private void GameOver()
    {
        DataManager.instance.AddUserData(playerName, score);
        DataManager.instance.Save();
        Destroy(playerinteraction.gameObject);
        gameOverPanel.DOAnchorPosX(0, 1);
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
        SoundManager.PlaySound("Success", 1, false);
    }
    public void Fail()
    {
        PlayerHP--;
        currentRoundGuestAmount--;
        GuestManager.instance.DequeuePlayer();
        SoundManager.PlaySound("Fail", 1, false);
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

    public void PlayerCamera()
    {
        Sequence s = DOTween.Sequence();
        mainCamera.transform.DOLocalMove(playerinteraction.transform.position + Vector3.up, 1f).SetEase(Ease.Linear);
        DOVirtual.Float(mainCamera.fieldOfView, 90, 1f, v => SetFieldOfView(v));

    }
    private void SetFieldOfView(float a)
    {
        mainCamera.fieldOfView = a;
    }
    public void CameraOrignalPos()
    {
        DOVirtual.Float(mainCamera.fieldOfView, 22.7f, 1f, v => SetFieldOfView(v));
        mainCamera.transform.DOLocalMove(cameraStartPos, 1f).SetEase(Ease.Linear);
    }
}
