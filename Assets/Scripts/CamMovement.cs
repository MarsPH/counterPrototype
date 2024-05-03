using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float edgeThresholdVertical = 10f;
    [SerializeField] float edgeThresholdHorizontal = 125f;
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

        if (Input.mousePosition.x <= edgeThresholdHorizontal)
        {
            rotationDirection.y -= rotationSpeed;
            Debug.Log("Touching the left edge");
        }

        else if (Input.mousePosition.x >= Screen.width - edgeThresholdHorizontal)
        {
            rotationDirection.y += rotationSpeed;
            Debug.Log("Toucing Right edge");
        }

        if (Input.mousePosition.y <= edgeThresholdVertical)
        {
            rotationDirection.x += rotationSpeed;
        }

        else if (Input.mousePosition.y >= Screen.height - edgeThresholdVertical)
        {
            rotationDirection.x -= rotationSpeed;
        }

        transform.eulerAngles += rotationDirection * Time.deltaTime;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        cam.fieldOfView -= scroll * zoomSensitivity;
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minFOV, maxFOV);


    }
}
