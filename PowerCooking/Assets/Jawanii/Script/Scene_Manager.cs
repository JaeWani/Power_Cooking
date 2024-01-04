using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject panel;
    [SerializeField] GameObject back;
    [SerializeField] GameObject buttons;

    public static Scene_Manager instance;

    private void Awake()
    {
        if(instance == null) 
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        buttons.SetActive(false);
        yield return new WaitForSeconds(2.5f);
        buttons.SetActive(true);
    }

    private void Update()
    {
        if (panel != null) 
        {
            if (panel.activeSelf)
            {
                back.SetActive(true);
            }
            else
            {
                back.SetActive(false);
            }
        }
    }

    public void StartGame()
    {
        canvas.SetActive(false);
        Transitioner.Instance.TransitionToScene("asd");
    }

    public void ShowRanking()
    {
        // 다른 씬으로 이동
    }

    public void ShowRule()
    {
        panel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Back()
    {
        panel.SetActive(false);
    }
}
