using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class CameraEffects : MonoBehaviour
{
    [SerializeField] private Animator blinkAnim = null;

    private Blink myBlink;

    private void Start() {
        myBlink = FindObjectOfType<Blink>();
        blinkAnim.SetTrigger("OpenEye");
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.F)) {
            Blink();
        }
    }

    [Button]
    public void Blink()
    {
        if (!blinkAnim.GetCurrentAnimatorStateInfo(0).IsName("Blink"))
        {
            blinkAnim.SetTrigger("Blink");    
        }
    }

    public void BlinkMechanic() {
        myBlink.PlayerBlink();
    }
}
