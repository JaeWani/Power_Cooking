using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestMovement : MonoBehaviour
{
    [SerializeField] Transform[] target;
    [SerializeField] Transform currentTarget;
    [SerializeField] float speed;
    [SerializeField] int currentOrder;
    public bool isReached;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentOrder < target.Length)
        {
            currentTarget = target[currentOrder];
        }

        if (transform.position != currentTarget.position && !target[currentOrder].GetComponent<Point>().isExisting)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, Time.deltaTime * speed);
            isReached = false;
        }
        else
        {
            isReached = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Point"))
        {
            Debug.Log("»Æ¿Œ");
            currentOrder++;
        }
    }
}
