using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectDetection : MonoBehaviour
{
    public Camera playerCamera;
    public LayerMask interactableLayer; //
    public TextMeshProUGUI interactText;
    public GameObject upgradeNodePreFab;
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
        if (currentObject != null && Input.GetKeyDown(KeyCode.E))
        {
            InteractWithObject(currentObject);
        }

        foreach (GameObject node in activeNodes)
        {
            RectTransform nodeRectTransform = node.GetComponent<RectTransform>();
            Vector2 localMousePosition = nodeRectTransform.InverseTransformPoint(Input.mousePosition);
            if (nodeRectTransform.rect.Contains(localMousePosition))
            {
                nodeRectTransform.localPosition = Vector3.one;
                node.GetComponentInChildren<TextMeshProUGUI>().enabled = true;

            }
            else
            {
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
        obj.transform.localPosition = new Vector3(0, 0, 2); // I may change this

        ShowUpgradeNodes(obj);
    }
    void ShowUpgradeNodes(GameObject obj)
    {
        ClearActiveNodes();
        //Defines Position
        Vector3[] nodePositions = { new Vector3(1, 0, 0), new Vector3(-1, 0, 0), new Vector3(0, 1, 0), new Vector3(0, -1, 0) };

        foreach (Vector3 pos in nodePositions)
        {
            GameObject node = Instantiate(upgradeNodePreFab, obj.transform.position + pos, Quaternion.identity, obj.transform);
            activeNodes.Add(node);
            node.GetComponentInChildren<TextMeshProUGUI>().text = "Upgrade Info";
            node.GetComponentInChildren<TextMeshProUGUI>().enabled = false;

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
}
