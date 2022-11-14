using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitSystem : MonoBehaviour
{
    //place de bulunacak

    public bool isClear;
    public int contractCount;
    public int[] placeCount;
    public bool inPlace;
    [SerializeField] private float _timerSpeed;
    [SerializeField] private Image _barImage;
    public GameObject objectPos, AIWaitPlace;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inPlace = true;
            StartCoroutine(bar(isClear));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            inPlace = false;
    }


    public IEnumerator bar(bool isClear)
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
                if (isClear)
                    StartCoroutine(StackSystem.Instance.StackDrop(this, objectPos, objectPos.transform.position, contractCount));
                else
                    StartCoroutine(StackSystem.Instance.DirtyThrashDropObject(objectPos, objectPos.transform.position));

                _barImage.fillAmount = 1;
                break;
            }
        }
        _barImage.fillAmount = 1;
    }
}
