using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class GroupBlock : MonoBehaviour
{
    public List<GameObject> Limits; //Список слотов
    public List<GameObject> items; //Список предметов для установки
    public List<Transform> basePositions; //Список начальных позиций
    private Dictionary<int, XRSocketInteractor> limitDictionary = new Dictionary<int, XRSocketInteractor>(); //Список компонентов слотов
    [SerializeField] private GameObject obj; //объект шага в списке действий
    [SerializeField] private GameObject prefab; //Экземпляр объекта для создания в слотах
    [SerializeField] private bool active = true;

    public bool isToggle = false; //ответственность за шаг работы
    public bool mean = false; //на какое действие переключать шаг (установку или изъятие объектов)
    public bool isInstantiatable = false; //1 тип установки предмета
    public bool isTransferable = false; //2 тип установки предмета
    private int i;
    private int k;
    private bool oneTimeStand = false;
    
    public void Awake() //Создание словаря компонентов слотов
    {
        oneTimeStand = !mean;
        i = 0;
        foreach (GameObject limit in Limits)
        {
            limitDictionary.Add(i++, limit.GetComponent<XRSocketInteractor>());
        }
    }
    public void ActivateExecution()
    {
        active = true;
    }
    public void OnToggleChange(int key) //Установка предметов в слоты, кроме того, который инициировал метод
    {
        if (!active)
            return;
        //if (!isTransferable)
        //{
        //    if (isToggle)
        //        Invoke("check", 0.1F);
        //    return;
        //}
        if (limitDictionary[key].hasSelection && !oneTimeStand)
        {
            oneTimeStand = !oneTimeStand;
            if (isInstantiatable)
            {
                foreach (var limit in limitDictionary)
                {
                    if (limit.Key != key)
                    {
                        Instantiate(prefab, limit.Value.gameObject.transform.position, limit.Value.gameObject.transform.rotation);
                    }
                }
            }
            if (isTransferable)
            {
                var itemsAndLimits = items.Zip(limitDictionary, (i, l) => new { item = i, limit = l });
                foreach (var itemAndLimit in itemsAndLimits)
                {
                    if (itemAndLimit.limit.Key != key)
                    {
                        itemAndLimit.item.transform.position = itemAndLimit.limit.Value.attachTransform.position;
                        itemAndLimit.item.transform.rotation = itemAndLimit.limit.Value.transform.rotation;
                        if (itemAndLimit.limit.Value.TryGetComponent<MeshRenderer>(out MeshRenderer mr))
                            mr.enabled = false;
                    }
                }
                itemsAndLimits = null;
            }
        }
        else if (!limitDictionary[key].hasSelection && oneTimeStand)
        {
            oneTimeStand = !oneTimeStand;
            if (isInstantiatable)
            {
                foreach (var limit in limitDictionary)
                {
                    if (limit.Key != key)
                        Destroy(limit.Value.GetOldestInteractableSelected().transform.gameObject);
                }
            }
            if (isTransferable)
            {
                int num = 0;
                var itemsAndLimits = items.Zip(limitDictionary, (i, l) => new { item = i, limit = l });
                foreach (var itemAndLimit in itemsAndLimits)
                {
                    if (itemAndLimit.limit.Key != key)
                    {
                        itemAndLimit.limit.Value.GetComponent<XRSocketInteractor>().socketActive = false;
                        itemAndLimit.item.transform.position = basePositions[num].position;
                        itemAndLimit.item.transform.rotation = basePositions[num].rotation;
                        //itemAndLimit.limit.Value.GetComponent<XRSocketInteractor>().socketActive = true;
                    }
                    num++;
                }
                itemsAndLimits = null;
            }
        }
        if (isToggle)
            Invoke("check", 0.5F);
    }
    void check() //переключение состояния шага на "выполнено"
    {
        k = Limits.Count;
        foreach (var limit in Limits)
        {
            if (limit.TryGetComponent(out XRSocketInteractor xrSocket))
                if (xrSocket.hasSelection == mean)
                    k--;
        }
        try
        {
            if (k == 0)
                obj.GetComponent<Toggle>().isOn = true;
        }
        catch (System.NullReferenceException)
        {

        }
    }
}
