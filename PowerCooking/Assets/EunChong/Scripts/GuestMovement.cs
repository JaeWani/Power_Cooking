using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    public int currentIndex;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, GuestManager.instance.points[currentIndex].position, Time.deltaTime * moveSpeed);
    }
}
