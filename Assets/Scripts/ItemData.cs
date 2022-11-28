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
            if (factor.AICount[i] == 0)
            {
                fieldPrice.AIStackCount[i] = fieldPrice.AIStackCountTemp;
                fieldPrice.AICount[i] = fieldPrice.AICountTemp;
                continue;
            }

            field.AICount[i] = standart.AICountTemp + (factor.AICount[i] * constant.AICountTemp);
            fieldPrice.AICount[i] = fieldPrice.AICountTemp * factor.AICount[i];

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
        Buttons.Instance.stackCountButton.enabled = true;

        if (factor.playerStackCount == maxFactor.playerStackCount)
        {
            Buttons.Instance.stackCountButton.enabled = false;
            Buttons.Instance.stackCountText.text = "Full";
        }
        else
            Buttons.Instance.stackCountText.text = ItemData.Instance.fieldPrice.playerStackCount.ToString();
    }

    public void SetAICount(int StackerTypeCount)
    {
        if (factor.AICount[StackerTypeCount] != 0)
            fieldPrice.AICount[StackerTypeCount] = fieldPrice.AICountTemp / factor.AICount[StackerTypeCount];
        factor.AICount[StackerTypeCount]++;
        fieldPrice.AICount[StackerTypeCount] = fieldPrice.AICountTemp * factor.AICount[StackerTypeCount];
        field.AICount[StackerTypeCount] = standart.AICountTemp + (factor.AICount[StackerTypeCount] * constant.AICountTemp);

        GameManager.Instance.FactorPlacementWrite(factor);
        Buttons.Instance.AICountButton.enabled = true;

        if (factor.AICount[Buttons.Instance.GarbageCarCount] == maxFactor.AICountTemp)
        {
            Buttons.Instance.AICountButton.enabled = false;
            Buttons.Instance.AICountText.text = "Full";

        }
        else
            Buttons.Instance.AICountText.text = ItemData.Instance.fieldPrice.AICount[StackerTypeCount].ToString();
    }

    public void SetAIStackCount(int StackerTypeCount)
    {
        if (factor.AIStackCount[StackerTypeCount] != 0)
            fieldPrice.AIStackCount[StackerTypeCount] = fieldPrice.AIStackCountTemp / factor.AIStackCount[StackerTypeCount];
        factor.AIStackCount[StackerTypeCount]++;
        fieldPrice.AIStackCount[StackerTypeCount] = fieldPrice.AIStackCountTemp * factor.AIStackCount[StackerTypeCount];
        field.AIStackCount[StackerTypeCount] = standart.AIStackCountTemp + (factor.AIStackCount[StackerTypeCount] * constant.AIStackCountTemp);
        GameManager.Instance.FactorPlacementWrite(factor);
        Buttons.Instance.AIStackCountButton.enabled = true;

        if (factor.AIStackCount[Buttons.Instance.GarbageCarCount] == maxFactor.AIStackCountTemp)
        {
            Buttons.Instance.AIStackCountButton.enabled = false;
            Buttons.Instance.AIStackCountText.text = "Full";

        }
        else
            Buttons.Instance.AIStackCountText.text = ItemData.Instance.fieldPrice.AIStackCount[Buttons.Instance.GarbageCarCount].ToString();

    }

    public void SetDirtyGarbage()
    {
        fieldPrice.dirtyGarbage = fieldPrice.dirtyGarbage / factor.dirtyGarbage;
        factor.dirtyGarbage++;
        fieldPrice.dirtyGarbage = fieldPrice.dirtyGarbage * factor.dirtyGarbage;
        field.dirtyGarbage = standart.dirtyGarbage + (factor.dirtyGarbage * constant.dirtyGarbage);

        GameManager.Instance.FactorPlacementWrite(factor);
        Buttons.Instance.dirtyThrashCountButton.enabled = true;

        if (factor.dirtyGarbage == maxFactor.dirtyGarbage)
        {
            Buttons.Instance.dirtyThrashCountButton.enabled = false;
            Buttons.Instance.dirtyThrashCountText.text = "Full";

        }
        else
            Buttons.Instance.dirtyThrashCountText.text = ItemData.Instance.fieldPrice.dirtyGarbage.ToString();
    }

    public void SetGarbageCar()
    {
        fieldPrice.garbageCar = fieldPrice.garbageCar / factor.garbageCar;
        factor.garbageCar++;
        fieldPrice.garbageCar = fieldPrice.garbageCar * factor.garbageCar;
        field.garbageCar = standart.garbageCar + (factor.garbageCar * constant.garbageCar);

        if (factor.garbageCar != maxFactor.garbageCar)
        {
            fieldPrice.AICount[factor.garbageCar - 1] = fieldPrice.AICountTemp;
            fieldPrice.AIStackCount[factor.garbageCar - 1] = fieldPrice.AIStackCountTemp;
        }

        GameManager.Instance.FactorPlacementWrite(factor);
        ContractSystem.Instance.FocusContract.contractLimit = field.garbageCar;
        GameManager.Instance.ContractPlacementWrite(ContractSystem.Instance.FocusContract);
        Buttons.Instance.contractCountButton.enabled = true;

        if (factor.garbageCar == maxFactor.garbageCar)
        {
            Buttons.Instance.contractCountButton.enabled = false;
            Buttons.Instance.contractCountText.text = "Full";
        }
        else
            Buttons.Instance.contractCountText.text = ItemData.Instance.fieldPrice.garbageCar.ToString();
    }
}
