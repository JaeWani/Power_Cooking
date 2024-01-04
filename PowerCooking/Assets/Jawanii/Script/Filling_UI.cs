using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Filling_UI : MonoBehaviour
{
    public Guest guest;
    private Camera mainCamera;

    [SerializeField] private Image barImage;
    [SerializeField] private Image faceImage;
    [SerializeField] private Sprite veryGood, good, normal, bad, veryBad;

    [SerializeField] private Color LightGreen, Orange;
    private void Start()
    {
        mainCamera = Camera.main;
        barImage.color = Color.green;
    }

    private void FixedUpdate()
    {
        transform.position = mainCamera.WorldToScreenPoint(guest.orderObject.transform.position + Vector3.up);
    }

    public void StartBar(float maxTime)
    {
        StartCoroutine(bar(maxTime));

    }
    private IEnumerator bar(float maxTime)
    {
        float coolTime = maxTime;
        float result = maxTime / 5;
        guest.currentGuestState = new GuestState();
        guest.currentGuestState = GuestState.VeryGood;

        while (coolTime > 0.0f)
        {
            coolTime -= Time.deltaTime;
            barImage.fillAmount = coolTime / maxTime;
            if (coolTime <= result * 5 && coolTime > result * 4)
            {
                barImage.color = Color.green;
                faceImage.sprite = veryGood;
                guest.currentGuestState = GuestState.VeryGood;
            }
            else if (coolTime <= result * 4 && coolTime > result * 3)
            {
                barImage.color = LightGreen;
                faceImage.sprite = good;
                guest.currentGuestState = GuestState.Good;
            }
            else if (coolTime <= result * 3 && coolTime > result * 2)
            {
                barImage.color = Color.yellow;
                faceImage.sprite = normal;
                guest.currentGuestState = GuestState.Normal;
            }
            else if (coolTime <= result * 2 && coolTime > result * 1)
            {
                barImage.color = Orange;
                faceImage.sprite = bad;
                guest.currentGuestState = GuestState.Bad;
            }
            else if (coolTime <= result * 1 && coolTime > 0)
            {
                barImage.color = Color.red;
                faceImage.sprite = veryBad;
                guest.currentGuestState = GuestState.VeryBad;
            }

            yield return null;
        }
        GameManager.instance.Fail();
        // 시간 0되면 할 것
    }
}
