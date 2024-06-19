using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterceptionMissile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UpgradableObject upgradable = GetComponent<UpgradableObject>();
        upgradable.objectName = "Interception Missile";
        upgradable.upgradeNodes = new UpgradeNode[]
        {
            new UpgradeNode { upgradeName = "Warhead", positionOffset = new Vector3(0, 1, 0), upgradeInfo = "Increase Damage" },
            new UpgradeNode { upgradeName = "Guidance", positionOffset = new Vector3(0, -1, 0), upgradeInfo = "Improve Accuracy" },
            // Add more nodes as needed
        };
    }

}
