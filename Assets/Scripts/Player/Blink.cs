using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    public delegate void BlinkDelegate();
    public BlinkDelegate blinkEvent;
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.F)) {
            PlayerBlink();
        }
    }

    private void PlayerBlink() {
        if (blinkEvent != null) blinkEvent();
    }
}
