using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Hatch : MonoBehaviour
{
    Vector3 defaultPos = Vector3.zero;

    Coroutine currentCoroutine;

    private void Start()
    {
        defaultPos = transform.position;
    }

    [Button]
    public void Unlock()
    {
        currentCoroutine = StartCoroutine(Move());
    }

    [Button]
    public void Lock()
    {
        StopCoroutine(currentCoroutine);
        transform.position = defaultPos;
    }

    IEnumerator Move()
    {
        Vector3 newPosition = transform.position;
        newPosition.x += 3.5f;

        while(Mathf.Abs(transform.position.x - newPosition.x) > 0.2)
        {
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime);
            yield return null;
        }
    }
}
