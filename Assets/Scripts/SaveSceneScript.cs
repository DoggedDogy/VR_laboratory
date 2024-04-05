using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveSceneScript : MonoBehaviour
{
    float X, Y,Z;
    private Transform player;
    private void Start()
    {
        player = GetComponent<Transform>();
    }
    private void Update()
    {
        X = player.transform.position.x;
        Y = player.transform.position.y;
        Z = player.transform.position.z;
    }
    public void SaveGame()
    {
        PlayerPrefs.SetFloat("X", X);
        PlayerPrefs.SetFloat("Y", Y);
        PlayerPrefs.SetFloat("Z", Z);
        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        if(PlayerPrefs.HasKey("X"))
        {
            X = PlayerPrefs.GetFloat("X");
        }
        if (PlayerPrefs.HasKey("Y"))
        {
            Y = PlayerPrefs.GetFloat("Y");
        }
        if (PlayerPrefs.HasKey("Z"))
        {
            Z = PlayerPrefs.GetFloat("Z");
        }
        player.transform.position = new Vector3(X, Y, Z);
    }
}
