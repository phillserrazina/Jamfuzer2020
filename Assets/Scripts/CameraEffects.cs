using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class CameraEffects : MonoBehaviour
{
    public Animator blinkAnim = null;

    private Blink myBlink;

    [SerializeField] private GameObject pauseGui = null;

    private void Start() {
        myBlink = FindObjectOfType<Blink>();
        blinkAnim.SetTrigger("OpenEye");
    }

    private void Update() {
        if (Input.GetButtonDown("Blink")) {
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

    public void PlaySound(string sound) {
        FindObjectOfType<AudioManager>().Play(sound);
    }

    public void ActivatePauseGUI() {
        pauseGui.SetActive(true);
    }
}
