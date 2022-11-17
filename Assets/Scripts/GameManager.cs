using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    //managerde bulunacak

    public bool inTransfer;
    public bool dropTransfer;
    public bool inStart;

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
        }

        if (PlayerPrefs.HasKey("level"))
        {
            level = PlayerPrefs.GetInt("level");
        }
        else
        {
            PlayerPrefs.SetInt("level", 1);
        }

        if (PlayerPrefs.HasKey("vibration"))
        {
            vibration = PlayerPrefs.GetInt("vibration");
        }
        else
        {
            PlayerPrefs.SetInt("vibration", 1);
        }

        if (PlayerPrefs.HasKey("sound"))
        {
            sound = PlayerPrefs.GetInt("sound");
        }
        else
        {
            PlayerPrefs.SetInt("sound", 1);
        }
        ContractSystem.Instance.ContractStart();
        ContractSystem.Instance.FocusContract = ContractPlacementRead();
    }

    public void ContractPlacementWrite(ContractSystem.Contract[] contract)
    {
        string jsonData = JsonUtility.ToJson(contract);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/ContractData.json", jsonData);
    }

    public ContractSystem.Contract[] ContractPlacementRead()
    {
        string jsonRead = System.IO.File.ReadAllText(Application.persistentDataPath + "/ContractData.json");
        ContractSystem.Contract[] contracts = new ContractSystem.Contract[ContractSystem.Instance.contractLimit];
        contracts = JsonUtility.FromJson<ContractSystem.Contract[]>(jsonRead);
        return contracts;
    }

    public void SetMoney(int plus)
    {
        money += plus;
        PlayerPrefs.SetInt("money", money);
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
