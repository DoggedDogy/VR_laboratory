using UnityEngine;

public class LowerBodyAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] [Range(0, 1)] private float leftFootPositionWeight;
    [SerializeField] [Range(0, 1)] private float rightFootPositionWeight;

    [SerializeField] [Range(0, 1)] private float leftFootRotationWeight;
    [SerializeField] [Range(0, 1)] private float rightFootRotationWeight;

    [SerializeField] private Vector3 footOffset;

    [SerializeField] private Vector3 raycastLeftOffset;
    [SerializeField] private Vector3 raycastRightOffset;

    RaycastHit ray1;
    
    private void OnAnimatorIK(int layerIndex)
    {
        Vector3 leftFootPosition = animator.GetIKPosition(AvatarIKGoal.LeftFoot);
        Vector3 rightFootPosition = animator.GetIKPosition(AvatarIKGoal.RightFoot); 

        RaycastHit hitLeftFoot;
        RaycastHit hitRightFoot;
        
        bool isLeftFootDown = Physics.Raycast(leftFootPosition + raycastLeftOffset, Vector3.down, out hitLeftFoot);
        bool isRightFootDown = Physics.Raycast(rightFootPosition + raycastRightOffset, Vector3.down, out hitRightFoot);
        ray1 = hitLeftFoot;

        CalculateLeftFoot(isLeftFootDown, hitLeftFoot);
        CalculateRightFoot(isRightFootDown, hitRightFoot);
    }

    void CalculateLeftFoot(bool isLeftFootDown, RaycastHit hitLeftFoot)
    {
        if (isLeftFootDown)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, leftFootPositionWeight);
            animator.SetIKPosition(AvatarIKGoal.LeftFoot, hitLeftFoot.point + footOffset);

            Quaternion leftFoodRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hitLeftFoot.normal), hitLeftFoot.normal);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, leftFootPositionWeight);
            animator.SetIKPosition(AvatarIKGoal.LeftFoot, hitLeftFoot.point + footOffset);
        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
        }
    }
    void CalculateRightFoot(bool isRightFootDown, RaycastHit hitRightFoot)
    {
        if (isRightFootDown)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightFootPositionWeight);
            animator.SetIKPosition(AvatarIKGoal.RightFoot, hitRightFoot.point + footOffset);

            Quaternion rightFoodRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hitRightFoot.normal), hitRightFoot.normal);
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightFootPositionWeight);
            animator.SetIKPosition(AvatarIKGoal.RightFoot, hitRightFoot.point + footOffset);
        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(ray1.point + footOffset, 0.25f);
    }
}