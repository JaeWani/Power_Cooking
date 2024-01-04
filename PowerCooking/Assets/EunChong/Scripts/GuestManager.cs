using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GuestManager : MonoBehaviour
{
    // ť�� ������ ����
    private Queue<Transform> playerQueue = new Queue<Transform>();

    // �÷��̾��� ������ (ť�� �߰��� ���)
    public Transform playerPrefab;

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
    }

    void Update()
    {
        // Ű���� �Է� ���� ���� ���ο� �÷��̾ ť�� �߰��ϴ� ����
        if (Input.GetKeyDown(KeyCode.Z))
        {
            EnqueuePlayer();
        }

        // ť���� �÷��̾ �����ϴ� ����
        if (Input.GetKeyDown(KeyCode.X))
        {
            DequeuePlayer();
        }
    }

    /// <summary>
    /// ť�� �÷��̾� �߰�
    /// </summary>
    void EnqueuePlayer()
    {
        if (playerQueue.Count < points.Length) 
        {
            Transform newPlayer = Instantiate(playerPrefab, transform.position, Quaternion.identity);
            newPlayer.GetComponent<GuestMovement>().entrance = transform;
            playerQueue.Enqueue(newPlayer);
            UpdatePlayerPositions();
        }
    }

    /// <summary>
    /// ť���� �÷��̾� ����
    /// </summary>
    void DequeuePlayer()
    {
        if (playerQueue.Count > 0)
        {
            Transform dequeuedPlayer = playerQueue.Dequeue();
            dequeuedPlayer.GetComponent<GuestMovement>().isFinished = true;
            StartCoroutine(dequeuedPlayer.GetComponent<GuestMovement>().Exit());
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
