using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoldedTogle : MonoBehaviour
{
    [SerializeField] private Toggle previousToggle;
    [SerializeField] private Toggle thisToggle;
    public void Update()
    {
        if (!previousToggle.isOn && !thisToggle.isOn)
            this.gameObject.SetActive(false);
        //while (!thisToggle.isOn)
        //{

        //    CheckToggles();
        //}
    }
    private void CheckToggles()
    {
        foreach (Toggle child in this.GetComponentsInChildren<Toggle>())
        {
            if (!child.isOn)
                return;
        }
        thisToggle.isOn = true;
        this.gameObject.SetActive(false);
    }
}
