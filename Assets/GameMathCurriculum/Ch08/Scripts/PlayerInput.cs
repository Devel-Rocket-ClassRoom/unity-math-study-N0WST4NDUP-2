using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public readonly string VerticalAxis = "Vertical";
    public readonly string HorizontalAxis = "Horizontal";
    public readonly string LeftRotateButton = "Fire1";
    public readonly string RightRotateButton = "Fire2";

    public float Vertical { get; private set; }
    public float Horizontal { get; private set; }
    public bool LeftRotate { get; private set; }
    public bool RightRotate { get; private set; }

    private void Update()
    {
        Vertical = Input.GetAxis(VerticalAxis);
        Horizontal = Input.GetAxis(HorizontalAxis);
        LeftRotate = Input.GetButton(LeftRotateButton);
        RightRotate = Input.GetButton(RightRotateButton);
    }
}
