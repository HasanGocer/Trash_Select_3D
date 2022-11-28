using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCarTouch : MonoBehaviour
{
    public int contractCount;

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ContractUISystem.Instance.contractCount = contractCount;
        Buttons.Instance.AICountText.text = ItemData.Instance.fieldPrice.AICount[contractCount].ToString();
        Buttons.Instance.AIStackCountText.text = ItemData.Instance.fieldPrice.AIStackCount[contractCount].ToString();

        if (ContractSystem.Instance.FocusContract.Contracts[contractCount].contractBool)
            ContractUISystem.Instance.ContractUIPlacement(ContractSystem.Instance.FocusContract.Contracts[contractCount]);
        else
            ContractUISystem.Instance.NewContractPlacement();
    }
}
