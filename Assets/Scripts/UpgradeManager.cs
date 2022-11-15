using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoSingleton<UpgradeManager>
{
    [System.Serializable]
    public class UpgradeItem
    {
        public List<GameObject> _items = new List<GameObject>();
    }
    public List<UpgradeItem> _upgradeItem = new List<UpgradeItem>();

    public void UpgradeSystemStart()
    {
        StartFirstSpawn();
    }

    public GameObject ItemSelect(int item, int count)
    {
        return _upgradeItem[item]._items[count];
    }
    public void StartFirstSpawn()
    {
        for (int i = 0; i < _upgradeItem[2]._items.Count; i++)
        {
            StartCoroutine(_upgradeItem[2]._items[i].GetComponent<FirstSpawn>().ItemSpawn());
        }
    }
}
