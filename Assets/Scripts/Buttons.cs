using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoSingleton<Buttons>
{
    //managerde bulunacak

    [SerializeField] private GameObject _money;

    [SerializeField] private Button _startButton;

    [SerializeField] private Button _settingButton;
    [SerializeField] private GameObject _settingGame;

    [SerializeField] private Sprite _red, _green;
    [SerializeField] private Button _settingBackButton;
    [SerializeField] private Button _soundButton, _vibrationButton;

    public Button _contractBackButton;
    [SerializeField] private Button _contractAceptedButton, _bactToTheContractSelectPanelButton;
    public GameObject contractGame, newContractSelectGame, contractSelectGame;

    public Button marketButton;

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

        //hata yeni kod yaz
        ContractUISystem.Instance.PosNewContractButton[0].onClick.AddListener(() => ContractUISystem.Instance.TouchContractButton(ContractUISystem.Instance.contract[0]));
        ContractUISystem.Instance.PosNewContractButton[1].onClick.AddListener(() => ContractUISystem.Instance.TouchContractButton(ContractUISystem.Instance.contract[1]));
        ContractUISystem.Instance.PosNewContractButton[2].onClick.AddListener(() => ContractUISystem.Instance.TouchContractButton(ContractUISystem.Instance.contract[2]));
        ContractUISystem.Instance.PosNewContractButton[3].onClick.AddListener(() => ContractUISystem.Instance.TouchContractButton(ContractUISystem.Instance.contract[3]));
        ContractUISystem.Instance.PosNewContractButton[4].onClick.AddListener(() => ContractUISystem.Instance.TouchContractButton(ContractUISystem.Instance.contract[4]));
    }

    private void StartButton()
    {
        print(1);
        _startButton.gameObject.SetActive(false);
        print(2);
        GarbageSystem.Instance.GarbagePlacement();
        print(3);
        RocketManager.Instance.RocketStart();
        print(4);
        DirtyManager.Instance.DirtyManagerStart();
        print(5);
        AIManager.Instance.StartPlace();
        print(6);
        UpgradeManager.Instance.UpgradeSystemStart();
        print(7);
    }

    private void ContractCloseButton()
    {
        contractGame.SetActive(false);
    }

    private void SettingButton()
    {
        _settingGame.SetActive(true);
        _settingButton.gameObject.SetActive(false);
        _money.SetActive(false);
    }

    private void SettingBackButton()
    {
        _settingGame.SetActive(false);
        _settingButton.gameObject.SetActive(true);
        _money.SetActive(true);
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
}
