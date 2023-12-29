using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class NetworkAnimateHandOnInput : NetworkBehaviour
{
    public InputActionProperty pinchAnimationAction; // подбор
    public Animator HandAnimator;
    public InputActionProperty gripAnimationAction; //сжатие

    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {
            float triggerValue = pinchAnimationAction.action.ReadValue<float>(); // записываем значение при нажатии клавиши в triggerValue 
            HandAnimator.SetFloat("Trigger", triggerValue); // ”станавливаем значение дл€ анимации
            float gripValue = gripAnimationAction.action.ReadValue<float>(); // записываем значение при нажатии клавиши в gripValue
            HandAnimator.SetFloat("Grip", gripValue); //ѕроигрываем анимацию 
        }
    }
}
