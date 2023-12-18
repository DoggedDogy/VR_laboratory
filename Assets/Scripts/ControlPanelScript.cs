using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanelScript : MonoBehaviour
{
    [SerializeField] TextMeshPro temperatureText;
    [SerializeField] TextMeshPro timeText;
    private int temperature = 40;
    private int time = 10;
    private float preassure = 0;
    [SerializeField] GameObject preasureArrow;
    [SerializeField] Toggle toChange;
    [SerializeField] Light greenLight;
    [SerializeField] Light redLight;

    void Start()
    {
        temperatureText.text = "Температура: " + temperature.ToString() + "°c";
        timeText.text = "Осталось: " + time.ToString() + "c";
    }

    // Update is called once per frame
    void Update()
    {
        if (temperature == 80)
        {
            toChange.isOn = true;
        }
        else        {
            toChange.isOn = false;
        }
    }

    public void UpTemperature(int num)
    {
        temperature += num;
        temperatureText.text = "Температура: " + temperature.ToString() + "°c";
    }
    public void StartProcess(float reference)
    {

        StartCoroutine(GoUp(reference));
        StartCoroutine(Timer());
    }
    public void StopProcess(float reference)
    {
        StopAllCoroutines();
        greenLight.enabled = false;
        greenLight.GetComponent<MeshRenderer>().materials[1].DisableKeyword("_EMISSION");
        preassure = 0;
        time = 10;
        timeText.text = "Осталось: " + time.ToString() + "c";
        preasureArrow.GetComponent<Transform>().Rotate(Vector3.up, -27.15F);
    }
    IEnumerator GoUp(float reference)
    {
        greenLight.GetComponent<MeshRenderer>().materials[1].EnableKeyword("_EMISSION");
        while (preassure < reference)
        {
            yield return new WaitForSeconds(0.05F);
            preassure += 0.0625F;
            if (preassure > 0.5F)
            {
                preasureArrow.GetComponent<Transform>().Rotate(Vector3.up, 0.67875F);
            }

        }
    }
    IEnumerator Timer()
    {
        while (time != 0)
        {
            yield return new WaitForSeconds(1);
            time -= 1;
            timeText.text = "Осталось: " + time.ToString() + "c";
        }
        greenLight.enabled = false;
        greenLight.GetComponent<MeshRenderer>().materials[1].DisableKeyword("_EMISSION");
        redLight.enabled = true;
        redLight.GetComponent<MeshRenderer>().materials[1].EnableKeyword("_EMISSION");
    }
}
