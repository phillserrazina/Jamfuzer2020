﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Leap : MonoBehaviour
{
    [SerializeField] Transform _LeapTo = null;
    [SerializeField] float _enableMovementDelayTime = 1.2f;

    CameraEffects _camEf = null;
    Transform _player = null;
    PlayerMovement _playerMovement = null;
    Hatch _hatch = null;

    private void OnValidate()
    {
        if (_camEf == null)
            _camEf = FindObjectOfType<CameraEffects>();
        if (_player == null)
            _player = GameObject.FindGameObjectWithTag("Player").transform;
        if (_playerMovement == null)
            _playerMovement = _player.GetComponent<PlayerMovement>();
        if (_hatch == null)
            _hatch = FindObjectOfType<Hatch>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _camEf.Blink();

            if (_hatch != null)
                _hatch.Lock();

            //Invoke("LeapMe", 0.25f);
            StartCoroutine(LeapMe());
        }
    }

    IEnumerator LeapMe()
    {
        yield return new WaitForSeconds(0.25f);
        _playerMovement.enabled = false;
        _player.position = _LeapTo.position;

        var pManager = FindObjectOfType<ProgressionManager>();
        if (MemRefTracker.currentMemRef < pManager.memoryReferences.Length)
            pManager.NextMemory();
        else
            SceneManager.LoadScene(2);
            
        FindObjectOfType<SubtitlesManager>().LockDescriptions();

        yield return new WaitForSeconds(_enableMovementDelayTime);
        _playerMovement.enabled = true;

    }
}
