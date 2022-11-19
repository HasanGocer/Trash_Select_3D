using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractSystem : MonoSingleton<ContractSystem>
{
    [System.Serializable]
    public class Contract
    {
        public List<int> objectTypeCount = new List<int>();
        public List<int> objectCount = new List<int>();
        public int money = 0;
        public bool contractBool = false;
        public bool contractBuy = false;
    }

    public class ContractArray
    {
        public List<Contract> Contracts;
    }
    public ContractArray FocusContract;

    public int contractLimit;
    public int levelMod, maxItemCount, maxitemTypeCount, contractBudge;
    public int waitBarUSCount;

    public void ContractStart()
    {
        Contract contract = NewContractForUI(levelMod, maxItemCount, maxitemTypeCount, contractBudge);
        for (int i = 0; i < contractLimit; i++)
        {
            FocusContract.Contracts[i] = contract;
            FocusContract.Contracts[i].contractBool = true;
            FocusContract.Contracts[i].contractBuy = true;
            WaitSysytemCountPlacement(waitBarUSCount, i);
        }
        ObjectCountUpdate();
    }

    public Contract NewContractForUI(int levelMod, int maxItemCount, int maxitemTypeCount, int contractBudget)
    {
        Contract contract = new Contract();
        contract.money = contractBudget;

        for (int i = 0; i < GameManager.Instance.level / levelMod; i++)
        {
            int itemTypeCount = Random.Range(0, maxitemTypeCount);
            int itemCount = Random.Range(1, maxItemCount);

            contract.objectTypeCount.Add(itemTypeCount);
            contract.objectCount.Add(itemCount);
        }

        return contract;
    }

    public void ContractCanceled(Contract contract)
    {
        GameManager.Instance.SetMoney(contract.money * -1);
        contract.objectCount.Clear();
        contract.objectTypeCount.Clear();
        contract.money = 0;
        //yeni kontrat UI
        ObjectCountUpdate();
    }

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
    }

    public void ObjectCountUpdate()
    {
        RocketManager.Instance.openObjectTypeCount.Clear();
        RocketManager.Instance.openObjectCount.Clear();
        RocketManager.Instance.openObjectTypeBool.Clear();

        for (int i1 = 0; i1 < FocusContract.Contracts.Count; i1++)
        {
            if (FocusContract.Contracts[i1].ContractBool)
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
            RocketManager.Instance.openObjectTypeCount.Add(objectType);
            RocketManager.Instance.openObjectCount.Add(objectCount);
            RocketManager.Instance.openObjectTypeBool.Add(false);
        }
    }

    public void WaitSysytemCountPlacement(int waitBar, int contractCount)
    {
        GameObject obj = UpgradeManager.Instance.ItemSelect(waitBar, contractCount);
        WaitSystem waitSystem = obj.GetComponent<WaitSystem>();
        waitSystem.placeCount = new int[FocusContract.Contracts[contractCount].objectTypeCount.Count];

        for (int i = 0; i < FocusContract.Contracts[contractCount].objectTypeCount.Count; i++)
        {
            waitSystem.placeCount[i] = FocusContract.Contracts[contractCount].objectTypeCount[i];
        }
    }

    public void ContractDownÝtem(int contractCount, int objectTypeCount, int forCount, bool isStack)
    {
        for (int i = 0; i < FocusContract.Contracts[contractCount].objectTypeCount.Count; i++)
        {
            if (FocusContract.Contracts[contractCount].objectTypeCount[i] == objectTypeCount)
            {
                FocusContract.Contracts[contractCount].objectCount[i]--;
                if (isStack)
                {
                    StackSystem.Instance.ObjectsCount.RemoveAt(forCount);
                    StackSystem.Instance.Objects.RemoveAt(forCount);
                }

                if (FocusContract.Contracts[contractCount].objectCount[i] <= 0)
                {
                    FocusContract.Contracts[contractCount].objectTypeCount.RemoveAt(i);
                    FocusContract.Contracts[contractCount].objectCount.RemoveAt(i);
                }

                if (FocusContract.Contracts[contractCount].objectTypeCount.Count == 0)
                {
                    ContractCompleted(FocusContract.Contracts[contractCount], i);
                }
                ObjectCountUpdate();
            }
        }
    }


    public void ContractCompleted(Contract contract, int contractCount)
    {
        GameManager.Instance.SetMoney(contract.money);
        contract.objectCount.Clear();
        contract.objectTypeCount.Clear();
        contract.money = 0;
        FocusContract.Contracts[contractCount].ContractBool = false;
        //yeni kontrat UI
        ObjectCountUpdate();
    }
}
