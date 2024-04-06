using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drill : MonoBehaviour
{
    [SerializeField] private Collider _collider;
    [SerializeField] private Animator animator;
    [SerializeField] private Image CircleRadian;

    [SerializeField] int drillAngleBase = 180;              // Base angle
    [SerializeField] int drillAngleDelta = 15;              // Angle deviance
    [SerializeField] float drillDistanceThreshold = 0.125f; // Distance between drill head and hole
    [SerializeField] float timeToDrill = 5.0f;              // Time to drill the hole in seconds
    bool isOn = false;

    public void ToggleOnDrill()
    {
        isOn = true;
        _collider.enabled = true;
        animator.SetBool("isDrilling", true);
    }
    public void ToggleOffDrill()
    {
        isOn = false;
        _collider.enabled = false;
        animator.SetBool("isDrilling", false);
    }

    List<Collider> listOfKnownHoles = new List<Collider>();
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Hole"))
        {
            //Destroy(collision.gameObject);
            if (!listOfKnownHoles.Contains(collision))
            {
                Debug.Log(string.Format("Found hole '{0}'", collision.name));
                StartCoroutine(DrillingAction(collision));
                listOfKnownHoles.Add(collision);
            }
        }
    }
    IEnumerator DrillingAction(Collider collision)
    {
        float progress = 0.0f;
        CircleRadian.fillAmount = progress;

        // Do stuff WHILE drilling
        while (progress < 1.00f && collision != null)
        {
            float angle = Mathf.Acos(Vector3.Dot(collision.transform.up, _collider.transform.forward)) * Mathf.Rad2Deg;
            float distance = Vector3.Distance(collision.transform.position, _collider.transform.position);

            if (angle >= (drillAngleBase - drillAngleDelta) &&
                angle <= drillAngleBase &&
                distance <= drillDistanceThreshold &&
                isOn)
            {
                progress += (Time.deltaTime) * (1 / timeToDrill);
                CircleRadian.fillAmount = progress;
            }

            //Debug.Log(string.Format("Distance: {0}, Angle: {1}, Progress: {2}", distance, angle, progress));
            yield return null;
        }


        // Do stuff AFTER drilling
        listOfKnownHoles.Remove(collision);
        if (collision.gameObject != null)
            Destroy(collision.gameObject);
        collision = null;
        progress = 0.0f;
        CircleRadian.fillAmount = 0;
        Debug.Log("Finished drilling hole. Removing hole.");
    }
}
