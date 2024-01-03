using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] GameObject obj;
    [SerializeField] int order;
    private Queue<GameObject> objects = new Queue<GameObject>();

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Instantiate(obj, new Vector3(transform.position.x + order, transform.position.y, transform.position.z), Quaternion.identity);
            AddGuest(obj);
        }
    }

    public void AddGuest(GameObject gameObject)
    {
        objects.Enqueue(gameObject);
        order++;
    }

    public void SendGuest()
    {
        if (objects.Count == 0)
        {
            Debug.Log("No objects to run!");
            return;
        }

        GameObject obj = objects.Dequeue();
        Debug.Log(obj);
    }
}
