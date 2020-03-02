using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingBlink : MonoBehaviour
{
    public GameObject[] stages;
    [SerializeField] GameObject _obstacles = null;
    [SerializeField] Hatch _hatch = null;
    [SerializeField] private Transform teleportPoint;
    int stage = 0;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<Blink>().blinkEvent += OnBlink;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBlink()
    {
        if(stage == 0)
        {
            _obstacles.SetActive(false);
            _hatch.Unlock();
        }
        else if(stage == 1)
        {
            stage++;
        }
        else if(stage == 2)
        {
            FindObjectOfType<PlayerMovement>().gameObject.transform.position = teleportPoint.position;
        }
        else
        {

        }
    }

    public void IncreaseStage() {
        stage++;
        stages[0].SetActive(false);
        stages[1].SetActive(true);
    }
}
