using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemRefTracker : MonoBehaviour
{
    public static int currentMemRef = 0;
    private void Start() {
        DontDestroyOnLoad(gameObject);
    }
}
