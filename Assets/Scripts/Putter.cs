using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Putter : MonoBehaviour
{
    [SerializeField] private Collider collider;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject prefab;
    public void ToggleOnRiveter()
    {
        collider.enabled = true;
        animator.SetTrigger("setRivet");
        Invoke("ToggleOffRiveter", 0.5F);
    }
    public void ToggleOffRiveter()
    {
        collider.enabled = false;
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hole"))
        {
            if (collision.gameObject.GetComponent<MeshRenderer>().enabled)
                Instantiate(prefab, collision.transform.position, collision.transform.rotation, collision.transform);
        }
    }
}
