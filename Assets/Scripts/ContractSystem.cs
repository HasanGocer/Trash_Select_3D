using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractSystem : MonoSingleton<ContractSystem>
{

    //contract cancel yok kullanlýmýyor
    [System.Serializable]
    public class Contract
    {
        public List<int> objectTypeCount = new List<int>();
        public List<int> objectCount = new List<int>();
        public int money = 0;
        public int XPPlus = 0;
        public bool contractBool = false;
        public bool contractBuy = false;
    }

    [System.Serializable]
    public class ContractArray
    {
        public int contractLimit;
        public List<Contract> Contracts;
    }
    public ContractArray FocusContract;


    public void ContractStart()
    {
        FocusContract.contractLimit = ItemData.Instance.field.garbageCar;
        ContractUISystem.Instance.contract = new Contract[FocusContract.contractLimit];
        Contract contract = NewContractForUI(LevelSystem.Instance.levelMod, GameManager.Instance.level * LevelSystem.Instance.generalXPFactor, ItemData.Instance.field.dirtyGarbage);
        for (int i = 0; i < FocusContract.contractLimit; i++)
        {
            ContractUISystem.Instance.contract[i] = contract;
            ContractUISystem.Instance.contract[i].contractBool = true;
            ContractUISystem.Instance.contract[i].contractBuy = true;
        }
    }
    //True
    public Contract NewContractForUI(int levelMod, int maxItemCount, int maxitemTypeCount)
    {
        Contract contract = new Contract();
        contract.money = Random.Range(1, maxitemTypeCount * LevelSystem.Instance.generalMoneyFactor);
        contract.XPPlus = Random.Range(1, maxitemTypeCount * LevelSystem.Instance.generalXPFactor);

        for (int i = 0; i < GameManager.Instance.level / levelMod; i++)
        {
            int itemTypeCount = Random.Range(0, maxitemTypeCount);
            int itemCount = Random.Range(1, maxItemCount);

            contract.objectTypeCount.Add(itemTypeCount);
            contract.objectCount.Add(itemCount);
        }

        return contract;
    }

    //True
    public void ContractCanceled(Contract contract)
    {
        MoneySystem.Instance.MoneyTextRevork(contract.money * -1);
        contract.objectCount.Clear();
        contract.objectTypeCount.Clear();
        contract.money = 0;
        contract.XPPlus = 0;
        //yeni kontrat UI
        ObjectCountUpdate();
    }

    /* Kullanýlmýyor
     public void NewContractSelected()
    {
        //object managerdekiler silinecek

        for (int i1 = 0; i1 < ObjectManager.Instance.objectÝnGame.Count; i1++)
        {
            for (int i = 0; i < RocketManager.Instance.openObjectTypeCount.Count; i++)
            {
                if (RocketManager.Instance.openObjectTypeCount[i] == i1)
                {
                    for (int i2 = 0; i2 < ObjectManager.Instance.objectÝnGame[i1].gameObjectÝnGame.Count; i2++)
                    {
                        GameObject obj = ObjectManager.Instance.objectÝnGame[i1].gameObjectÝnGame[i2];
                        obj.GetComponent<ObjectTouchPlane>().AddedObjectPool(i1);
                        RocketManager.Instance.AddedObjectPool(obj);
                    }
                }
            }

            int placeCount = 0;
            for (int i = 0; i < StackSystem.Instance.Objects.Count; i++)
            {
                GameObject obj = StackSystem.Instance.Objects[placeCount];
                if (StackSystem.Instance.ObjectsCount[i] == i1)
                {
                    StackSystem.Instance.ObjectsCount.RemoveAt(placeCount);
                    StackSystem.Instance.Objects.RemoveAt(placeCount);
                    placeCount++;
                }
                StartCoroutine(StackSystem.Instance.ObjectDistancePlacement(obj, placeCount, StackSystem.Instance.stackDistance));
            }
        }

        ObjectCountUpdate();
    }*/

    public void ObjectCountUpdate()
    {
        RocketManager.Instance.openObjectTypeCount.Clear();
        RocketManager.Instance.openObjectCount.Clear();
        RocketManager.Instance.openObjectTypeBool.Clear();

        
        for (int i1 = 0; i1 < FocusContract.Contracts.Count; i1++)
        {
            if (FocusContract.Contracts[i1].contractBool)
            {
                for (int i2 = 0; i2 < FocusContract.Contracts[i1].objectTypeCount.Count; i2++)
                {
                    CheckObject(FocusContract.Contracts[i1].objectTypeCount[i2], FocusContract.Contracts[i1].objectCount[i2]);
                }
            }
        }
        if (RocketManager.Instance.openObjectTypeCount.Count == 0)
            GameManager.Instance.inStart = false;
    }

    public void CheckObject(int objectType, int objectCount)
    {
        RocketManager.Instance.DeleteListsAll();
        bool isThere = false;
        for (int i = 0; i < RocketManager.Instance.openObjectTypeCount.Count; i++)
        {
            if (objectType == RocketManager.Instance.openObjectTypeCount[i])
            {
                RocketManager.Instance.openObjectCount[i] += objectCount;
                isThere = true;
            }

        }
        if (!isThere)
        {
            GameManager.Instance.inStart = true;
            RocketManager.Instance.openObjectTypeCount.Add(objectType);
            RocketManager.Instance.openObjectCount.Add(objectCount);
            RocketManager.Instance.openObjectTypeBool.Add(false);
        }
        RocketManager.Instance.openObjectTypeCount.Sort()
        DirtyManager.Instance.AllListDelete();
        DirtyManager.Instance.NewDirtyListPlacement();
    }

    public void WaitSystemCountPlacement(int waitBar, int contractCount)
    {
        GameObject obj = UpgradeManager.Instance.ItemSelect(waitBar, contractCount);
        WaitSystem waitSystem = obj.GetComponent<WaitSystem>();
        Contract contract = CallContract(contractCount);
        waitSystem.placeCount = new int[contract.objectTypeCount.Count];
        for (int i = 0; i < contract.objectTypeCount.Count; i++)
        {
            waitSystem.placeCount[i] = contract.objectTypeCount[i];
        }
    }

    public void ContractDownÝtem(int contractCount, int objectTypeCount, int forCount, bool isStack)
    {
        Contract contract = CallContract(contractCount);
        for (int i = 0; i < contract.objectTypeCount.Count; i++)
        {
            if (contract.objectTypeCount[i] == objectTypeCount)
            {
                contract.objectCount[i]--;
                DeleteFirstPlaceList(contractCount, objectTypeCount);
                if (isStack)
                {
                    StackSystem.Instance.ObjectsCount.RemoveAt(forCount);
                    StackSystem.Instance.Objects.RemoveAt(forCount);
                }

                if (contract.objectCount[i] <= 0)
                {

                    contract.objectTypeCount.RemoveAt(i);
                    contract.objectCount.RemoveAt(i);
                }

                if (contract.objectTypeCount.Count == 0)
                {
                    ContractCompleted(contract, i);
                }
                ObjectCountUpdate();
                GameManager.Instance.ContractPlacementWrite(FocusContract);
            }
        }
    }
    private Contract CallContract(int contractCount)
    {
        return FocusContract.Contracts[contractCount];
    }

    public void DeleteFirstPlaceList(int contractCount, int objectTypeCount)
    {
        FirstSpawn firstSpawn = UpgradeManager.Instance._upgradeItem[GarbageSystem.Instance.garbagePlaceUSCount]._items[FocusContract.Contracts[contractCount].objectTypeCount[objectTypeCount]].GetComponent<FirstSpawn>();
        firstSpawn.Objects.RemoveAt(firstSpawn.Objects.Count - 1);
        firstSpawn.ObjectsBool.RemoveAt(firstSpawn.ObjectsBool.Count - 1);
    }


    public void ContractCompleted(Contract contract, int contractCount)
    {
        MoneySystem.Instance.MoneyTextRevork(contract.money);
        LevelSystem.Instance.BarLerp(contract.XPPlus);
        contract.objectCount.Clear();
        contract.objectTypeCount.Clear();
        contract.money = 0;
        contract.XPPlus = 0;
        FocusContract.Contracts[contractCount].contractBool = false;
        StartCoroutine(UpgradeManager.Instance._upgradeItem[GarbageSystem.Instance.garbageCarUSCount]._items[contractCount].GetComponent<GarbageCarMove>().GarbageCarMoveFunc());
        ObjectCountUpdate();
    }
}
