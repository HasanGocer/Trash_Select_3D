using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContractUISystem : MonoSingleton<ContractUISystem>
{
    [SerializeField] private List<Image> _TemplateImage = new List<Image>();

    [SerializeField] private List<Image> _PosContractImage = new List<Image>();
    [SerializeField] private List<Text> _PosContractText = new List<Text>();

    public List<Button> PosNewContractButton = new List<Button>();
        
    [SerializeField] private List<Image> _PosSelectImage = new List<Image>();
    [SerializeField] private List<Text> _PosSelectText = new List<Text>();


    public int contractCount;
    [SerializeField] private int contractLimitCount;

    public ContractSystem.Contract[] contract;

    public void ContractUIPlacement(ContractSystem.Contract contract)
    {
        Buttons.Instance.contractGame.SetActive(true);

        for (int i = 0; i < _TemplateImage.Count; i++)
        {
            _PosContractImage[i].gameObject.SetActive(false);
            _PosContractText[i].gameObject.SetActive(false);
        }

        for (int i2 = 0; i2 < contract.objectTypeCount.Count; i2++)
        {
            _PosContractImage[i2] = _TemplateImage[contract.objectTypeCount[i2]];
            _PosContractImage[i2].gameObject.SetActive(true);
            _PosContractText[i2].text = contract.objectCount[i2].ToString();
            _PosContractText[i2].gameObject.SetActive(true);
        }
    }

    public void TouchContractButton(ContractSystem.Contract contract)
    {
        Buttons.Instance.contractSelectGame.SetActive(true);

        for (int i = 0; i < _TemplateImage.Count; i++)
        {
            _PosSelectImage[i].gameObject.SetActive(false);
            _PosSelectText[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < contract.objectTypeCount.Count; i++)
        {
            _PosSelectImage[i] = _TemplateImage[contract.objectTypeCount[i]];
            _PosSelectImage[i].gameObject.SetActive(false);
            _PosSelectText[i].text = contract.objectCount[i].ToString();
            _PosSelectText[i].gameObject.SetActive(true);
        }

    }

    public void SelectTheContract(int contractCount, ContractSystem.Contract contract)
    {
        ContractSystem.Instance.FocusContract.Contracts[contractCount] = contract;
        ContractSystem.Instance.FocusContract.Contracts[contractCount].contractBool = true;
        ContractSystem.Instance.FocusContract.Contracts[contractCount].contractBuy = true;
        ContractSystem.Instance.WaitSystemCountPlacement(ContractSystem.Instance.waitBarUSCount, contractCount);
    }

    public void BackToTheContracts()
    {
        for (int i = 0; i < contractLimitCount; i++)
        {
            PosNewContractButton[i].gameObject.SetActive(true);
        }
    }

    public void NewContractPlacement()
    {
        contract = new ContractSystem.Contract[contractLimitCount];
        for (int i = 0; i < contractLimitCount; i++)
        {
            contract[i] = ContractSystem.Instance.NewContractForUI(ContractSystem.Instance.levelMod, ContractSystem.Instance.maxItemCount, ContractSystem.Instance.maxitemTypeCount, ContractSystem.Instance.contractBudge);
            PosNewContractButton[i].gameObject.SetActive(true);
        }
    }
}
