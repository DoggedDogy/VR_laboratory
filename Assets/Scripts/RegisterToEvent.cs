using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RegisterToEvent : MonoBehaviour
{
    
    private XRGrabInteractable iteractableDrill;
    private XRGrabInteractable iteractableObject;
    private DetailAnimator animator;
    private Coroutine activeCoroutine;

    public void Start()
    {
        iteractableDrill = FindObjectsOfType<XRGrabInteractable>()
            .First(it => it.TryGetComponent<Drill>(out _));
    }

    public void OnSelect(SelectEnterEventArgs args)
    {
        iteractableObject = (XRGrabInteractable)args.interactableObject;
        animator = iteractableObject.GetComponent<DetailAnimator>();
        iteractableDrill.activated.AddListener(Trigger);
        iteractableDrill.deactivated.AddListener(StopActiveCoroutine);
    }

    public void UnSelect(SelectEnterEventArgs args)
    {
        iteractableDrill.activated.RemoveListener(Trigger);
        iteractableDrill.deactivated.RemoveListener(StopActiveCoroutine);
        iteractableObject = null;
        StopActiveCoroutine(null);
    }
    
    private void Trigger(ActivateEventArgs args)
    {
        activeCoroutine = StartCoroutine(animator.DoAnimation());
    }

    private void StopActiveCoroutine(DeactivateEventArgs args)
    {
        StopCoroutine(activeCoroutine);
        activeCoroutine = null;
    }
   
}
