using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XROffsetGrabInteractable : XRGrabInteractable
{
    private Vector3 initialLocalPos; //позиция по умолчанию
    private Quaternion initialLocalRot; //позиции вращения по умолчанию
    
    // Start is called before the first frame update
    void Start()
    {
        if (!attachTransform)
        {
            GameObject attachPoint = new GameObject("Offest Grab Pivot"); // создание точки присоединения
            attachPoint.transform.SetParent(transform, false); // устанавливаем положение и вращение в ноль
            attachTransform = attachPoint.transform; // установка позиции
        }
        else
        {
            initialLocalPos = attachTransform.localPosition; // возврат позиции по умолчанию
            initialLocalRot = attachTransform.localRotation; // возврат позиции вращения по умолчанию
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject is XRDirectInteractor) // если взаимодействие происходит с помощью направленного действия
        {
            attachTransform.position = args.interactorObject.transform.position; // позиция присоединения такая же, как и позиция объекта
            attachTransform.rotation = args.interactorObject.transform.rotation; // позиция поворта присоединения такая же, как и позиция поворота объекта
        }
        else
        {
            attachTransform.localPosition = initialLocalPos; // возврат позиции по умолчанию
            attachTransform.localRotation = initialLocalRot; // возврат позиции вращения по умолчанию
        }
        base.OnSelectEntered(args);
    }
}
