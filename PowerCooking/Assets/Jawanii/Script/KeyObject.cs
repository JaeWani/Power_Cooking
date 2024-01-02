using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObject : MonoBehaviour
{
    public enum KeyState { Left, Right, Up, Down }

    private KeyState currentState;

    private void Start()
    {
        RandomState();
    }

    public void RandomState() => currentState = (KeyState)Random.Range(0, 4);
}
