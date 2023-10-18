using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnapBlocking : MonoBehaviour
{
    public List<GameObject> Limits;
    [SerializeField] bool blocked;
    XRSocketInteractor socket;
    MeshCollider collider;
    GameObject Oldest;
    void Start()
    {
        socket = this.GetComponent<XRSocketInteractor>();
        collider = this.GetComponent<MeshCollider>();
        if (blocked)
            //collider.isTrigger = false;
            socket.socketActive = false;
    }
    public void check()
    {
        foreach (GameObject limit in Limits)
        {
            if (limit.GetComponent<XRSocketInteractor>().hasSelection)
            {
                blocked = true;
                break;
            }
            else
                blocked = false;
        }
        if (blocked)
        {
            //if (!socket.hasSelection)
            //{
            //    socket.socketActive = false;
            //}
            //socket.keepSelectedTargetValid = false;
            try
            {
                Oldest = socket.GetOldestInteractableSelected().transform.gameObject;
            }
            catch (System.NullReferenceException)
            { }
            //collider.isTrigger = false;
            if (Oldest != null)
            {
                Oldest.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                Oldest.GetComponent<XRGrabInteractable>().trackPosition = false;
                Oldest.GetComponent<XRGrabInteractable>().trackRotation = false;
            }
            if (Oldest == null)
            {
                socket.socketActive = false;
            }
        }
        else
        {
            //if (!socket.hasSelection)
            //{
            //    socket.socketActive = true;
            //}
            //socket.keepSelectedTargetValid = true;
            //collider.isTrigger = true;
            if (Oldest != null)
            {
                Oldest.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                Oldest.GetComponent<XRGrabInteractable>().trackPosition = true;
                Oldest.GetComponent<XRGrabInteractable>().trackRotation = true;
            }

            if (Oldest == null)
            {
                socket.socketActive = true;
            }

            if (socket.transform.gameObject.TryGetComponent<MeshRenderer>(out MeshRenderer _mr))
                _mr.enabled = false;
        }
    }
    IEnumerator GetOldest()
    {
        yield return new WaitForEndOfFrame();
    }
}
