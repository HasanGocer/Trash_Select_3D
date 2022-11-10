using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoSingleton<UpgradeManager>
{
    [System.Serializable]
    public class UpgradeItem
    {
        public GameObject[] _items;
    }
    public UpgradeItem[] _upgradeItem;

    public GameObject ItemSelect(int item, int count)
    {
        return _upgradeItem[item]._items[count];
    }
}
