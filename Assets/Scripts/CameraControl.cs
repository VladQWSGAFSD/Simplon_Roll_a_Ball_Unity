using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    public Transform target;
    public float distance = 5f;
    public float sensitivity = 2f;

    private float currentX = 0f;
    private float currentY = 0f;

    public void OnMove(InputValue value)
    {
        Vector2 moveVector = value.Get<Vector2>();
        currentX += moveVector.x * sensitivity;
        currentY -= moveVector.y * sensitivity;
        currentY = Mathf.Clamp(currentY, -90f, 90f);
    }

    private void LateUpdate()
    {
        Vector3 direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, -90f);
        Vector3 position = rotation * direction + target.position;

        transform.rotation = rotation;
        transform.position = position;
    }
}
