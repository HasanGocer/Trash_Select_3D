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
        }
        else if (other.CompareTag("Mid"))
        {
            midSidePanel.SetActive(true);
        }
        else if (other.CompareTag("Dirty"))
        {
            dirtySidePanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        gabageCarSidePanel.SetActive(false);
        midSidePanel.SetActive(false);
        dirtySidePanel.SetActive(false);
    }
}
