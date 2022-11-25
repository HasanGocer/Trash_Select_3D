using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageSystem : MonoSingleton<GarbageSystem>
{
    [SerializeField] private int _garbagePlaceUSCount;
    [SerializeField] private int _garbageCarUSCount;

    public void GarbagePlacement()
    {
        for (int i = 0; i < ItemData.Instance.field.dirtyGarbage; i++)
        {
            UpgradeManager.Instance._upgradeItem[_garbagePlaceUSCount]._items[i].SetActive(true);
        }
        for (int i = 0; i < ItemData.Instance.field.garbageCar; i++)
        {
            UpgradeManager.Instance._upgradeItem[_garbageCarUSCount]._items[i].SetActive(true);
        }
    }
}
