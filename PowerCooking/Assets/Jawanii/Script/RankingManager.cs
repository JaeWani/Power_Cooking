using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RankingManager : MonoBehaviour
{
    public TextMeshProUGUI _1stText, _2stText, _3stText, _1stName, _1stScore;
    public Button titleButton;
    private void Start()
    {
        UIManager.SetActiveUI(1, true);

        DataManager.instance.Load();
        string _1st = "";
        string _2st = "";
        string _3st = "";
        if (DataManager.instance.userDatas[0] == null) _1st = "null";
        else _1st = DataManager.instance.userDatas[0].name + " : " + DataManager.instance.userDatas[0].score;
        _1stText.text = _1st;

        if (DataManager.instance.userDatas[1] == null) _2st = "null";
        else _2st = DataManager.instance.userDatas[1].name + " : " + DataManager.instance.userDatas[1].score;
        _2stText.text = _2st;

        if (DataManager.instance.userDatas[2] == null) _3st = "null";
        else _3st = DataManager.instance.userDatas[2].name + " : " + DataManager.instance.userDatas[2].score;
        _3stText.text = _3st;


        if (DataManager.instance.userDatas[0] != null) _1stName.text = DataManager.instance.userDatas[0].name;
        else _1stName.tag = "null";

        if (DataManager.instance.userDatas[0] != null) _1stScore.text = DataManager.instance.userDatas[0].score.ToString();
        else _1stScore.text = "null";

        titleButton.onClick.AddListener(() =>
        {
            Transitioner.Instance.TransitionToScene("Title");
            UIManager.SetActiveUI(0, false);
        });
    }
}
