using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Drill : MonoBehaviour
{
    [SerializeField] private Collider collider;
    [SerializeField] private Animator animator;
    public void ToggleOnDrill()
    {
        collider.enabled = true;
        animator.SetBool("isDrilling", true);
    }
    public void ToggleOffDrill()
    {
        collider.enabled = false;
        animator.SetBool("isDrilling", false);
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hole"))
        {
            collision.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
