using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDecorations : MonoBehaviour
{
    [SerializeField] Transform tepFrom = null;

    [SerializeField] List<GameObject> dec = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < dec.Count; i++)
        {
            dec[i] = Instantiate(dec[i]);
            dec[i].transform.position = tepFrom.position;
            dec[i].AddComponent<Rigidbody>();
            dec[i].GetComponent<Rigidbody>().isKinematic = true;
        }

        InvokeRepeating("SpawnRandomDecoration", 1f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    int currentIndex = 0;
    public void SpawnRandomDecoration()
    {
        Vector3 newpos = tepFrom.position;
        newpos.x = Random.Range(-20f, 20f);
        tepFrom.position = newpos;
        currentIndex = (currentIndex + 1) % dec.Count;
        Rigidbody rb = dec[currentIndex].GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.angularVelocity = new Vector3(Random.Range(-50f, 50f), Random.Range(-50f, 50f), Random.Range(-50f, 50f));
        dec[currentIndex].transform.position = tepFrom.position;
        StartCoroutine(DisableMe(rb));
    }

    IEnumerator DisableMe(Rigidbody a)
    {
        yield return new WaitForSeconds(4f);
        a.isKinematic = true;
    }
}
