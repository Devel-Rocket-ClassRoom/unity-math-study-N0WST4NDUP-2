using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 3f;
    [Range(30f, 120f)] public float RotateSpeed = 30f;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        var vertical = _playerInput.Vertical;
        var horizontal = _playerInput.Horizontal;
        var leftRotate = _playerInput.LeftRotate;
        var rightRotate = _playerInput.RightRotate;

        var moveDistance = MoveSpeed * Time.deltaTime;
        transform.position += transform.forward * vertical * moveDistance + transform.right * horizontal * moveDistance;

        if (leftRotate)
        {
            float angle = RotateSpeed * Time.deltaTime;
            transform.rotation *= Quaternion.Euler(0f, -angle, 0f);
        }

        if (rightRotate)
        {
            float angle = RotateSpeed * Time.deltaTime;
            transform.rotation *= Quaternion.Euler(0f, angle, 0f);
        }

    }
}
