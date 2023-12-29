using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRRigReference : MonoBehaviour
{
    public static VRRigReference Singleton;

    public Transform root;
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    private void Awake()
    {
        Singleton = this;
    }
}
