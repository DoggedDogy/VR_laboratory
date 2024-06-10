using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Unity.VRTemplate
{
    /// <summary>
    /// Controls the steps in the in coaching card.
    /// </summary>
    public class StepManager : MonoBehaviour
    {
        public GameObject lvl1;
        public GameObject ToSpawn;
        [SerializeField] GameObject Blank;
        public GameObject BlankStep;
        public GameObject BlankName;
        public GameObject BlankDesk;
        public WebManager webManager;

        public Button debug1;
        private void Start()
        {
            BlankName = Blank.transform.GetChild(2).gameObject;
            BlankDesk = Blank.transform.GetChild(4).gameObject;
            BlankStep = Blank.transform.GetChild(3).gameObject;
            webManager.workId = "0";
        }
        [Serializable]
        class Step
        {
            [SerializeField] 
            public GameObject stepObject;
            public string id;

            public Step(GameObject stepObject, string id)
            {
                this.stepObject = stepObject ?? throw new ArgumentNullException(nameof(stepObject));
                this.id = id ?? throw new ArgumentNullException(nameof(id));
            }
        }


        [SerializeField] List<Step> m_StepList = new List<Step>();

        int m_CurrentStepIndex = 0;

        public void Next()
        {
            m_StepList[m_CurrentStepIndex].stepObject.SetActive(false);
            m_CurrentStepIndex = (m_CurrentStepIndex + 1) % m_StepList.Count;
            m_StepList[m_CurrentStepIndex].stepObject.SetActive(true);
        }
        public void Back()
        {
            m_StepList[m_CurrentStepIndex].stepObject.SetActive(false);
            m_CurrentStepIndex = (m_CurrentStepIndex - 1) % m_StepList.Count;
            m_StepList[m_CurrentStepIndex].stepObject.SetActive(true);
        }
        public void Scenes(int numberScenes)
        {
            lvl1.GetComponent<Button>().interactable = true;
            webManager.work = new WorkData();
            webManager.work.Lab_Id = m_StepList[m_CurrentStepIndex].id;
            webManager.work.Student_Id = webManager.data.Student_Id;
            UnityEngine.SceneManagement.SceneManager.LoadScene(numberScenes);
        }
        public void CardGenerator(List<LabData> list)
        {
            int i = 0;
            foreach (var page in list)
            {
                BlankName.GetComponentInChildren<TMP_Text>().SetText(page.Lab_Name);
                BlankStep.GetComponent<TMP_Text>().SetText("Количество шагов: " + page.Steps);
                BlankDesk.GetComponent<TMP_Text>().SetText(page.Description);
                GameObject labPage = Instantiate(Blank, this.transform);
                m_StepList.Add(new Step(labPage, page.Lab_Id));
                labPage.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(delegate { Scenes(i); });
                if (list.FindIndex(x => x == page) != (list.Count - 1))
                    labPage.transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(Next);
                else
                    labPage.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
                labPage.transform.GetChild(0).transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(Back);
                i++;
            }
        }
    }
}