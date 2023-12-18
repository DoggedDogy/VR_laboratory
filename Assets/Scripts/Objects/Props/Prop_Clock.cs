using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ent_Clock : MonoBehaviour
{
    //[ExecuteAlways]
    [SerializeField] GameObject armSecond;
    [SerializeField] GameObject armMinute;
    [SerializeField] GameObject armHour;

    int timeSecond;
    int timeMinute;
    int timeHour;

    // Start is called before the first frame update
    void Awake()
    {
        timeHour = DateTime.Now.Hour;
        timeMinute = DateTime.Now.Minute;
        timeSecond = DateTime.Now.Second;

        armHour.transform.Rotate(0, 0, (timeHour % 12) * 30);
        armMinute.transform.Rotate(0, 0.0f, timeMinute * 6);
        armSecond.transform.Rotate(0, 0, timeSecond * 6);

    }

    // Update is called once per frame
    void Update()
    {
        GetTime();
        SetTime();
        //Debug.Log(string.Format("Current Time: {0}:{1}:{2}.", timeHour, timeMinute, timeHour));
    }

    void GetTime()
    {
        timeHour = DateTime.Now.Hour;
        timeMinute = DateTime.Now.Minute;
        timeSecond = DateTime.Now.Second;
    }
    void SetTime()
    {
        armHour.transform.localEulerAngles = new Vector3(0, 0, (timeHour % 12) * 30 + timeMinute/2);
        armMinute.transform.localEulerAngles = new Vector3(0, 0, timeMinute * 6);
        armSecond.transform.localEulerAngles = new Vector3(0, 0, timeSecond * 6);
    }
}
