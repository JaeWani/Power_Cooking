using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public List<GameObject> SetActiveUIs = new List<GameObject>();


    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public static void StopAllCoroutine() => instance.StopAllCoroutines();

    public static void SetActiveUI(float time, bool isActive) => instance.StartCoroutine(instance._SetActiveUI(time, isActive));

    private IEnumerator _SetActiveUI(float time, bool isActive)
    {
        yield return new WaitForSeconds(time);
        foreach (var item in SetActiveUIs)
        {
            item.SetActive(isActive);
        }
    }

    public static void SetActiveSelectUI(GameObject obj, float time, bool isActive) => instance.StartCoroutine(instance._SetActiveSelectUI(obj, time, isActive));

    private IEnumerator _SetActiveSelectUI(GameObject obj, float time, bool isActive)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(isActive);
    }
}
