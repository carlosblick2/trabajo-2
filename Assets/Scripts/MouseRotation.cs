using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotation : MonoBehaviour
{
    public float rotationSpeed = 5.0f;

    private Vector3 lastMousePosition;
    private Quaternion initialRotation;

    void Start()
    {
        // Store the initial rotation of the GameObject
        initialRotation = transform.localRotation;
    }


    void Update()
    {
        /// Get the current mouse position
        Vector3 currentMousePosition = Input.mousePosition;

        // Calculate the mouse movement since the last frame
        Vector3 mouseDelta = currentMousePosition - lastMousePosition;
        // Vector3 mouseDelta = currentMousePosition;

        // Update the last mouse position for the next frame
        lastMousePosition = currentMousePosition;

        // Calculate local rotation quaternions based on mouse movement
        Quaternion rotationY = Quaternion.AngleAxis(mouseDelta.x * rotationSpeed, Vector3.up);


        // Apply the new local rotation to the GameObject
        transform.localRotation *= rotationY;
    }
}
