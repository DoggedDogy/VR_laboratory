using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class GroupBlock : MonoBehaviour
{
    public List<GameObject> Limits;
    public List<GameObject> items; 
    public List<Transform> basePositions;
    private Dictionary<int, XRSocketInteractor> limitDictionary = new Dictionary<int, XRSocketInteractor>();
    [SerializeField] private GameObject obj;
    [SerializeField] private GameObject prefab;

    public bool isToggle = false;
    public bool mean = false;
    public bool isInstantiatable = false;
    private int i;
    private int k;
    private bool oneTimeStand = false;
    public void Start()
    {
        i = 0;
        foreach (GameObject limit in Limits)
        {
            limitDictionary.Add(i++, limit.GetComponent<XRSocketInteractor>());
        }
    }
    public void OnToggleChange(int key)
    {
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
            if (!isInstantiatable)
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
            if (!isInstantiatable)
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
            Invoke("check", 0.1F);
    }
    void check()
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
