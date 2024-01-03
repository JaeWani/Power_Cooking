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
    [SerializeField] private Sprite veryGood,good,normal,bad;
    private void Start()
    {
        mainCamera = Camera.main;
        barImage.color = Color.green;
    }

    private void FixedUpdate()
    {
        transform.position = mainCamera.WorldToScreenPoint(guest.orderObject.transform.position + Vector3.up );
    }

    public void StartBar(float maxTime, GuestState guestState)
    {
        StartCoroutine(bar());
        IEnumerator bar()
        {
            float coolTime = maxTime;
            float result = maxTime / 5;
            Debug.Log(coolTime);
            guestState = GuestState.VeryGood;

            while (coolTime > 0.0f)
            {
                Debug.Log("ㅁㄴㅇ");
                coolTime -= Time.deltaTime;
                barImage.fillAmount = coolTime / maxTime;

                if(coolTime <= result * 4 && coolTime >= result * 3)
                {   
                    barImage.color = new Color(100,255,170,100);
                    faceImage.sprite = good;
                }
                else if(coolTime <= result * 3 && coolTime >= result * 2)
                {   
                    barImage.color = Color.yellow;
                    faceImage.sprite = normal;
                }
                else if(coolTime <= result * 2 && coolTime >= result * 1)
                {   
                    barImage.color = Color.red;
                    faceImage.sprite = bad;
                }

                yield return null;
            }
        }
    }
}
