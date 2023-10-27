using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Putter : MonoBehaviour
{
    [SerializeField] private Collider _collider;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject prefab;
    [SerializeField] private float animationTime = 0.5F;
    public void ToggleOnRiveter()
    {
        _collider.enabled = true;
        animator.SetTrigger("setRivet");
        Invoke("PutRivet", animationTime/2);
        Invoke("ToggleOffRiveter", animationTime);
    }
    public void ToggleOffRiveter()
    {
        _collider.enabled = false;
    }
    public void PutRivet()
    {
        Instantiate(prefab, _collider.transform.position, _collider.transform.rotation);
    }
}
