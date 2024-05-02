using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    [SerializeField] float edgeThreshold = 10f;
    [SerializeField] float rotationSpeed = 5f;
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

    }
}
