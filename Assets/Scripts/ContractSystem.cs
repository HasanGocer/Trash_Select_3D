using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractSystem : MonoSingleton<ContractSystem>
{
    [System.Serializable]
    public class Contract
    {
        public Hashtable itemCount;
        public int money;
    }

    public bool ContractBool;
    public int contractLimit = 3;
    public Contract[] FocusContract;

    private void Start()
    {
        FocusContract = new Contract[contractLimit];
    }

    public Contract NewContractForUI(int levelMod, int minItemInCount, int maxItemInCount, int minItemCount, int maxItemCount, int contractBudget)
    {
        Contract contract = new Contract();
        contract.money = contractBudget;

        for (int i = 0; i < GameManager.Instance.level / levelMod; i++)
        {
            int itemCount = Random.Range(minItemCount, maxItemCount);
            int itemInCount = Random.Range(minItemInCount, maxItemInCount);

            contract.itemCount.Add(itemCount, itemInCount);
        }

        return contract;
    }

    public void ContractCompleted(Contract contract)
    {
        GameManager.Instance.SetMoney(contract.money);
        contract.itemCount.Clear();
        contract.money = 0;
        //yeni kontrat UI
    }

    public void ContractCanceled(Contract contract)
    {
        GameManager.Instance.SetMoney(contract.money * -1);
        contract.itemCount.Clear();
        contract.money = 0;
        //yeni kontrat UI
    }

    public void NewContractSelected()
    {
        //object managerdekiler silinecek
        bool[] intbool = new bool[FocusContract.Length];
        for (int i1 = 0; i1 < FocusContract.Length; i1++)
        {
            for (int i2 = 0; i2 < intbool.Length; i2++)
            {
                if (FocusContract[i1].itemCount.ContainsKey(i2))
                {
                    intbool[i2] = true;
                }
            }
        }
        for (int i1 = 0; i1 < intbool.Length; i1++)
        {
            if (!intbool[i1])
            {
                for (int i2 = 0; i2 < ObjectManager.Instance.object�nGame[i1].gameObject�nGame.Count; i2++)
                {
                    GameObject obj = ObjectManager.Instance.object�nGame[i1].gameObject�nGame[i2];
                    obj.GetComponent<ObjectTouchPlane>().AddedObjectPool(i1);
                    RocketManager.Instance.AddedObjectPool(obj);
                }
            }
            int placeCount = 0;
            for (int i = 0; i < StackSystem.Instance.Objects.Count; i++)
            {
                if (StackSystem.Instance.ObjectsCount[i] == i1)
                {
                    GameObject obj = StackSystem.Instance.Objects[placeCount];
                    StackSystem.Instance.ObjectsCount.RemoveAt(placeCount);
                    StackSystem.Instance.Objects.RemoveAt(placeCount);
                    placeCount++;
                }
                //hareket fonksiyonu;
            }
        }
        //A� ayaralanacak
    }

    public int PlayerPrefsContract(int contractCount, int itemCount)
    {
        contractCount++;
        FocusContract[0].itemCount.Add(contractCount, itemCount);
        return contractCount;
    }
}
