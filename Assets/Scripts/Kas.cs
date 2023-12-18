using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kas : MonoBehaviour
{
    public GameObject water;
    IEnumerator MyCoroutine()
    {
        yield return new WaitForSeconds(3f);
        water.SetActive(true);
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bucket")
        {
            StartCoroutine(MyCoroutine());
        }
    }
}