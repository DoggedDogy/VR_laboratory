using UnityEngine;

[System.Serializable]
public class MapTransform
{
    public Transform vrTarget;
    public Transform IKTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public void MapVRAvatar()
    {
        IKTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        IKTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset); // 
    }
}
public class AvatarController : MonoBehaviour
{
    [SerializeField] private GameObject pelvis;
    [SerializeField] private MapTransform head;
    [SerializeField] private MapTransform leftHand;
    [SerializeField] private MapTransform rightHand;

    [SerializeField] private float turnSmoothness;

    [SerializeField] private Transform IKHead;

    [SerializeField] private Vector3 headBodyOffset;

    void LateUpdate()
    {
        Vector3 noY = Vector3.Cross(rightHand.vrTarget.position - pelvis.transform.position, leftHand.vrTarget.position - pelvis.transform.position).normalized;
        noY.y = 0f;
        pelvis.transform.forward = noY;
        Debug.DrawLine(pelvis.transform.position, pelvis.transform.position + pelvis.transform.forward);


        transform.position = IKHead.position + headBodyOffset;
        transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(IKHead.forward, Vector3.up).normalized, Time.deltaTime * turnSmoothness); ;
        head.MapVRAvatar();
        leftHand.MapVRAvatar();
        rightHand.MapVRAvatar();
    }
}