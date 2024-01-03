using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestManager : MonoBehaviour
{
    public List<Vector3> ticketQueuePos = new List<Vector3>();
    public List<GameObject> npcInQueue = new List<GameObject>();

    void Start()
    {
        // yes, I should use a for cicle, but the positions in reality would be different :)
        ticketQueuePos.Add(new Vector3(8, 0, 0));
        ticketQueuePos.Add(new Vector3(10, 0, 0));
        ticketQueuePos.Add(new Vector3(12, 0, 0));
        ticketQueuePos.Add(new Vector3(14, 0, 0));
        ticketQueuePos.Add(new Vector3(16, 0, 0));

        for (int i = 0; i < 5; i++)
        {
            Instantiate(npcInQueue[i], ticketQueuePos[i], transform.rotation);
        }
    }

    public void TicketAcquistato()
    {
        if (npcInQueue.Count != 0)
        {
            npcInQueue.RemoveAt(0);
        }

        for (int i = 0; i < npcInQueue.Count; i++)
        {

            npcInQueue[i].transform.position = ticketQueuePos[i]; //not working
        }
    }
}
