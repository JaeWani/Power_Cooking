using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestManager : MonoBehaviour
{
    [SerializeField] GameObject guest;
    [SerializeField] Transform[] spawnPoints; 
    [SerializeField] Transform[] firstLinePoints; 
    [SerializeField] Transform[] secondLinePoints; 
    [SerializeField] Transform[] thirdLinePoints; 

    [SerializeField] int guestAppearanceTime;
    int currentGuestAppearanceTime;
    int currentGuestLineUpOrder;

    public void AddGuest(Transform point, Transform[] points)
    {
        GameObject obj = Instantiate(guest, point.position, Quaternion.identity);
        //obj.GetComponent<GuestMovement>().Init(points);
    }

    public void SendGuest()
    {
        //string cmd = guests.Dequeue();
        //Debug.Log(cmd);
    }

    private void Start()
    {
        StartCoroutine(manageRrestaurant());
    }

    IEnumerator manageRrestaurant()
    {
        while (true)
        {
            currentGuestAppearanceTime = Random.Range(1, guestAppearanceTime + 1);
            Debug.Log("currentGuestAppearanceTime : " + currentGuestAppearanceTime);
            yield return new WaitForSeconds(currentGuestAppearanceTime);
            currentGuestLineUpOrder = Random.Range(0, spawnPoints.Length);
            Debug.Log("currentGuestLineUpOrder : " + currentGuestLineUpOrder);

            switch(currentGuestLineUpOrder)
            {
                case 0 :
                    AddGuest(spawnPoints[currentGuestLineUpOrder], firstLinePoints);
                    break;
                case 1 :
                    AddGuest(spawnPoints[currentGuestLineUpOrder], secondLinePoints);
                    break;
                case 2 :
                    AddGuest(spawnPoints[currentGuestLineUpOrder], thirdLinePoints);
                    break;
            }
        }
    }
}
