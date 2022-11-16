using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCarTouch : MonoBehaviour
{
    public int contractCount;

    private void OnCollisionEnter(Collision collision)
    {
        if (ContractSystem.Instance.FocusContract[contractCount].ContractBool)
            ContractUISystem.Instance.ContractUIPlacement(ContractSystem.Instance.FocusContract[contractCount]);
        else
            ContractUISystem.Instance.NewContractPlacement();
        ContractUISystem.Instance.contractCount = contractCount;
    }
}
