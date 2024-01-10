using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class UserData
{
    public string name;
    public float score;
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public List<UserData> userDatas;
    public List<UserData> defaultDatas = new List<UserData>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
         Load();
        int result = userDatas.Count;
        Debug.Log(result);
    }
    public void Save()
    {
        Debug.Log("아 제발");
        ES3.Save("New_Data", userDatas);
    }
    public void Load()
    {
        userDatas = ES3.Load<List<UserData>>("New_Data",defaultDatas);
        userDatas.Sort(delegate (UserData A, UserData B) { return A.score.CompareTo(B.score); });
        userDatas.Reverse();
    }

    public void AddUserData(string name, float score)
    {
        userDatas.Sort(delegate (UserData A, UserData B) { return A.score.CompareTo(B.score); });
        var userData = new UserData();
        userData.name = name;
        userData.score = score;
        userDatas.Add(userData);
    }

}
