using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform Target;
    public Vector3 Offset = new(0f, 3f, -5f);
    public float RotationSmoothSpeed = 5f;

    private Vector3 _zoomVelocity = Vector3.zero;

    private void LateUpdate()
    {
        Vector3 desiredPos = Target.position + Target.forward * Offset.z + Vector3.up * Offset.y;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPos,
            ref _zoomVelocity,
            0.1f
            );

        Quaternion desiredRotation = Quaternion.LookRotation(Target.position - transform.position);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            desiredRotation,
            RotationSmoothSpeed * Time.deltaTime
            );
    }
}
