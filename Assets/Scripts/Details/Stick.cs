using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour, DetailAnimator
{
    public IEnumerator DoAnimation()
    {
        for (;;)
        {
            transform.position -= transform.forward * 0.0002f;
            yield return new WaitForSeconds(0.001f);
        }
    }
}
