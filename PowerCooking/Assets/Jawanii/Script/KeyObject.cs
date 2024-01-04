using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyObject : MonoBehaviour
{
    public enum KeyState { W, A, S, D }

    public KeyCode keyCode;

    private KeyState currentState;

    [SerializeField] private Text text;



    private void Start()
    {
        RandomState();
    }

    public void RandomState()
    {
        currentState = (KeyState)Random.Range(0, 4);
        switch (currentState)
        {
            case KeyState.W:
                text.text = "W";
                keyCode = KeyCode.W;
                break;
            case KeyState.A:
                text.text = "A";
                keyCode = KeyCode.A;
                break;
            case KeyState.S:
                text.text = "S";
                keyCode = KeyCode.S;
                break;
            case KeyState.D:
                text.text = "D";
                keyCode = KeyCode.D;
                break;
        }
    }

}
