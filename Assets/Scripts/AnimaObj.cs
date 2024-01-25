using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimaObj : MonoBehaviour
{
    Animator anim;

    IEnumerator MyCoroutine2()
    {
        yield return new WaitForSeconds(5f);
        anim.SetBool("isOpen", true);
       // anim.SetBool("isOpen", false);
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        anim = GetComponent<Animator>();
        if (other.gameObject.tag == "poke")
        {
            StartCoroutine(MyCoroutine2());
        }
    }
}


