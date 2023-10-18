using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forward : MonoBehaviour
{
    public void Trigger()
    {
        StartCoroutine(Move());
    }
    
    private IEnumerator Move()
    {
        for (int i = 0; i < 100000; i++)
        {
            transform.position -= transform.forward * 0.0001f;
            yield return new WaitForSeconds(0.001f); 
        }
    }
}
