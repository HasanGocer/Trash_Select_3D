using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCarTouch : MonoBehaviour
{
    public int contractCount;

    private void OnCollisionEnter(Collision collision)
    {
        ContractUISystem.Instance.contractCount = contractCount;
        if (ContractSystem.Instance.FocusContract.Contracts[contractCount].contractBool)
            ContractUISystem.Instance.ContractUIPlacement(ContractSystem.Instance.FocusContract.Contracts[contractCount]);
        else
            ContractUISystem.Instance.NewContractPlacement();
    }
}
