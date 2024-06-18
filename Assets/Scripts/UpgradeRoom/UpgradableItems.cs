using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradableItem")]
public class UpgradableItems : ScriptableObject
{
    public string itemName;

    [System.Serializable]
    public class UpgradeLevel
    {
        public int level;
        public int cost;
        public float powerIncrease;
        public float cooldownDecrease;
    }

    public List<UpgradeLevel> levels;
}
