using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageSystem : MonoSingleton<GarbageSystem>
{
    public int garbagePlaceUSCount;
    public int garbageCarUSCount;
    public int ContractGarbageUSCount;
    public int waitBarUSCount;

    public void GarbagePlacement()
    {
        for (int i = 0; i < ItemData.Instance.field.dirtyGarbage; i++)
        {
            UpgradeManager.Instance._upgradeItem[garbagePlaceUSCount]._items[i].SetActive(true);
        }
        for (int i = 0; i < ItemData.Instance.field.garbageCar; i++)
        {
            UpgradeManager.Instance._upgradeItem[garbageCarUSCount]._items[i].SetActive(true);
        }
    }
}
