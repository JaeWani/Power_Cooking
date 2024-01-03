using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestManager : MonoBehaviour
{
    private Queue<GameObject> guests = new Queue<GameObject>();

    [SerializeField] int guestAppearanceTime;
    int currentGuestAppearanceTime;
    [SerializeField] int guestLineUpOrder;
    int currentGuestLineUpOrder;

    public void AddGuest(string command)
    {
        //guests.Enqueue(Instantiate(guest));
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
            currentGuestLineUpOrder = Random.Range(1, guestLineUpOrder + 1);
            Debug.Log("currentGuestLineUpOrder : " + currentGuestLineUpOrder);
            
        }
    }
}
