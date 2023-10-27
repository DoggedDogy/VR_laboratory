using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Drill : MonoBehaviour
{
    [SerializeField] private Collider _collider;
    [SerializeField] private Animator animator;

    public void ToggleOnDrill()
    {
        _collider.enabled = true;
        animator.SetBool("isDrilling", true);
    }
    public void ToggleOffDrill()
    {
        _collider.enabled = false;
        animator.SetBool("isDrilling", false);
    }
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Hole"))
        {
            Destroy(collision.gameObject);
        }
    }
}
