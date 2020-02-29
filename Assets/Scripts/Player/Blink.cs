using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    public delegate void BlinkDelegate();
    public BlinkDelegate blinkEvent;

    public void PlayerBlink() {
        if (blinkEvent != null) blinkEvent();
    }
}
