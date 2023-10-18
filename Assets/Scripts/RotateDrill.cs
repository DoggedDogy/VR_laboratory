using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateDrill : MonoBehaviour
{
    public void Trigger()
    {
        StartCoroutine(Rotate());
    }
    
    private IEnumerator Rotate()
    {
        for (int i = 0; i < 100000; i++)
        {
            transform.Rotate(0,0,10f);
            yield return new WaitForSeconds(0.001f); 
        }
    }
}
