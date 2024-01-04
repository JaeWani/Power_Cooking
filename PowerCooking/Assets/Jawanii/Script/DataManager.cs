using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{
    public string name;
    public float score;
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public List<UserData> userDatas = new List<UserData>();

    private void Awake()
    {
        if(instance == null) 
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
   
    public void Save() => ES3.Save<List<UserData>>("userData", userDatas);

    public void Load() => userDatas = ES3.Load<List<UserData>>("userData");

    public void AddUserData(string name, float score)
    {
        userDatas.Sort();

        var userData = new UserData();
        userData.name = name;
        userData.score = score;
        userDatas.Add(userData);
    }

}
