using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mug : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject mLiquid;
    public GameObject mLiquidMesh;

    private int mSloshSpeed = 60;
    private int mRotateSpeed = 15;

    private int difference = 25;

  

    // Update is called once per frame
    void Update()
    {
        Slosh();
        mLiquidMesh.transform.Rotate(Vector3.up * mRotateSpeed * Time.deltaTime, Space.Self);
    }
    void Slosh()
    {

    }
}
