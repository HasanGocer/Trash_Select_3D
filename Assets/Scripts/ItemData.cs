using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoSingleton<ItemData>
{
    //managerde bulunacak

    [System.Serializable]
    public class Field
    {
        public int[] AIStackCount;
        public int[] AICount;
        public int playerStackCount, AICountTemp, AIStackCountTemp, dirtyGarbage, garbageCar;
    }

    public Field field;
    public Field standart;
    public Field factor;
    public Field constant;
    public Field maxFactor;
    public Field max;
    public Field fieldPrice;

    private void Start()
    {
        field.playerStackCount = standart.playerStackCount + (factor.playerStackCount * constant.playerStackCount);
        fieldPrice.playerStackCount = fieldPrice.playerStackCount * factor.playerStackCount;

        field.AICount = new int[AIManager.Instance.maxStackerTypeCount];
        factor.AICount = new int[AIManager.Instance.maxStackerTypeCount];
        fieldPrice.AICount = new int[AIManager.Instance.maxStackerTypeCount];

        for (int i = 0; i < AIManager.Instance.maxStackerTypeCount; i++)
        {
            field.AICount[i] = standart.AICountTemp + (factor.AICount[i] * constant.AICountTemp);
            fieldPrice.AICount[i] = fieldPrice.AICount[i] * factor.AICount[i];
        }

        field.AIStackCount = new int[ AIManager.Instance.maxStackerCount];
        factor.AIStackCount = new int[ AIManager.Instance.maxStackerCount];
        fieldPrice.AIStackCount = new int[AIManager.Instance.maxStackerCount];

            for (int i = 0; i < AIManager.Instance.maxStackerCount; i++)
            {
                factor.AIStackCount[i] = 1;
                field.AIStackCount[i] = standart.AIStackCountTemp + (factor.AIStackCount[i] * constant.AIStackCountTemp);
                fieldPrice.AIStackCount[i] = fieldPrice.AIStackCountTemp * factor.AIStackCount[i];
            }

        field.dirtyGarbage = standart.dirtyGarbage + (factor.dirtyGarbage * constant.dirtyGarbage);
        fieldPrice.dirtyGarbage = fieldPrice.dirtyGarbage * factor.dirtyGarbage;

        field.garbageCar = standart.garbageCar + (factor.garbageCar * constant.garbageCar);
        fieldPrice.garbageCar = fieldPrice.garbageCar * factor.garbageCar;
    }

    public void SetPlayerStackCount()
    {
        fieldPrice.playerStackCount = fieldPrice.playerStackCount / factor.playerStackCount;
        fieldPrice.playerStackCount++;

        fieldPrice.playerStackCount = fieldPrice.playerStackCount * factor.playerStackCount;
        field.playerStackCount = standart.playerStackCount + (factor.playerStackCount * constant.playerStackCount);
        GameManager.Instance.FactorPlacementWrite(factor);
    }

    public void SetAICount(int StackerTypeCount)
    {
        fieldPrice.AICount[StackerTypeCount] = fieldPrice.AICount[StackerTypeCount] / factor.AICount[StackerTypeCount];
        fieldPrice.AICount[StackerTypeCount]++;
        fieldPrice.AICount[StackerTypeCount] = fieldPrice.AICount[StackerTypeCount] * factor.AICount[StackerTypeCount];
        field.AICount[StackerTypeCount] = standart.AICountTemp + (factor.AICount[StackerTypeCount] * constant.AICountTemp);
        GameManager.Instance.FactorPlacementWrite(factor);
    }

    public void SetAIStackCount(int StackerTypeCount)
    {
        fieldPrice.AIStackCount[StackerTypeCount] = fieldPrice.AIStackCount[StackerTypeCount] / factor.AIStackCount[StackerTypeCount];
        fieldPrice.AIStackCount[StackerTypeCount]++;
        fieldPrice.AIStackCount[StackerTypeCount] = fieldPrice.AIStackCount[StackerTypeCount] * factor.AIStackCount[StackerTypeCount];
        field.AIStackCount[StackerTypeCount] = standart.AIStackCountTemp + (factor.AIStackCount[StackerTypeCount] * constant.AIStackCountTemp);
        GameManager.Instance.FactorPlacementWrite(factor);
    }

    public void SetDirtyGarbage()
    {
        fieldPrice.dirtyGarbage = fieldPrice.dirtyGarbage / factor.dirtyGarbage;
        fieldPrice.dirtyGarbage++;
        fieldPrice.dirtyGarbage = fieldPrice.dirtyGarbage * factor.dirtyGarbage;
        field.dirtyGarbage = standart.dirtyGarbage + (factor.dirtyGarbage * constant.dirtyGarbage);
        GameManager.Instance.FactorPlacementWrite(factor);
    }

    public void SetGarbageCar()
    {
        fieldPrice.garbageCar = fieldPrice.garbageCar / factor.garbageCar;
        fieldPrice.garbageCar++;
        fieldPrice.garbageCar = fieldPrice.garbageCar * factor.garbageCar;
        field.garbageCar = standart.garbageCar + (factor.garbageCar * constant.garbageCar);
        GameManager.Instance.FactorPlacementWrite(factor);
        ContractSystem.Instance.FocusContract.contractLimit = field.garbageCar;
    }
}
