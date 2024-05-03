using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float edgeThreshold = 10f;
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] float zoomSensitivity = 5f;
    [SerializeField] float minFOV = 15f;
    [SerializeField] float maxFOV = 90f;

    void Start()
    {

    }

    void Update()
    {
        Vector3 rotationDirection = Vector3.zero;

        if (Input.mousePosition.x <= edgeThreshold)
        {
            rotationDirection.y -= rotationSpeed;
        }

        else if (Input.mousePosition.x >= Screen.width - edgeThreshold)
        {
            rotationDirection.y += rotationSpeed;
        }

        if (Input.mousePosition.y <= edgeThreshold)
        {
            rotationDirection.x += rotationSpeed;
        }

        else if (Input.mousePosition.y >= Screen.height - edgeThreshold)
        {
            rotationDirection.x -= rotationSpeed;
        }

        transform.eulerAngles += rotationDirection * Time.deltaTime;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        cam.fieldOfView -= scroll * zoomSensitivity;
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minFOV, maxFOV);


    }
}
