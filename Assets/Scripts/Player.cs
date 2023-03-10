using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private float movementX, movementY;
    bool grounded = false;
    
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float speed = 20.0f;

    #region Events delegate
    #endregion
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ContinuousJump();
    }

    void ContinuousJump()
    {
        if (IsGrounded())
        {
            Vector3 jumpVelocity = _rigidbody.velocity;
            jumpVelocity.y = jumpForce;
            _rigidbody.velocity = jumpVelocity;
        }
    }
    bool IsGrounded()
    {
        return grounded;
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Target"))
            grounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
            grounded = false;
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;

    }
    void OnJump()
    {
        Debug.Log("jump");
    }
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0f, movementY);
        _rigidbody.AddForce(movement * speed);
    }


}