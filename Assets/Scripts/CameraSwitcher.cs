using System.Collections;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera mainCamera;
    public Camera artilleryCamera;
    public float transitionDuration = 1.0f; 

    private Camera currentCamera;
    private bool isTransitioning = false;

    void Start()
    {
        mainCamera.enabled = true;
        artilleryCamera.enabled = true;
        currentCamera = mainCamera;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && !isTransitioning)
        {
            // Start the transition coroutine
            StartCoroutine(SwitchCamera());
        }
    }

    IEnumerator SwitchCamera()
    {
        isTransitioning = true;

        Camera targetCamera = (currentCamera == mainCamera) ? artilleryCamera : mainCamera;
        Vector3 initialPosition = currentCamera.transform.position;
        Quaternion initialRotation = currentCamera.transform.rotation;
        Vector3 targetPosition = targetCamera.transform.position;
        Quaternion targetRotation = targetCamera.transform.rotation;

        float elapsedTime = 0.0f;

        targetCamera.enabled = true;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionDuration;

            // Lerp the position and rotation of the current camera
            currentCamera.transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            currentCamera.transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, t);

            yield return null;
        }

        // Make sure that the final position and rotation are set correctly
        currentCamera.transform.position = targetPosition;
        currentCamera.transform.rotation = targetRotation;

        currentCamera.enabled = false;
        currentCamera = targetCamera;
        isTransitioning = false;
    }
}
