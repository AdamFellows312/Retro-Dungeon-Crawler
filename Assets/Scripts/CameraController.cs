using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;

    private Vector3 velocity = Vector3.zero;

    [SerializeField, Range(0.0f, 1.0f)] private float smoothTime = 0.3f;
    [Space(10)]

    [SerializeField] private Vector3 targetFollowOffset; 

    private void Start()
    {
        // Get the inital target for the camera to follow
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        CameraFollow();
    }

    private void CameraFollow()
    {
        // Define a target position above and behind the target transform
        Vector3 targetPosition = target.position + targetFollowOffset;

        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
