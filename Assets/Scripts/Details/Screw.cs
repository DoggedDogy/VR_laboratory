using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screw : MonoBehaviour, DetailAnimator 
{
    public IEnumerator DoAnimation()
    {
        for (;;)
        {
            transform.Rotate(0, 0, 1f);
            transform.position -= transform.forward * 0.0005f;
            yield return new WaitForSeconds(0.001f);
        }
    }
}
