using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitSystem : MonoBehaviour
{
    //place de bulunacak

    public int contractCount;
    public int[] placeCount;
    public bool inPlace;
    [SerializeField] private float _timerSpeed;
    [SerializeField] private Image _barImage;
    [SerializeField] private GameObject objectPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inPlace = true;
            StartCoroutine(bar(other.gameObject));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            inPlace = false;
    }


    public IEnumerator bar(GameObject player)
    {
        float timer = 0;

        while (inPlace)
        {
            timer += Time.deltaTime * _timerSpeed;
            _barImage.fillAmount = Mathf.Lerp(1, 0, timer);
            yield return new WaitForEndOfFrame();
            if (_barImage.fillAmount == 0)
            {
                inPlace = false;
                StartCoroutine(StackSystem.Instance.StackDrop(this, objectPos, objectPos.transform.position));
                _barImage.fillAmount = 1;
                break;
            }
        }
        _barImage.fillAmount = 1;
    }
}
