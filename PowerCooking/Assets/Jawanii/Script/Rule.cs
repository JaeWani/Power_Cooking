using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rule : MonoBehaviour
{
    [SerializeField] private Button Titlebutton;
    void Start()
    {
        UIManager.SetActiveUI(1, true);
        Titlebutton.onClick.AddListener(() =>
        {
            UIManager.SetActiveUI(0, false);
            Transitioner.Instance.TransitionToScene("Title");
        });
    }

}
