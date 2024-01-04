using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GuestManager : MonoBehaviour
{
    // 큐를 저장할 변수
    private Queue<Transform> playerQueue = new Queue<Transform>();

    // 플레이어의 프리팹 (큐에 추가할 대상)
    public Transform[] playerPrefab;

    // 줄서기 포인트
    public Transform[] points;

    public static GuestManager instance = null;

    void Awake()
    {
        if (null == instance)
        {
            //이 클래스 인스턴스가 탄생했을 때 전역변수 instance에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
            instance = this;
        }
        else Destroy(gameObject);
    }

    void Update()
    {
      
    }

    /// <summary>
    /// 큐에 플레이어 추가
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
    /// 큐에서 플레이어 제거
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
    /// 플레이어들의 위치를 업데이트
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
