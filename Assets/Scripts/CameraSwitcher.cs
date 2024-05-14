using System.Collections;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera mainCamera;
    public Camera artilleryCamera;
    public float transitionDuration = 1.0f;

    private Camera currentCamera;
    private bool isTransitioning = false;

    // Variables to store initial positions and rotations
    private Vector3 mainCameraInitialPosition;
    private Quaternion mainCameraInitialRotation;
    private Vector3 artilleryCameraInitialPosition;
    private Quaternion artilleryCameraInitialRotation;

    void Start()
    {
        mainCamera.enabled = true;
        artilleryCamera.enabled = false;
        mainCamera.depth = 1;
        artilleryCamera.depth = 0;
        currentCamera = mainCamera;
        Debug.Log("Starting with Main Camera");

        // Store initial positions and rotations
        mainCameraInitialPosition = mainCamera.transform.position;
        mainCameraInitialRotation = mainCamera.transform.rotation;
        artilleryCameraInitialPosition = artilleryCamera.transform.position;
        artilleryCameraInitialRotation = artilleryCamera.transform.rotation;
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && !isTransitioning && currentCamera == mainCamera)
        {
            Debug.Log("Switching Camera...");
            StartCoroutine(SwitchCamera(artilleryCamera));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && !isTransitioning && currentCamera == artilleryCamera)
        {
            Debug.Log("Switching to Main Camera...");
            StartCoroutine(SwitchCamera(mainCamera));
        }
    }

    IEnumerator SwitchCamera(Camera targetCamera)
    {
        isTransitioning = true;

        Vector3 initialPosition = currentCamera.transform.position;
        Quaternion initialRotation = currentCamera.transform.rotation;
        Vector3 targetPosition = targetCamera.transform.position;
        Quaternion targetRotation = targetCamera.transform.rotation;

        float elapsedTime = 0.0f;

        targetCamera.enabled = true;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / transitionDuration);

            currentCamera.transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            currentCamera.transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, t);

            yield return null;
        }

        currentCamera.transform.position = targetPosition;
        currentCamera.transform.rotation = targetRotation;


        currentCamera.enabled = false;
        currentCamera.depth = 0;
        targetCamera.depth = 1;

        currentCamera = targetCamera;
        Debug.Log("Switched to " + currentCamera.name);

        // Reset cameras to their initial positions and rotations
        mainCamera.transform.position = mainCameraInitialPosition;
        mainCamera.transform.rotation = mainCameraInitialRotation;
        artilleryCamera.transform.position = artilleryCameraInitialPosition;
        artilleryCamera.transform.rotation = artilleryCameraInitialRotation;

        isTransitioning = false;
    }
}
