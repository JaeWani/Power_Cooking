using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleManager : MonoBehaviour
{

    public Button start, ranking, rule, exit;
    void Start()
    {
        UIManager.SetActiveUI(1,true);

        start.onClick.AddListener(() =>
        {
            Transitioner.Instance.TransitionToScene("asd");
            UIManager.SetActiveUI(0,false);
        });
        ranking.onClick.AddListener(() =>
        {
            Transitioner.Instance.TransitionToScene("Ranking");
            UIManager.SetActiveUI(0,false);
        });
        rule.onClick.AddListener(() =>
        {
            Transitioner.Instance.TransitionToScene("Rule");
            UIManager.SetActiveUI(0,false);
        });
        exit.onClick.AddListener(() => Application.Quit());
    }
}
