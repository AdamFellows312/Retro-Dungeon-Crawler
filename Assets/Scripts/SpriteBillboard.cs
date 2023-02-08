using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
    private Transform cameraTransform;

    private Vector3 cameraDirection;

    private void Start() => cameraTransform = Camera.main.transform;
    
    private void Update()
    {
        // Get the forward of the camera
        cameraDirection = cameraTransform.forward;
        // Reset the vectors y value
        cameraDirection.y = 0;

        // Lock towards the camera
        transform.rotation = Quaternion.LookRotation(cameraDirection);
    }
}
