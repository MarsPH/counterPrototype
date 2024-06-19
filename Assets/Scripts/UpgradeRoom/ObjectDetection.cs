using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectDetection : MonoBehaviour
{
    public Camera playerCamera;
    public LayerMask interactableLayer; // Layer for interactable objects
    public TextMeshProUGUI interactText; // TextMeshPro for interaction text
    public GameObject upgradeNodePrefab; // Prefab for the upgrade node
    private GameObject currentObject;
    private List<GameObject> activeNodes = new List<GameObject>();

    void Update()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactableLayer))
        {
            currentObject = hit.collider.gameObject;
            ShowInteractUI(currentObject.name);
        }
        else
        {
            currentObject = null;
            HideInteractUI();
        }

        if (currentObject != null && Input.GetKeyDown(KeyCode.E)) // Assuming "E" is the interact key
        {
            InteractWithObject(currentObject);
        }

        // Handle mouse hover over upgrade nodes
        foreach (GameObject node in activeNodes)
        {
            RectTransform nodeRectTransform = node.GetComponent<RectTransform>();
            Vector2 localMousePosition = nodeRectTransform.InverseTransformPoint(Input.mousePosition);
            if (nodeRectTransform.rect.Contains(localMousePosition))
            {
                // Enlarge and show upgrade information
                nodeRectTransform.localScale = Vector3.one * 1.2f;
                node.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
            }
            else
            {
                // Reset scale and hide upgrade information
                nodeRectTransform.localScale = Vector3.one;
                node.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            }
        }
    }

    void ShowInteractUI(string objectName)
    {
        interactText.text = $"Press E to interact with {objectName}";
        interactText.enabled = true;
    }

    void HideInteractUI()
    {
        interactText.enabled = false;
    }

    void InteractWithObject(GameObject obj)
    {
        obj.transform.SetParent(playerCamera.transform);
        obj.transform.localPosition = new Vector3(0, 0, 2); // Adjust as needed

        ShowUpgradeNodes(obj);
    }

    void ShowUpgradeNodes(GameObject obj)
    {
        ClearActiveNodes();

        UpgradableObject upgradableObject = obj.GetComponent<UpgradableObject>();
        if (upgradableObject == null)
            return;

        foreach (UpgradeNode node in upgradableObject.upgradeNodes)
        {
            GameObject nodeObj = Instantiate(upgradeNodePrefab, obj.transform.position + node.positionOffset, Quaternion.identity, obj.transform);
            activeNodes.Add(nodeObj);
            nodeObj.GetComponentInChildren<TextMeshProUGUI>().text = node.upgradeInfo;
            nodeObj.GetComponentInChildren<TextMeshProUGUI>().enabled = false; // Initially hide the text
        }
    }

    void ClearActiveNodes()
    {
        foreach (GameObject node in activeNodes)
        {
            Destroy(node);
        }
        activeNodes.Clear();
    }
}
