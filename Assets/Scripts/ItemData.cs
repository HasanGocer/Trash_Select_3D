using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoSingleton<ItemData>
{
    [System.Serializable]
    public class Field
    {
        public int runnerCount, bobinCount, tableCount;
        public float runnerSpeed, addedMoney, addedResearchPoint, barSpeed;
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
        field.runnerSpeed = standart.runnerSpeed - (factor.runnerSpeed * constant.runnerSpeed);
        fieldPrice.runnerSpeed = fieldPrice.runnerSpeed * factor.runnerSpeed;

        field.runnerCount = standart.runnerCount + (factor.runnerCount * constant.runnerCount);
        fieldPrice.runnerCount = fieldPrice.runnerCount * factor.runnerCount;
        StartCoroutine(RunnerManager.Instance.StartRunner());
        field.bobinCount = standart.bobinCount + (factor.bobinCount * constant.bobinCount);
        fieldPrice.bobinCount = fieldPrice.bobinCount * factor.bobinCount;
        BobinManager.Instance.StartBobinPlacement();
        field.tableCount = standart.tableCount + (factor.tableCount * constant.tableCount);
        fieldPrice.tableCount = fieldPrice.tableCount * factor.tableCount;
        TableBuy.Instance.TablePlacement();


        field.addedMoney = standart.addedMoney + (factor.addedMoney * constant.addedMoney);
        fieldPrice.addedMoney = fieldPrice.addedMoney * factor.addedMoney;
        field.addedResearchPoint = standart.addedResearchPoint + (factor.addedResearchPoint * constant.addedResearchPoint);
        fieldPrice.addedResearchPoint = fieldPrice.addedResearchPoint * factor.addedResearchPoint;
        field.barSpeed = standart.barSpeed - (factor.barSpeed * constant.barSpeed);
        fieldPrice.barSpeed = fieldPrice.barSpeed * factor.barSpeed;

        if (field.runnerCount > max.runnerCount)
        {
            field.runnerCount = max.runnerCount;
        }

        if (field.bobinCount > max.bobinCount)
        {
            field.bobinCount = max.bobinCount;
        }

        if (field.tableCount > max.tableCount)
        {
            field.tableCount = max.tableCount;
        }

        if (field.runnerSpeed < max.runnerSpeed)
        {
            field.runnerSpeed = max.runnerSpeed;
        }

        if (field.addedMoney > max.addedMoney)
        {
            field.addedMoney = max.addedMoney;
        }

        if (field.addedResearchPoint > max.addedResearchPoint)
        {
            field.addedResearchPoint = max.addedResearchPoint;
        }

        if (field.barSpeed < max.barSpeed)
        {
            field.barSpeed = max.barSpeed;
        }
    }

    public void RunnerCount()
    {
        field.runnerCount = standart.runnerCount + (factor.runnerCount * constant.runnerCount);

        if (field.runnerCount > max.runnerCount)
        {
            field.runnerCount = max.runnerCount;
        }
    }

    public void BobinCount()
    {
        field.bobinCount = standart.bobinCount + (factor.bobinCount * constant.bobinCount);

        if (field.bobinCount > max.bobinCount)
        {
            field.bobinCount = max.bobinCount;
        }
    }

    public void TableCount()
    {
        field.tableCount = standart.tableCount + (factor.tableCount * constant.tableCount);

        if (field.tableCount > max.tableCount)
        {
            field.tableCount = max.tableCount;
        }
    }

    public void RunnerSpeed()
    {
        field.runnerSpeed = standart.runnerSpeed - (factor.runnerSpeed * constant.runnerSpeed);

        if (field.runnerSpeed < max.runnerSpeed)
        {
            field.runnerSpeed = max.runnerSpeed;
        }
    }

    public void AddedMoney()
    {
        field.addedMoney = standart.addedMoney + (factor.addedMoney * constant.addedMoney);

        if (field.addedMoney > max.addedMoney)
        {
            field.addedMoney = max.addedMoney;
        }
    }

    public void AddedResearchPoint()
    {
        field.addedResearchPoint = standart.addedResearchPoint + (factor.addedResearchPoint * constant.addedResearchPoint);

        if (field.addedResearchPoint > max.addedResearchPoint)
        {
            field.addedResearchPoint = max.addedResearchPoint;
        }
    }

    public void BarSpeed()
    {
        field.barSpeed = standart.barSpeed - (factor.barSpeed * constant.barSpeed);

        if (field.barSpeed < max.barSpeed)
        {
            field.barSpeed = max.barSpeed;
        }
    }
}
