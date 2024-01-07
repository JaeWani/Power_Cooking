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
}
