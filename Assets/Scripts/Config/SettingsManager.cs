using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public UnityTransport transport;
    public static Settings Settings { get; set; }
    private void Start()
    {
        LoadSettings();
        Debug.Log("IP set to " + transport.ConnectionData.Address + ":" + transport.ConnectionData.Port);
    }
    public void SaveSettings()
    {
        string json = JsonUtility.ToJson(Settings);
        File.WriteAllText(Path.Combine(Application.streamingAssetsPath, "settin.json"), json);
    }

    // Update is called once per frame
    public void LoadSettings()
    {
        string savedJson = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "settin.json"));

        Settings = JsonUtility.FromJson<Settings>(savedJson);
        transport.ConnectionData.Address = Settings.address;
        transport.ConnectionData.Port = Settings.port;
    }
}