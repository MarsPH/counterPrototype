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
            if (node == null) continue;

            Collider nodeCollider = node.GetComponent<Collider>();
            Ray nodeRay = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit nodeHit;

            if (nodeCollider.Raycast(nodeRay, out nodeHit, Mathf.Infinity))
            {
                node.transform.localScale = Vector3.one * 1.2f;
                node.GetComponentInChildren<Canvas>().gameObject.SetActive(true);
            }
            else
            {
                node.transform.localScale = Vector3.one;
                node.GetComponentInChildren<Canvas>().gameObject.SetActive(false);
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
            nodeObj.GetComponentInChildren<Canvas>().gameObject.SetActive(false); // Initially hide the panel
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
