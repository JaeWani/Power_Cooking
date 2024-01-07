using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rule : MonoBehaviour
{
    [SerializeField] private Button Titlebutton;
    void Start()
    {
        Titlebutton.onClick.AddListener(() => Transitioner.Instance.TransitionToScene("Title"));
    }

}
