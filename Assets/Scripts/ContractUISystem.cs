using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContractUISystem : MonoSingleton<ContractUISystem>
{
    [SerializeField] private List<Image> _TemplateImage = new List<Image>();
    [SerializeField] private List<Image> _PosImage = new List<Image>();
    [SerializeField] private List<Text> _PosText = new List<Text>();
    public List<Button> PosButton = new List<Button>();

    public int contractCount;
    public int selectedContractCount;
    [SerializeField] private int contractLimitCount;

    public ContractSystem.Contract[] contract;

    public void ContractUIPlacement(ContractSystem.Contract contract, int selectedContractCount = -1)
    {
        Buttons.Instance._contractGame.SetActive(true);
        if (ContractSystem.Instance.FocusContract[contractCount].ContractBool)
            this.selectedContractCount = selectedContractCount;

        for (int i = 0; i < _TemplateImage.Count; i++)
        {
            _PosImage[i].gameObject.SetActive(false);
            _PosText[i].gameObject.SetActive(false);
            PosButton[i].gameObject.SetActive(false);
        }

        for (int i2 = 0; i2 < contract.objectTypeCount.Count; i2++)
        {
            _PosImage[i2] = _TemplateImage[contract.objectTypeCount[i2]];
            _PosImage[i2].gameObject.SetActive(true);
            _PosText[i2].text = contract.objectCount[i2].ToString();
            _PosText[i2].gameObject.SetActive(true);
        }
    }

    public void SelectTheContract(int contractCount, ContractSystem.Contract contract)
    {
        ContractSystem.Instance.FocusContract[contractCount] = contract;
    }

    public void BackToTheContracts()
    {
        for (int i = 0; i < contractLimitCount; i++)
        {
            PosButton[i].gameObject.SetActive(true);
            _PosImage[i].gameObject.SetActive(false);
            _PosText[i].gameObject.SetActive(false);
        }
    }

    public void NewContractPlacement()
    {
        contract = new ContractSystem.Contract[contractLimitCount];
        for (int i = 0; i < contractLimitCount; i++)
        {
            contract[i] = ContractSystem.Instance.NewContractForUI(ContractSystem.Instance.levelMod, ContractSystem.Instance.maxItemCount, ContractSystem.Instance.maxitemTypeCount, ContractSystem.Instance.contractBudge);
            PosButton[i].gameObject.SetActive(true);
            _PosImage[i].gameObject.SetActive(false);
            _PosText[i].gameObject.SetActive(false);
        }
    }
}
