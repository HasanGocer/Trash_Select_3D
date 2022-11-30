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
            Buttons buttons = Buttons.Instance;
            ItemData itemData = ItemData.Instance;

            gabageCarSidePanel.SetActive(true);
            buttons.GarbageCarCount = other.GetComponent<ColliderManager>().ContractCount;
            buttons.AICountText.text = itemData.fieldPrice.AICount[buttons.GarbageCarCount].ToString();
            buttons.AICountButton.enabled = true;
            if (itemData.factor.AICount[buttons.GarbageCarCount] == itemData.maxFactor.AICountTemp)
            {
                buttons.AICountText.text = "Full";
                buttons.AICountButton.enabled = false;
            }
            buttons.AIStackCountText.text = itemData.fieldPrice.AIStackCount[buttons.GarbageCarCount].ToString();
            buttons.AIStackCountButton.enabled = true;
            if (itemData.factor.AIStackCount[buttons.GarbageCarCount] == itemData.maxFactor.AIStackCountTemp)
            {
                buttons.AIStackCountText.text = "Full";
                buttons.AIStackCountButton.enabled = false;
            }
            buttons.contractCountText.text = itemData.fieldPrice.garbageCar.ToString();
            buttons.contractCountButton.enabled = true;
            if (itemData.factor.garbageCar == itemData.maxFactor.garbageCar)
            {
                buttons.contractCountText.text = "Full";
                buttons.contractCountButton.enabled = false;
            }
        }
        else if (other.CompareTag("Mid"))
        {
            Buttons buttons = Buttons.Instance;
            ItemData itemData = ItemData.Instance;

            midSidePanel.SetActive(true);
            buttons.stackCountText.text = itemData.fieldPrice.playerStackCount.ToString();
            buttons.stackCountButton.enabled = true;
            if (itemData.factor.playerStackCount == itemData.maxFactor.playerStackCount)
            {
                buttons.stackCountText.text = "Full";
                buttons.stackCountButton.enabled = false;
            }
        }
        else if (other.CompareTag("Dirty"))
        {
            Buttons buttons = Buttons.Instance;
            ItemData itemData = ItemData.Instance;

            dirtySidePanel.SetActive(true);
            buttons.dirtyThrashCountText.text = itemData.fieldPrice.dirtyGarbage.ToString();
            buttons.dirtyThrashCountButton.enabled = true;
            if (itemData.factor.AIStackCount[buttons.GarbageCarCount] == itemData.maxFactor.AIStackCountTemp)
            {
                buttons.dirtyThrashCountText.text = "Full";
                buttons.dirtyThrashCountButton.enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GarbageCar") || other.CompareTag("Mid") || other.CompareTag("Dirty"))
        {
            gabageCarSidePanel.SetActive(false);
            dirtySidePanel.SetActive(false);
            midSidePanel.SetActive(false);
        }
    }
}
