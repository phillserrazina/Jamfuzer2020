using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class CameraEffects : MonoBehaviour
{
    [SerializeField] private Animator blinkAnim = null;

    [Button]
    public void Blink()
    {
        if (!blinkAnim.GetCurrentAnimatorStateInfo(0).IsName("Blink"))
        {
            blinkAnim.SetTrigger("Blink");    
        }
    }
}
