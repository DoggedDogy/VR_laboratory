using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class NetworkAnimateHandOnInput : NetworkBehaviour
{
    public InputActionProperty pinchAnimationAction; // ������
    public Animator HandAnimator;
    public InputActionProperty gripAnimationAction; //������

    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {
            float triggerValue = pinchAnimationAction.action.ReadValue<float>(); // ���������� �������� ��� ������� ������� � triggerValue 
            HandAnimator.SetFloat("Trigger", triggerValue); // ������������� �������� ��� ��������
            float gripValue = gripAnimationAction.action.ReadValue<float>(); // ���������� �������� ��� ������� ������� � gripValue
            HandAnimator.SetFloat("Grip", gripValue); //����������� �������� 
        }
    }
}
