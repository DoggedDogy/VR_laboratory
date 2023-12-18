using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasteController : MonoBehaviour
{

    [SerializeField] GameObject clippingPlane;
    [SerializeField] float clippingPlaneSpeed = 0.0125F;
    [SerializeField] GameObject pasteDispenser;
    [SerializeField] float pasteDispenserTriggerDistance = 0.125f;
    [SerializeField] GameObject pathStart;
    [SerializeField] GameObject pathEnd;
    [SerializeField] Material material_;
    //public bool ready = false;
    //public void ReadyTrue()
    //{
    //    ready = true;
    //}
    //public void ReadyFalse()
    //{
    //    ready = false;
    //}

    //ost
    [SerializeField] GameObject togle;

    Vector3 startValue;
    Vector3 endValue;
    float displacemet;

    void Awake()
    {
        StartCoroutine(Down());
        startValue = pathStart.transform.position;
        endValue = pathEnd.transform.position;
    }

    float pathPercent = 0;
    IEnumerator Down()
    {
        float timeElapsed = 0;

        float distancePlaneToEnd = Vector3.Distance(clippingPlane.transform.position, pathStart.transform.position);
        float distanceStartToEnd = Vector3.Distance(pathStart.transform.position, pathEnd.transform.position);

        float normalisedDistance = distancePlaneToEnd / distanceStartToEnd;

        // While the object is still "moving"
        while (Mathf.Abs(clippingPlane.transform.position.y - endValue.y) > 0.05)
        {
            // Is the paste dispenser close enough to the end point?
            if (Vector3.Distance(clippingPlane.transform.position, pasteDispenser.transform.position) <= pasteDispenserTriggerDistance)
            {
                pathPercent += clippingPlaneSpeed;
                distancePlaneToEnd = Vector3.Distance(clippingPlane.transform.position, pathStart.transform.position);
            }


            // Update position of clipping plane
            clippingPlane.transform.position = Vector3.Lerp(startValue, endValue, pathPercent);

            // Advance the shader param
            material_.SetFloat("_fillPercentage", distancePlaneToEnd / distanceStartToEnd);

            // Debug
            timeElapsed += Time.deltaTime;

            // Wait until next frame
            yield return null;
        }

        //if (distancePlaneToEnd / distanceStartToEnd > 1)
        //{
            clippingPlane.GetComponentInChildren<MeshRenderer>().enabled = false;
            // Snap plane to endpoint
            clippingPlane.transform.position = endValue;
            togle.GetComponent<Toggle>().isOn = true;
        //}

        //Debug.Log("Ended coroutine in " + timeElapsed + " seconds.");
    }
}
