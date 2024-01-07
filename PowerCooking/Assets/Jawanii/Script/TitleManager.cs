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
        start.onClick.AddListener(() =>
        {
            Transitioner.Instance.TransitionToScene("asd");
            start.gameObject.SetActive(false);
            ranking.gameObject.SetActive(false);
            rule.gameObject.SetActive(false);
            exit.gameObject.SetActive(false);
        });
        ranking.onClick.AddListener(() =>
        {
            Transitioner.Instance.TransitionToScene("Ranking");
            start.gameObject.SetActive(false);
            ranking.gameObject.SetActive(false);
            rule.gameObject.SetActive(false);
            exit.gameObject.SetActive(false);
        });
        rule.onClick.AddListener(() =>
        {
             Transitioner.Instance.TransitionToScene("Rule");
            start.gameObject.SetActive(false);
            ranking.gameObject.SetActive(false);
            rule.gameObject.SetActive(false);
            exit.gameObject.SetActive(false);
        });
        exit.onClick.AddListener(() => Application.Quit());
    }
}
