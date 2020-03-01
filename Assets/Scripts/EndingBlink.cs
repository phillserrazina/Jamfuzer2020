using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingBlink : MonoBehaviour
{
    [SerializeField] GameObject _obstacles = null;
    [SerializeField] Hatch _hatch = null;
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
            stage++;
        }
        else if(stage == 1)
        {

        }
        else if(stage == 2)
        {

        }
        else
        {

        }
    }
}
