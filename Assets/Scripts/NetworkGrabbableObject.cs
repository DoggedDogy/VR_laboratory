using UnityEngine;
using Unity.Netcode;
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkGrabbableObject : NetworkBehaviour
{
    private XRGrabInteractable grabInteractable;
    ulong clientID;
    private NetworkObject networkObject;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        grabInteractable = GetComponent<XRGrabInteractable>();
        networkObject = GetComponent<NetworkObject>();
        clientID = NetworkManager.Singleton.LocalClientId;


        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed);

        }
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        RequestOwnershipServerRpc(clientID);
    }

    [ServerRpc(RequireOwnership = false)]
    private void RequestOwnershipServerRpc(ulong clientID)
    {
        networkObject.ChangeOwnership(clientID);
    }
}