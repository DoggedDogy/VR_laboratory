using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{
    public Transform root;
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    public Renderer[] meshToDisable;
    // Start is called before the first frame update
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsOwner)
        {
            foreach (var item in meshToDisable)
            {
                item.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {
            root.position = VRRigReference.Singleton.root.position;
            root.rotation = VRRigReference.Singleton.root.rotation;

            head.position = VRRigReference.Singleton.head.position;
            head.rotation = VRRigReference.Singleton.head.rotation;

            leftHand.position = VRRigReference.Singleton.leftHand.position;
            leftHand.rotation = VRRigReference.Singleton.leftHand.rotation;

            rightHand.position = VRRigReference.Singleton.rightHand.position;
            rightHand.rotation = VRRigReference.Singleton.rightHand.rotation;
        }
    }
}
