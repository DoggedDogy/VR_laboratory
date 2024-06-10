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
    public WebManager webManager;
    int score = 0;
    private void Start()
    {

        if (GameObject.Find("WebComponent"))
        {
            webManager = GameObject.Find("WebComponent").gameObject.GetComponent<WebManager>();
        }
    }
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
        if (k != score)
        {
            score = k;
            txt.text = score.ToString() + "/" + toggles.Count.ToString();
            webManager.work.Done_Steps = score;
            webManager.sendPutRequest();
        }
    }
    public void OnToggleUpdate()
    {
        
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
