using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GuestManager : MonoBehaviour
{
    // ť�� ������ ����
    private Queue<Transform> playerQueue = new Queue<Transform>();

    // �÷��̾��� ������ (ť�� �߰��� ���)
    public Transform[] playerPrefab;

    // �ټ��� ����Ʈ
    public Transform[] points;

    public static GuestManager instance = null;

    void Awake()
    {
        if (null == instance)
        {
            //�� Ŭ���� �ν��Ͻ��� ź������ �� �������� instance�� ���ӸŴ��� �ν��Ͻ��� ������� �ʴٸ�, �ڽ��� �־��ش�.
            instance = this;
        }
        else Destroy(gameObject);
    }

    void Update()
    {
      
    }

    /// <summary>
    /// ť�� �÷��̾� �߰�
    /// </summary>
    public GameObject EnqueuePlayer()
    {
        if (playerQueue.Count < points.Length)
        {
            Transform newPlayer = Instantiate(playerPrefab[Random.Range(0, playerPrefab.Length)], transform.position, Quaternion.identity);
            newPlayer.GetComponent<GuestMovement>().entrance = transform;
            playerQueue.Enqueue(newPlayer);
            UpdatePlayerPositions();
            return newPlayer.gameObject;
        }
        else return null;
    }

    /// <summary>
    /// ť���� �÷��̾� ����
    /// </summary>
    public void DequeuePlayer()
    {
        if (playerQueue.Count > 0)
        {
            Transform dequeuedPlayer = playerQueue.Dequeue();
            var guestMovement = dequeuedPlayer.GetComponent<GuestMovement>();
            guestMovement.isFinished = true;
            Destroy(guestMovement.guest.filling.gameObject);
            StartCoroutine(guestMovement.Exit());
            UpdatePlayerPositions();
        }
    }

    /// <summary>
    /// �÷��̾���� ��ġ�� ������Ʈ
    /// </summary>
    void UpdatePlayerPositions()
    {
        int positionIndex = 0;
        foreach (Transform player in playerQueue)
        {
            player.GetComponent<GuestMovement>().currentIndex = positionIndex;
            positionIndex++;
        }
    }
}
