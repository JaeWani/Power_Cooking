using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Effect
{
    public GameObject EffectPrefab;
    public string key;
}

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;

    public List<Effect> EffectList = new List<Effect>();
    public Dictionary<string, GameObject> EffectDictionary = new Dictionary<string, GameObject>();

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        foreach (var item in EffectList)
        {
            string key = item.key;
            GameObject prefab = item.EffectPrefab;
            EffectDictionary.Add(key, prefab);
        }
    }

    private GameObject _SpawnEffect(string key, Vector3 position)
    {
        return Instantiate(EffectDictionary[key], position, Quaternion.identity);
    }
    public static GameObject SpawnEffect(string key, Vector3 position) => instance._SpawnEffect(key, position);
    
    private GameObject _SpawnEffect(string key, Vector3 position, Transform parent)
    {
        var obj = Instantiate(EffectDictionary[key], position, Quaternion.identity);
        obj.transform.SetParent(parent);
        return obj;
    }
    public static GameObject SpawnEffect(string key, Vector3 position, Transform parent) => instance._SpawnEffect(key,position,parent);
    private GameObject _SpawnEffect(string key, Vector3 position, Vector3 scale)
    {
        var obj = Instantiate(EffectDictionary[key], position, Quaternion.identity);
        obj.transform.localScale = scale;
        return obj;
    }
    public static GameObject SpawnEffect(string key, Vector3 position, Vector3 scale) => instance._SpawnEffect(key,position,scale);
}
