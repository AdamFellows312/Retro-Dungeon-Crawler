using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;

    private float targetZoom;
    private float zoomSensitivity = 1.0f;
    private float zoomTime = 30.0f;

    private Camera targetCamera;

    private Transform target;

    [SerializeField] private float defaultZoom = 3.5f;
    [SerializeField] private float maxZoom = 2.0f;
    [SerializeField] private float minZoom = 5.0f;
    [Space(10)]

    [SerializeField] private float smoothSpeed = 0.125f;
    [Space(10)]

    [SerializeField] Vector3 offset = new Vector3(0.0f, 0.5f, 0.0f);

    private void Start()
    {
        targetCamera = this.GetComponent<Camera>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        targetZoom = defaultZoom;
    }

    private void Update()
    {
        CameraZoom();
    }

    private void LateUpdate()
    {
        CameraFollow();
    }

    private void CameraZoom()
    {
        // Get the target size based on the mouse scroll
        targetZoom -= Input.mouseScrollDelta.y * zoomSensitivity;
        // Clamp the target zoom between the max and minimum values
        targetZoom = Mathf.Clamp(targetZoom, maxZoom, minZoom);

        // Get the new size of the camera 
        float newOrthoSize = Mathf.MoveTowards(targetCamera.orthographicSize, targetZoom, zoomTime * Time.deltaTime);
        // Apply the new size to the camera
        targetCamera.orthographicSize = newOrthoSize;
    }

    private void CameraFollow()
    {
        // Get and smooth the desired position
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(targetCamera.transform.position, desiredPosition, ref velocity, smoothSpeed);

        // Have the camera move towards the new position
        targetCamera.transform.position = smoothedPosition;
    }
}
