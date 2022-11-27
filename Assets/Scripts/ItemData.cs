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

    public void IDAwake()
    {
        field.playerStackCount = standart.playerStackCount + (factor.playerStackCount * constant.playerStackCount);
        fieldPrice.playerStackCount = fieldPrice.playerStackCount * factor.playerStackCount;

        for (int i = 0; i < AIManager.Instance.maxStackerTypeCount; i++)
        {
            field.AICount[i] = standart.AICountTemp + (factor.AICount[i] * constant.AICountTemp);
            fieldPrice.AICount[i] = fieldPrice.AICount[i] * factor.AICount[i];
        }

        for (int i = 0; i < AIManager.Instance.maxStackerTypeCount; i++)
        {
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
        factor.playerStackCount++;
        fieldPrice.playerStackCount = fieldPrice.playerStackCount * factor.playerStackCount;
        field.playerStackCount = standart.playerStackCount + (factor.playerStackCount * constant.playerStackCount);
        GameManager.Instance.FactorPlacementWrite(factor);
    }

    public void SetAICount(int StackerTypeCount)
    {
        fieldPrice.AICount[StackerTypeCount] = fieldPrice.AICount[StackerTypeCount] / factor.AICount[StackerTypeCount];
        factor.AICount[StackerTypeCount]++;
        fieldPrice.AICount[StackerTypeCount] = fieldPrice.AICount[StackerTypeCount] * factor.AICount[StackerTypeCount];
        field.AICount[StackerTypeCount] = standart.AICountTemp + (factor.AICount[StackerTypeCount] * constant.AICountTemp);
        GameManager.Instance.FactorPlacementWrite(factor);
    }

    public void SetAIStackCount(int StackerTypeCount)
    {
        fieldPrice.AIStackCount[StackerTypeCount] = fieldPrice.AIStackCount[StackerTypeCount] / factor.AIStackCount[StackerTypeCount];
        factor.AIStackCount[StackerTypeCount]++;
        fieldPrice.AIStackCount[StackerTypeCount] = fieldPrice.AIStackCount[StackerTypeCount] * factor.AIStackCount[StackerTypeCount];
        field.AIStackCount[StackerTypeCount] = standart.AIStackCountTemp + (factor.AIStackCount[StackerTypeCount] * constant.AIStackCountTemp);
        GameManager.Instance.FactorPlacementWrite(factor);
    }

    public void SetDirtyGarbage()
    {
        fieldPrice.dirtyGarbage = fieldPrice.dirtyGarbage / factor.dirtyGarbage;
        factor.dirtyGarbage++;
        fieldPrice.dirtyGarbage = fieldPrice.dirtyGarbage * factor.dirtyGarbage;
        field.dirtyGarbage = standart.dirtyGarbage + (factor.dirtyGarbage * constant.dirtyGarbage);
        GameManager.Instance.FactorPlacementWrite(factor);
    }

    public void SetGarbageCar()
    {
        fieldPrice.garbageCar = fieldPrice.garbageCar / factor.garbageCar;
        factor.garbageCar++;
        fieldPrice.garbageCar = fieldPrice.garbageCar * factor.garbageCar;
        field.garbageCar = standart.garbageCar + (factor.garbageCar * constant.garbageCar);
        GameManager.Instance.FactorPlacementWrite(factor);
        ContractSystem.Instance.FocusContract.contractLimit = field.garbageCar;
        GameManager.Instance.ContractPlacementWrite(ContractSystem.Instance.FocusContract);

    }
}
