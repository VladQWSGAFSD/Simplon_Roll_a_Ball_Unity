using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float followSpeed = 5f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float distance = 10f;

    private Vector2 moveInput;

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector3 targetPos = playerTransform.position - transform.forward * distance;

        // Apply movement based on input
        Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y).normalized;
        if (moveDirection.magnitude > 0f)
        {
            targetPos += moveDirection * moveSpeed * Time.deltaTime;
        }

        // Smoothly move the camera towards the target position
        Vector3 smoothPos = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
        transform.position = smoothPos;
    }
}
