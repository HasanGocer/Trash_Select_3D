using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    //managerde bulunacak

    public bool inTransfer;
    public bool dropTransfer;
    public bool inStart;
    [SerializeField] private bool inFirstStart;

    public int money;
    public int level;
    public int researchPoint;
    public int vibration;
    public int sound;

    public void Awake()
    {
        PlayerPrefsPlacement();
    }

    private void PlayerPrefsPlacement()
    {
        if (PlayerPrefs.HasKey("money"))
        {
            money = PlayerPrefs.GetInt("money");
        }
        else
        {
            PlayerPrefs.SetInt("money", 100);
            money = PlayerPrefs.GetInt("money");
        }
        MoneySystem.Instance.MoneyTextRevork(10000);

        if (PlayerPrefs.HasKey("level"))
        {
            level = PlayerPrefs.GetInt("level");
        }
        else
        {
            PlayerPrefs.SetInt("level", 1);
            level = PlayerPrefs.GetInt("level");
        }

        if (PlayerPrefs.HasKey("XP"))
        {
            LevelSystem.Instance.XP = PlayerPrefs.GetInt("XP");
        }
        else
        {
            PlayerPrefs.SetInt("XP", LevelSystem.Instance.XP);
        }

        if (PlayerPrefs.HasKey("vibration"))
        {
            vibration = PlayerPrefs.GetInt("vibration");
        }
        else
        {
            PlayerPrefs.SetInt("vibration", 1);
            vibration = PlayerPrefs.GetInt("vibration");
        }

        if (PlayerPrefs.HasKey("sound"))
        {
            sound = PlayerPrefs.GetInt("sound");
        }
        else
        {
            PlayerPrefs.SetInt("sound", 1);
            sound = PlayerPrefs.GetInt("sound");
        }

        if (PlayerPrefs.HasKey("first"))
        {
            ContractSystem.Instance.FocusContract = ContractPlacementRead();
            ItemData.Instance.factor = FactorPlacementRead();
        }
        else
        {
            for (int i = 0; i < ContractSystem.Instance.FocusContract.contractLimit; i++)
            {
                ContractSystem.Contract contract = new ContractSystem.Contract();
                ContractSystem.Instance.FocusContract.Contracts.Add(contract);
            }
            ItemData.Instance.fieldPrice.AICount[0] = ItemData.Instance.fieldPrice.AICountTemp;
            ItemData.Instance.fieldPrice.AIStackCount[0] = ItemData.Instance.fieldPrice.AIStackCountTemp;


            ContractPlacementWrite(ContractSystem.Instance.FocusContract);
            FactorPlacementWrite(ItemData.Instance.factor);
            PlayerPrefs.SetInt("first", 1);
        }
        ContractSystem.Instance.FocusContract = ContractPlacementRead();
        ItemData.Instance.factor = FactorPlacementRead();
        ItemData.Instance.IDAwake();
    }

    public void ContractPlacementWrite(ContractSystem.ContractArray contract)
    {
        string jsonData = JsonUtility.ToJson(contract);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/ContractData.json", jsonData);
    }

    public void FactorPlacementWrite(ItemData.Field factor)
    {
        string jsonData = JsonUtility.ToJson(factor);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/FactorData.json", jsonData);
    }

    public ContractSystem.ContractArray ContractPlacementRead()
    {
        string jsonRead = System.IO.File.ReadAllText(Application.persistentDataPath + "/ContractData.json");
        ContractSystem.ContractArray contracts = new ContractSystem.ContractArray();
        contracts = JsonUtility.FromJson<ContractSystem.ContractArray>(jsonRead);
        return contracts;
    }

    public ItemData.Field FactorPlacementRead()
    {
        string jsonRead = System.IO.File.ReadAllText(Application.persistentDataPath + "/FactorData.json");
        ItemData.Field factor = new ItemData.Field();
        factor = JsonUtility.FromJson<ItemData.Field>(jsonRead);
        return factor;
    }

    public void SetMoney()
    {
        PlayerPrefs.SetInt("money", money);
        Buttons.Instance.moneyText.text = money.ToString();
    }

    public void SetLevel()
    {
        PlayerPrefs.SetInt("level", level);
    }

    public void SetXP()
    {
        PlayerPrefs.SetInt("XP", LevelSystem.Instance.XP);
    }

    public void SetSound()
    {
        PlayerPrefs.SetInt("sound", sound);
    }

    public void SetVibration()
    {
        PlayerPrefs.SetInt("vibration", vibration);
    }
}
