using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float exitTime;
    public bool isFinished;
    public Transform entrance;
    public int currentIndex;
    Transform target;

    private void Update()
    {
        if (isFinished)
        {
            target = entrance;
        }
        else
        {
            target = GuestManager.instance.points[currentIndex];
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);
    }

    public IEnumerator Exit()
    {
        yield return new WaitForSeconds(exitTime);
        Destroy(gameObject);
    }
}
