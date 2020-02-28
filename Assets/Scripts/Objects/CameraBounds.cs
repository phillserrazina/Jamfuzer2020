using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    private Camera cam;

    public static Vector3 screenBottomLeft;
    public static Vector3 screenTopRight;
    public static Vector3 screenBottomRight;
    public static Vector3 screenTopLeft;

    private void Awake() {
        cam = Camera.main;
    }

    private void Update() {
        screenBottomLeft = cam.WorldToViewportPoint(new Vector3(0, 0, transform.position.z));
        screenTopRight = cam.WorldToViewportPoint(new Vector3(1, 1, transform.position.z));
 
        screenBottomRight = cam.WorldToViewportPoint(new Vector3(1, 0, transform.position.z));
        screenTopLeft = cam.WorldToViewportPoint(new Vector3(0, 1, transform.position.z));
    }
}
