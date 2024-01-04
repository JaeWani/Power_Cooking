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
    public Guest guest;

    private void Start()
    {
        guest = GetComponent<Guest>();
    }

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
        if (currentIndex == 0 && !guest.isFilling) guest.Init();
    }

    public IEnumerator Exit()
    {
        transform.eulerAngles = new Vector3(0, 180, 0);
        yield return new WaitForSeconds(exitTime);
        Destroy(gameObject);
    }
}
