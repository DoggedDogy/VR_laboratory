using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndStage : MonoBehaviour
{
    public TextMeshProUGUI txt;
    public GameObject image;
    [SerializeField] private List<GameObject> toggles;
    // Start is called before the first frame update
    public void OnToggleChange()
    {
        int k = 0;
        foreach (var toggle in toggles)
        {
            if (toggle.GetComponent<Toggle>().isOn)
                k++;
            if (!toggle.active)
                k++;
        }
        if (k == toggles.Count)
        {
            image.SetActive(true);
            Invoke("Finish", 10);
        }
    }
    void Update()
    {
        int score = 0;
        foreach (var toggle in toggles)
        {
            if (toggle.GetComponent<Toggle>().isOn)
            {
                score++;
                txt.text = score.ToString()+"/" + toggles.Count.ToString();
            }
        }
    }
    public void Finish()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("START");
    }

    public void Close(GameObject obj)
    {
        obj.SetActive(false); 
    }
    public void Open(GameObject obj)
    {
        obj.SetActive(true);
    }
}
