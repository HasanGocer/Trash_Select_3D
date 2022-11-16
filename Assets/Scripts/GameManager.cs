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
        ContractPlacement();
    }

    public void ContractPlacement()
    {
        //Json Gerekli
        if (PlayerPrefs.HasKey("contractBool"))
        {
            if (PlayerPrefs.GetInt("contractBool") == 0)
            {
                ContractSystem.Instance.FocusContract[0].ContractBool = false;
            }
            else
            {
                int contractCount = 0;
                if (PlayerPrefs.HasKey("contract1"))
                {
                    ContractSystem.Instance.PlayerPrefsContract(contractCount, PlayerPrefs.GetInt("contract1"));
                }
                if (PlayerPrefs.HasKey("contract2"))
                {
                    ContractSystem.Instance.PlayerPrefsContract(contractCount, PlayerPrefs.GetInt("contract2"));
                }
                if (PlayerPrefs.HasKey("contract3"))
                {
                    ContractSystem.Instance.PlayerPrefsContract(contractCount, PlayerPrefs.GetInt("contract3"));
                }
                if (PlayerPrefs.HasKey("contract4"))
                {
                    ContractSystem.Instance.PlayerPrefsContract(contractCount, PlayerPrefs.GetInt("contract4"));
                }
                if (PlayerPrefs.HasKey("contract5"))
                {
                    ContractSystem.Instance.PlayerPrefsContract(contractCount, PlayerPrefs.GetInt("contract5"));
                }
                ContractSystem.Instance.FocusContract[0].ContractBool = true;
            }
        }
        else
        {
            PlayerPrefs.SetInt("contractBool", 0);
            ContractSystem.Instance.FocusContract[0].ContractBool = false;
        }

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

    public void SetUpgradeTest(int i)
    {
        ItemData.Instance.test1[i].test2++;
        if (i == 0)
        {
            PlayerPrefs.SetInt("Test0", ItemData.Instance.test1[i].test2);
        }
        else if (i == 1)
        {
            PlayerPrefs.SetInt("Test1", ItemData.Instance.test1[i].test2);
        }
        else if (i == 2)
        {
            PlayerPrefs.SetInt("Test2", ItemData.Instance.test1[i].test2);
        }
        else if (i == 3)
        {
            PlayerPrefs.SetInt("Test3", ItemData.Instance.test1[i].test2);
        }
        else if (i == 4)
        {
            PlayerPrefs.SetInt("Test4", ItemData.Instance.test1[i].test2);
        }
    }
}
