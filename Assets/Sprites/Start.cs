using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Start : MonoBehaviour
{
    public void Scenes (int numberScenes)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(numberScenes);
    }
}
