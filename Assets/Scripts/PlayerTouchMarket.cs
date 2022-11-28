using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchMarket : MonoBehaviour
{
    [SerializeField] private GameObject gabageCarSidePanel, midSidePanel, dirtySidePanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GarbageCar"))
        {
            gabageCarSidePanel.SetActive(true);
            Buttons.Instance.GarbageCarCount = other.GetComponent<ColliderManager>().ContractCount;
            Buttons.Instance.AICountText.text = ItemData.Instance.fieldPrice.AICount[Buttons.Instance.GarbageCarCount].ToString();
            Buttons.Instance.AICountButton.enabled = true;
            if (ItemData.Instance.factor.AICount[Buttons.Instance.GarbageCarCount] == ItemData.Instance.maxFactor.AICountTemp)
            {
                Buttons.Instance.AICountText.text = "Full";
                Buttons.Instance.AICountButton.enabled = false;
            }
            Buttons.Instance.AIStackCountText.text = ItemData.Instance.fieldPrice.AIStackCount[Buttons.Instance.GarbageCarCount].ToString();
            Buttons.Instance.AIStackCountButton.enabled = true;
            if (ItemData.Instance.factor.AIStackCount[Buttons.Instance.GarbageCarCount] == ItemData.Instance.maxFactor.AIStackCountTemp)
            {
                Buttons.Instance.AIStackCountText.text = "Full";
                Buttons.Instance.AIStackCountButton.enabled = false;
            }
            Buttons.Instance.contractCountText.text = ItemData.Instance.fieldPrice.garbageCar.ToString();
            Buttons.Instance.contractCountButton.enabled = true;
            if (ItemData.Instance.factor.garbageCar == ItemData.Instance.maxFactor.garbageCar)
            {
                Buttons.Instance.contractCountText.text = "Full";
                Buttons.Instance.contractCountButton.enabled = false;
            }
        }
        else if (other.CompareTag("Mid"))
        {
            midSidePanel.SetActive(true);
            Buttons.Instance.stackCountText.text = ItemData.Instance.fieldPrice.playerStackCount.ToString();
            Buttons.Instance.stackCountButton.enabled = true;
            if (ItemData.Instance.factor.playerStackCount == ItemData.Instance.maxFactor.playerStackCount)
            {
                Buttons.Instance.stackCountText.text = "Full";
                Buttons.Instance.stackCountButton.enabled = false;
            }
        }
        else if (other.CompareTag("Dirty"))
        {
            dirtySidePanel.SetActive(true);
            Buttons.Instance.dirtyThrashCountText.text = ItemData.Instance.fieldPrice.dirtyGarbage.ToString();
            Buttons.Instance.dirtyThrashCountButton.enabled = true;
            if (ItemData.Instance.factor.AIStackCount[Buttons.Instance.GarbageCarCount] == ItemData.Instance.maxFactor.AIStackCountTemp)
            {
                Buttons.Instance.dirtyThrashCountText.text = "Full";
                Buttons.Instance.dirtyThrashCountButton.enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        gabageCarSidePanel.SetActive(false);
        midSidePanel.SetActive(false);
        dirtySidePanel.SetActive(false);
    }
}
