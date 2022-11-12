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
        public bool ContractBool = false;
    }

    public int contractLimit = 1;
    public Contract[] FocusContract;

    private void Start()
    {
        FocusContract = new Contract[contractLimit];
        Buttons.Instance.Contract();
        WaitSysytemCountPlacement(0, 0);
        StartCoroutine(RocketManager.Instance.RocketStart(1));
    }

    public Contract NewContractForUI(int levelMod, int maxItemInCount, int maxItemCount, int contractBudget)
    {
        Contract contract = new Contract();
        contract.money = contractBudget;

        for (int i = 0; i < GameManager.Instance.level / levelMod; i++)
        {
            int itemCount = Random.Range(0, maxItemCount);
            int itemInCount = Random.Range(1, maxItemInCount);

            contract.objectTypeCount.Add(itemCount);
            contract.objectCount.Add(itemInCount);
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

    public int PlayerPrefsContract(int contractCount, int itemCount)
    {
        contractCount++;
        FocusContract[0].objectTypeCount.Add(contractCount);
        FocusContract[0].objectTypeCount.Add(itemCount);
        return contractCount;
    }

    public void ObjectCountUpdate()
    {
            Debug.Log(FocusContract[0].ContractBool);
        RocketManager.Instance.openObjectTypeCount.Clear();
        for (int i1 = 0; i1 < FocusContract.Length; i1++)
        {
            if (FocusContract[i1].ContractBool)
            {
                for (int i2 = 0; i2 < FocusContract[i1].objectTypeCount.Count; i2++)
                {
                    bool isFull = false;
                    for (int i3 = 0; i3 < RocketManager.Instance.openObjectTypeCount.Count; i3++)
                    {
                        if (RocketManager.Instance.openObjectTypeCount[i3] == FocusContract[i1].objectTypeCount[i2])
                            isFull = true;
                    }
                    if (!isFull)
                        RocketManager.Instance.openObjectTypeCount.Add(FocusContract[i1].objectTypeCount[i2]);
                }
            }
        }
        if (RocketManager.Instance.openObjectTypeCount.Count == 0)
            GameManager.Instance.openContract = false;
    }

    public void WaitSysytemCountPlacement(int waitBar, int contractCount)
    {
        GameObject obj = UpgradeManager.Instance.ItemSelect(waitBar, contractCount);
        WaitSystem waitSystem = obj.GetComponent<WaitSystem>();
        waitSystem.placeCount = new int[FocusContract[contractCount].objectTypeCount.Count];

        for (int i = 0; i < FocusContract[contractCount].objectTypeCount.Count; i++)
        {
            waitSystem.placeCount[i] = FocusContract[contractCount].objectTypeCount[i];
        }
    }

    public void ContractDownÝtem(int contractCount, int objectTypeCount, int forCount, bool isStack)
    {
        Debug.Log("1");
        for (int i = 0; i < FocusContract[contractCount].objectTypeCount.Count; i++)
        {
            Debug.Log("2");
            if (FocusContract[contractCount].objectTypeCount[i] == objectTypeCount)
            {
                Debug.Log("3");
                FocusContract[contractCount].objectCount[i]--;
                if (isStack)
                {
                    StackSystem.Instance.ObjectsCount.RemoveAt(forCount);
                    StackSystem.Instance.Objects.RemoveAt(forCount);
                }
                Debug.Log("4");

                Debug.Log("5");
                if (FocusContract[contractCount].objectCount[i] <= 0)
                {
                    Debug.Log("6");
                    FocusContract[contractCount].objectTypeCount.RemoveAt(i);
                    FocusContract[contractCount].objectCount.RemoveAt(i);
                    Debug.Log("7");
                }

                Debug.Log("8");
                if (FocusContract[contractCount].objectTypeCount.Count == 0)
                {
                    Debug.Log("9");
                    ContractCompleted(FocusContract[contractCount], i);
                    Debug.Log("10");
                }
                Debug.Log("11");
                ObjectCountUpdate();
                Debug.Log("12");
            }
        }
    }


    public void ContractCompleted(Contract contract, int contractCount)
    {
        GameManager.Instance.SetMoney(contract.money);
        contract.objectCount.Clear();
        contract.objectTypeCount.Clear();
        contract.money = 0;
        FocusContract[contractCount].ContractBool = false;
        //yeni kontrat UI
        ObjectCountUpdate();
    }
}
