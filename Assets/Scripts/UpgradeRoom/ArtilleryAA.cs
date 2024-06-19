using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryAA : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UpgradableObject upgradable = GetComponent<UpgradableObject>();
        upgradable.objectName = "Artillery AA";
        upgradable.upgradeNodes = new UpgradeNode[]
        {
            new UpgradeNode { upgradeName = "Engine", positionOffset = new Vector3(1, 0, 0), upgradeInfo = "Reduce Cooldown" },
            new UpgradeNode { upgradeName = "Armor", positionOffset = new Vector3(-1, 0, 0), upgradeInfo = "Increase Durability" },
            // Add more nodes as needed
        };
    }


}
