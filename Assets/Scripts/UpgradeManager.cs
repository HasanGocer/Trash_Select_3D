using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [System.Serializable]
    private class UpgradeItem
    {
        public GameObject[] _items;
    }
    private UpgradeItem[] _upgradeItem;

    public GameObject ItemSelect(int item, int count)
    {
        return _upgradeItem[item]._items[count];
    }
}
