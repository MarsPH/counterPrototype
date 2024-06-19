using UnityEngine;

public class UpgradableObject : MonoBehaviour
{
    public string objectName;
    public UpgradeNode[] upgradeNodes;
}

[System.Serializable]
public class UpgradeNode
{
    public string upgradeName;
    public Vector3 positionOffset;
    public string upgradeInfo;
}
