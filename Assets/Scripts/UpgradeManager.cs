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
        for (int i = 0; i < ItemData.Instance.field.dirtyGarbage; i++)
        {
            StartCoroutine(_upgradeItem[GarbageSystem.Instance.garbagePlaceUSCount]._items[i].GetComponent<FirstSpawn>().ItemSpawn());
        }

        for (int i = 0; i < ItemData.Instance.field.garbageCar; i++)
        {
            _upgradeItem[GarbageSystem.Instance.ContractGarbageUSCount]._items[i].SetActive(true);
        }
    }
}
