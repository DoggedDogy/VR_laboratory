using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    public InputActionProperty pinchAnimationAction; // подбор
    public Animator HandAnimator;
    public InputActionProperty gripAnimationAction; //сжатие

    // Update is called once per frame
    void Update()
    {
        float triggerValue = pinchAnimationAction.action.ReadValue<float>(); // записываем значение при нажатии клавиши в triggerValue 
        HandAnimator.SetFloat("Trigger", triggerValue); // Устанавливаем значение для анимации
        float gripValue = gripAnimationAction.action.ReadValue<float>(); // записываем значение при нажатии клавиши в gripValue
        HandAnimator.SetFloat("Grip", gripValue); //Проигрываем анимацию 
    }
}