using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkConection : MonoBehaviour
{
    // Start is called before the first frame update
    public void Host()
    {
        NetworkManager.Singleton.StartHost();
    }

    // Update is called once per frame
    public void Join()
    {
        NetworkManager.Singleton.StartClient();
    }
}
