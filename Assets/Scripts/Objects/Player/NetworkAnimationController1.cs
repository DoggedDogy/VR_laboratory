using UnityEngine.InputSystem;
using UnityEngine;
using System;
using Unity.Netcode;

public class NetworkAnimationController : NetworkBehaviour
{
    [SerializeField] private InputActionReference move;
    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        if (IsOwner)
        {
            move.action.started += AnimateLegs;
            move.action.canceled += StopAnimation;
        }
    }

    private void OnDisable()
    {
        if (IsOwner)
        {
            move.action.started -= AnimateLegs;
            move.action.canceled -= StopAnimation;
        }
    }

    private void AnimateLegs(InputAction.CallbackContext obj)
    {
        bool isMovingForward = move.action.ReadValue<Vector2>().y > 0;
        if (isMovingForward)
        {
            animator.SetBool("isWalking", true);
            animator.SetFloat("animSpeed", 1.0f);
        }
        else
        {
            animator.SetBool("isWalking", true);
            animator.SetFloat("animSpeed", -1.0f);
        }
    }
    private void StopAnimation(InputAction.CallbackContext obj)
    {
        animator.SetBool("isWalking", false);
        animator.SetFloat("animSpeed", 0.0f);
    }
}

