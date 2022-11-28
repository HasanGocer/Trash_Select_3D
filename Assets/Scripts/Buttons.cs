using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoSingleton<Buttons>
{
    //managerde bulunacak

    public Text moneyText;

    [SerializeField] private Button _startButton;

    [SerializeField] private Button _settingButton;
    [SerializeField] private GameObject _settingGame;

    [SerializeField] private Sprite _red, _green;
    [SerializeField] private Button _settingBackButton;
    [SerializeField] private Button _soundButton, _vibrationButton;

    public Button _contractBackButton;
    [SerializeField] private Button _contractAceptedButton, _bactToTheContractSelectPanelButton;
    public GameObject contractGame, newContractSelectGame, contractSelectGame;
    public Button AIStackCountButton, AICountButton, stackCountButton, dirtyThrashCountButton, contractCountButton;
    public Text AIStackCountText, AICountText, stackCountText, dirtyThrashCountText, contractCountText;
    public int GarbageCarCount;

    private void Start()
    {
        ButtonPlacement();
        if (GameManager.Instance.sound == 1)
        {
            _soundButton.gameObject.GetComponent<Image>().sprite = _green;
            //SoundSystem.Instance.MainMusicPlay();
        }
        else
        {
            _soundButton.gameObject.GetComponent<Image>().sprite = _red;
        }

        if (GameManager.Instance.vibration == 1)
        {
            _vibrationButton.gameObject.GetComponent<Image>().sprite = _green;
        }
        else
        {
            _vibrationButton.gameObject.GetComponent<Image>().sprite = _red;
        }
    }

    private void ButtonPlacement()
    {
        _settingButton.onClick.AddListener(SettingButton);
        _settingBackButton.onClick.AddListener(SettingBackButton);
        _soundButton.onClick.AddListener(SoundButton);
        _vibrationButton.onClick.AddListener(VibrationButton);
        _startButton.onClick.AddListener(StartButton);
        _contractBackButton.onClick.AddListener(ContractCloseButton);
        _contractAceptedButton.onClick.AddListener(() => ContractUISystem.Instance.SelectTheContract(ContractUISystem.Instance.contractCount, ContractUISystem.Instance.contract[ContractUISystem.Instance.contractCount]));
        _bactToTheContractSelectPanelButton.onClick.AddListener(ContractUISystem.Instance.BackToTheContracts);

        dirtyThrashCountButton.onClick.AddListener(DirtyThrashCountFunc);
        AIStackCountButton.onClick.AddListener(AIStackCountFunc);
        AICountButton.onClick.AddListener(AIStackFunc);
        contractCountButton.onClick.AddListener(ContractCountFunc);
        stackCountButton.onClick.AddListener(StackCountFunc);

        ContractUISystem.Instance.PosNewContractButton[0].onClick.AddListener(() => ContractUISystem.Instance.TouchContractButton(ContractUISystem.Instance.contract[0], 0));
        ContractUISystem.Instance.PosNewContractButton[1].onClick.AddListener(() => ContractUISystem.Instance.TouchContractButton(ContractUISystem.Instance.contract[1], 1));
        ContractUISystem.Instance.PosNewContractButton[2].onClick.AddListener(() => ContractUISystem.Instance.TouchContractButton(ContractUISystem.Instance.contract[2], 2));
        ContractUISystem.Instance.PosNewContractButton[3].onClick.AddListener(() => ContractUISystem.Instance.TouchContractButton(ContractUISystem.Instance.contract[3], 3));
        ContractUISystem.Instance.PosNewContractButton[4].onClick.AddListener(() => ContractUISystem.Instance.TouchContractButton(ContractUISystem.Instance.contract[4], 4));

        ContractUISystem.Instance.backContractButton.onClick.AddListener(ContractUISystem.Instance.BackToTheContracts);
        ContractUISystem.Instance.acceptedContractButton.onClick.AddListener(() => ContractUISystem.Instance.SelectTheContract(ContractUISystem.Instance.contractCount, ContractUISystem.Instance.contract[ContractUISystem.Instance.selectContractCount]));
    }

    private void StartButton()
    {
        _startButton.gameObject.SetActive(false);
        GarbageSystem.Instance.GarbagePlacement();
        ContractSystem.Instance.ObjectCountUpdate();
        for (int i = 0; i < ContractSystem.Instance.FocusContract.Contracts.Count; i++)
        {
            ContractSystem.Instance.WaitSystemCountPlacement(ContractSystem.Instance.waitBarUSCount, i);
        }
        StartCoroutine(RocketManager.Instance.RocketStart());
        DirtyManager.Instance.DirtyManagerStart();
        UpgradeManager.Instance.UpgradeSystemStart();
        ContractUISystem.Instance.contract = new ContractSystem.Contract[ContractUISystem.Instance.contractLimitCount];
        AIManager.Instance.StartPlace();
    }

    private void ContractCloseButton()
    {
        contractGame.SetActive(false);
    }

    private void SettingButton()
    {
        _settingGame.SetActive(true);
        _settingButton.gameObject.SetActive(false);
    }

    private void SettingBackButton()
    {
        _settingGame.SetActive(false);
        _settingButton.gameObject.SetActive(true);
    }

    private void SoundButton()
    {
        if (GameManager.Instance.sound == 1)
        {
            GameManager.Instance.sound = 0;
            _soundButton.gameObject.GetComponent<Image>().sprite = _red;
            SoundSystem.Instance.MainMusicStop();
            GameManager.Instance.sound = 0;
            GameManager.Instance.SetSound();
        }
        else
        {
            GameManager.Instance.sound = 1;
            _soundButton.gameObject.GetComponent<Image>().sprite = _green;
            SoundSystem.Instance.MainMusicPlay();
            GameManager.Instance.sound = 1;
            GameManager.Instance.SetSound();
        }
    }

    private void VibrationButton()
    {
        if (GameManager.Instance.vibration == 1)
        {
            GameManager.Instance.vibration = 0;
            _vibrationButton.gameObject.GetComponent<Image>().sprite = _red;
            GameManager.Instance.vibration = 0;
            GameManager.Instance.SetVibration();
        }
        else
        {
            GameManager.Instance.vibration = 1;
            _vibrationButton.gameObject.GetComponent<Image>().sprite = _green;
            GameManager.Instance.vibration = 1;
            GameManager.Instance.SetVibration();
        }
    }

    private void AIStackCountFunc()
    {
        if (GameManager.Instance.money >= ItemData.Instance.fieldPrice.AIStackCount[GarbageCarCount] && ItemData.Instance.factor.AIStackCount[GarbageCarCount] <= ItemData.Instance.maxFactor.AIStackCountTemp)
        {
            MoneySystem.Instance.MoneyTextRevork(ItemData.Instance.fieldPrice.AIStackCount[GarbageCarCount] * -1);
            ItemData.Instance.SetAIStackCount(GarbageCarCount);
        }
    }

    private void AIStackFunc()
    {
        if (GameManager.Instance.money >= ItemData.Instance.fieldPrice.AICount[GarbageCarCount] && ItemData.Instance.factor.AICount[GarbageCarCount] <= ItemData.Instance.maxFactor.AICountTemp)
        {
            MoneySystem.Instance.MoneyTextRevork(ItemData.Instance.fieldPrice.AICount[GarbageCarCount] * -1);
            ItemData.Instance.SetAICount(GarbageCarCount);
            print(1);

            if (ItemData.Instance.factor.AICount[GarbageCarCount] == 1)
                ItemData.Instance.factor.AIStackCount[GarbageCarCount]++;

            AIManager.Instance.stackerInGame[GarbageCarCount].boolStacker.Add(true);
            AIManager.Instance.stackerInGame[GarbageCarCount].gameObjectStacker[ItemData.Instance.field.AICount[GarbageCarCount] - 1].SetActive(true);
            StartCoroutine(AIManager.Instance.stackerInGame[GarbageCarCount].gameObjectStacker[ItemData.Instance.field.AICount[GarbageCarCount] - 1].GetComponent<AIStackAndDrop>().WalkAI(AIManager.Instance.stackerInGame[GarbageCarCount].stackOutPlace));
        }
    }

    private void ContractCountFunc()
    {
        if (GameManager.Instance.money >= ItemData.Instance.fieldPrice.garbageCar && ItemData.Instance.factor.garbageCar <= ItemData.Instance.maxFactor.garbageCar)
        {
            MoneySystem.Instance.MoneyTextRevork(ItemData.Instance.fieldPrice.garbageCar * -1);
            ItemData.Instance.SetGarbageCar();

            ContractSystem.Contract contract = new ContractSystem.Contract();
            ContractSystem.Instance.FocusContract.Contracts.Add(contract);
            UpgradeManager.Instance._upgradeItem[GarbageSystem.Instance.ContractGarbageUSCount]._items[ItemData.Instance.factor.garbageCar - 1].SetActive(true);
        }
    }

    private void StackCountFunc()
    {
        if (GameManager.Instance.money >= ItemData.Instance.fieldPrice.playerStackCount && ItemData.Instance.factor.playerStackCount <= ItemData.Instance.maxFactor.playerStackCount)
        {
            MoneySystem.Instance.MoneyTextRevork(ItemData.Instance.fieldPrice.playerStackCount * -1);
            ItemData.Instance.SetPlayerStackCount();
        }
    }

    private void DirtyThrashCountFunc()
    {
        if (GameManager.Instance.money >= ItemData.Instance.fieldPrice.dirtyGarbage && ItemData.Instance.factor.dirtyGarbage <= ItemData.Instance.maxFactor.dirtyGarbage)
        {
            MoneySystem.Instance.MoneyTextRevork(ItemData.Instance.fieldPrice.dirtyGarbage * -1);
            ItemData.Instance.SetDirtyGarbage();

            UpgradeManager.Instance._upgradeItem[GarbageSystem.Instance.garbagePlaceUSCount]._items[ItemData.Instance.factor.dirtyGarbage - 1].SetActive(true);
            StartCoroutine(UpgradeManager.Instance._upgradeItem[GarbageSystem.Instance.garbagePlaceUSCount]._items[ItemData.Instance.factor.dirtyGarbage - 1].GetComponent<FirstSpawn>().ItemSpawn());
        }
    }
}
