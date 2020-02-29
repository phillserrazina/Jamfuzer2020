using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leap : MonoBehaviour
{
    [SerializeField] Transform _LeapTo;
    [SerializeField] float _enableMovementDelayTime = 1.2f;
    CameraEffects _camEf;
    Transform _player;
    PlayerMovement _playerMovement;

    private void OnValidate()
    {
        if (_camEf == null)
            _camEf = FindObjectOfType<CameraEffects>();
        if (_player == null)
            _player = GameObject.FindGameObjectWithTag("Player").transform;
        if (_playerMovement == null)
            _playerMovement = _player.GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _camEf.Blink();
            //Invoke("LeapMe", 0.25f);
            StartCoroutine(LeapMe());
        }
    }

    IEnumerator LeapMe()
    {
        yield return new WaitForSeconds(0.25f);
        _playerMovement.enabled = false;
        _player.position = _LeapTo.position;

        yield return new WaitForSeconds(_enableMovementDelayTime);
        _playerMovement.enabled = true;

    }
}
