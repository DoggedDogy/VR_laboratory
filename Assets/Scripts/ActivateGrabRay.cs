using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateGrabRay : MonoBehaviour
{
    public GameObject LeftGrabRay; // луч подбора левой руки
    public GameObject RightGrabRay; // луч подбора правой руки

    public XRDirectInteractor LeftDirectGrab; // направленный луч подбора левой руки
    public XRDirectInteractor RightDirectGrab;// направленный луч подбора правой руки

    // Update is called once per frame
    void Update()
    {
        LeftGrabRay.SetActive(LeftDirectGrab.interactablesSelected.Count == 0); // при удержании предмета луч подбора левой руки не будет отображаться
        RightGrabRay.SetActive(RightDirectGrab.interactablesSelected.Count == 0);// при удержании предмета луч подбора правой руки не будет отображаться
    }
}
