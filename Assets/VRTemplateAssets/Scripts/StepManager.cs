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

        [Serializable]
        class Step
        {
            [SerializeField] 
            public GameObject stepObject;

            [SerializeField] public string buttonText;
        }

        [SerializeField] public TextMeshProUGUI m_StepButtonTextField;

        [SerializeField] List<Step> m_StepList = new List<Step>();

        int m_CurrentStepIndex = 0;

        public void Next()
        {
            m_StepList[m_CurrentStepIndex].stepObject.SetActive(false);
            m_CurrentStepIndex = (m_CurrentStepIndex + 1) % m_StepList.Count;
            m_StepList[m_CurrentStepIndex].stepObject.SetActive(true);
            m_StepButtonTextField.text = m_StepList[m_CurrentStepIndex].buttonText;
        }

        public void Scenes(int numberScenes)
        {
            if (m_CurrentStepIndex == 3)
            {
                lvl1.GetComponent<Button>().interactable = true;
                UnityEngine.SceneManagement.SceneManager.LoadScene(numberScenes);
            }
        }
    }
}