using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class AnimaColor : MonoBehaviour
{
    public GameObject obj;
//    public GameObject obj2;
    public Material[] materials;
    IEnumerator MyCoroutine()
    {
        yield return new WaitForSeconds(3f);
        //obj.GetComponent<Renderer>().material.color = Color.Lerp(Color.gray, Color.magenta, Mathf.Abs(Mathf.Sin(Time.time)));
        obj.GetComponent<Renderer>().material = materials[1];
    }
    IEnumerator MyCoroutine2()
    {
        yield return new WaitForSeconds(5f);
        obj.SetActive(false);
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "poke")
        {
            print("Yes");
            StartCoroutine(MyCoroutine2());
        }
        if (other.gameObject.tag == "flag")
        {
            print("WERTY");
            StartCoroutine(MyCoroutine());
        }
        
    }
}

    
