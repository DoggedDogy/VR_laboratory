using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public ParticleSystem particleSystemLiquid;
    public float fillAmount = 0.8f;
    float m_StartingFillAmount;
    // Start is called before the first frame update
    void Start()
    {
        particleSystemLiquid.Stop();
        m_StartingFillAmount = fillAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Dot(transform.up, Vector3.down) > 0 && fillAmount > 0)
        {
            if (particleSystemLiquid.isStopped)
            {
                particleSystemLiquid.Play();

            }

            fillAmount -= 0.1f * Time.deltaTime;

            float fillRatio = fillAmount / m_StartingFillAmount;


            RaycastHit hit;
            if (Physics.Raycast(particleSystemLiquid.transform.position, Vector3.down, out hit, 50.0f, ~0, QueryTriggerInteraction.Collide))
            {
                PotionReceiver receiver = hit.collider.GetComponent<PotionReceiver>();

            }

        }
        else
        {
            particleSystemLiquid.Stop();

        }

       
    }
}
