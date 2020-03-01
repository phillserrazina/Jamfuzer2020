﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Awake() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start() {
        var am = FindObjectOfType<AudioManager>();
        am?.StopAllSound();
        am?.Play("Game Theme");
    }
}
