using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform lookAt;
    private Vector3 startOffset;
    private Vector3 moveVector;

    private float transition = 0.0f;
    private float animationDuration = 3.0f;
    private Vector3 animationOffset = new Vector3(0, 5, 5);

    void Start()
    {
        lookAt = GameObject.FindGameObjectWithTag("Player").transform;
        //lookAt = GetComponent<PlayerController>().transform;
        startOffset = transform.position - lookAt.position;
    }

    void Update()
    {
        moveVector = lookAt.position + startOffset;
        moveVector.y = 0;
        moveVector.y = Mathf.Clamp(moveVector.y, 6, 12);
        if (transition > 1.0f)
            transform.position = lookAt.position + startOffset;
        else
            transform.position = Vector3.Lerp(moveVector + animationOffset, moveVector, transition);
        transition += Time.deltaTime * 1 / animationDuration;
        transform.LookAt(lookAt.position + Vector3.up);
    }
}